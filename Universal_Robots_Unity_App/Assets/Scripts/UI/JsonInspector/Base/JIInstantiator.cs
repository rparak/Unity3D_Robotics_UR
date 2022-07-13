using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Instantiates its child with only a jsonString.
/// </summary>
public class JIInstantiator : MonoBehaviour
{

    public Transform content;
    public List<JIInstantiatorBase> registry;
    

    /// <summary>
    /// This creates all childs from scratch by using the json
    /// </summary>
    public void Refresh(string json)
    {
        List<JIInstantiatorBase.Data> items = JsonConvert.DeserializeObject<List<JIInstantiatorBase.Data>>(json);

        content.DeleteAllChilds();

        foreach (JIInstantiatorBase.Data item in items)
        {
            bool gotItem = false;

            foreach (JIInstantiatorBase generic in registry)
            {
                if (generic.name == item.name)
                {
                    Instantiate(generic.gameObject, content)
                        .GetComponent<JIInstantiatorBase>()
                        .Write(item);
                    gotItem = true;
                    break;
                }
            }
            if (!gotItem) Debug.LogWarning($"JIInstantiator unable to find {item.name} in registry");
        }
    }

    public void PushUpdate(string json)
    {
        JIInstantiatorBase.Data item = JsonConvert.DeserializeObject<JIInstantiatorBase.Data>(json);
        JIInstantiatorBase childItem;

        foreach (Transform child in content)
        {
            childItem = child.GetComponent<JIInstantiatorBase>();

            if (item.key == childItem.data.key)
            {
                childItem.Write(item);
                return;
            }
        }
        Debug.LogWarning($"JIInstantiator unable to find {item.name} with key {item.key} in childs");
    }



    public string ReadJsonInfo()
    {
        List<JIInstantiatorBase.Data> data = ReadData();

        return JsonConvert.SerializeObject(data,
                                               Formatting.None,
                                               settings: new JsonSerializerSettings()
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   NullValueHandling = NullValueHandling.Ignore,
                                                   DefaultValueHandling = DefaultValueHandling.Ignore
                                               });
    }

    public List<JIInstantiatorBase.Data> ReadData()
    {
        List<JIInstantiatorBase.Data> data = new List<JIInstantiatorBase.Data>();

        foreach (Transform child in content)
        {
            data.Add(child.GetComponent<JIInstantiatorBase>().GetData());
        }
        return data;
    }

#if UNITY_EDITOR

    //[Button]
    private void PrintJsonConfig()
    {
        Debug.Log(ReadJsonInfo());
    }

    //[Button]
    private void ImportJsonConfig(string json)
    {
        if (string.IsNullOrEmpty(json)) return;
        if (content.childCount != 0) return;
        Refresh(json);
    }


#endif


    public abstract class JIInstantiatorBase : MonoBehaviour
    {
        public static JIInstantiatorBase selected;

        public Data data;
        public Image selectionImage;
        public LayoutElement layout;

        public abstract void Write(Data data);
        protected abstract void Read();

        public void Select()
        {
            if (selected != null) selected.selectionImage.color = Color.white;
            selected = this;
            selectionImage.color = Color.black;
        }

        public bool InsideFolder
        {
            get { return layout.preferredWidth == 190; }
            protected set
            {
                if (value) layout.preferredWidth = 190;
                else layout.preferredWidth = 400;
            }
        }

        public virtual void Delete()
        {
            Destroy(gameObject);
        }

        public Data GetData()
        {
            Read();
            return data;
        }

        [System.Serializable]
        public class Data
        {
            public string name, key, json, parentGuid;

            [JsonConstructor]
            public Data() { }
        }
    }
}
