using UnityEngine;

public class JIFolder : JIInstantiator.JIInstantiatorBase
{
    public TMPro.TMP_InputField nameTxt;
    bool isOpen = true;

    public override void Write(Data data)
    {
        this.data = data;
        nameTxt.text = data.json;
    }

    protected override void Read()
    {
        data.name = "Folder";
        data.json = nameTxt.text;
    }

    public void Start()
    {
        if (string.IsNullOrEmpty(data.key))
        {
            data.key = System.Guid.NewGuid().ToString();
        }
    }

    public void Toggle()
    {
        if (isOpen) Close();
        else Open();
        isOpen = !isOpen;
    }

    public void Open()
    {
        foreach(Transform child in transform.parent)
        {
            if(child.TryGetComponent(out JIInstantiator.JIInstantiatorBase jiBase))
            {
                if(jiBase.data.parentGuid == data.key)
                {
                    jiBase.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Close()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.TryGetComponent(out JIInstantiator.JIInstantiatorBase jiBase))
            {
                if (jiBase.data.parentGuid == data.key)
                {
                    jiBase.gameObject.SetActive(false);
                }
            }
        }
    }

    public override void Delete()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.TryGetComponent(out JIInstantiator.JIInstantiatorBase jiBase))
            {
                if (jiBase.data.parentGuid == data.key)
                {
                    Destroy(jiBase.gameObject);
                }
            }
        }
        Destroy(gameObject);
    }

    public async void MoveToAllWaypoint()
    {
        foreach (Transform child in transform.parent)
        {
            if(child.TryGetComponent(out JIInstantiator.JIInstantiatorBase jiBase))
            {
                if(jiBase.data.parentGuid == data.key)
                {
                    if (child.TryGetComponent(out IJIExecute executable))
                    {
                        bool sucess = await executable.Execute();
                        if (!sucess) return;
                    }
                }
            } 
        }
    }
}
