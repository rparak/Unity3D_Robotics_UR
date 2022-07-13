using UnityEngine;

public class JIObject : JIInstantiator.JIInstantiatorBase
{
    [SerializeField] GameObject waypointAsset;
    [SerializeField] GameObject folderAsset;
    [SerializeField] GameObject gripperAsset;
    [SerializeField] GameObject waitAsset;
    [Space]
    [SerializeField] GameObject folder;

    public void CreateWaypoint() => Create(waypointAsset);

    public void CreateFolder()
    {
        data.parentGuid = string.Empty;
        Create(folderAsset);
    }

    public void CreateGripper() => Create(gripperAsset);

    public void CreateWait() => Create(waitAsset);

    private void Create(GameObject go)
    {
        go = Instantiate(go, transform.parent);
        go.transform.SetSiblingIndex(transform.GetSiblingIndex());

        JIInstantiator.JIInstantiatorBase jiBase = go.GetComponent<JIInstantiator.JIInstantiatorBase>();
        if (InsideFolder) jiBase.data.parentGuid = data.parentGuid;
        jiBase.Select();
    }

    public override void Write(Data data)
    {
        this.data = data;
    }

    protected override void Read()
    {
        data.name = "Object";
    }

    public void PutInFolder()
    {
        InsideFolder = true;
        folder.SetActive(false);

        //Get ParentGuid but it can fail lol
        Transform topSibling = transform.parent.GetChild(transform.GetSiblingIndex() - 1);

        if (topSibling.TryGetComponent(out JIFolder jiFolder))
        {
            data.parentGuid = jiFolder.data.key;
        }
        else if(topSibling.TryGetComponent(out JIInstantiator.JIInstantiatorBase jiBase) && !string.IsNullOrEmpty(jiBase.data.parentGuid))
        {
            data.parentGuid = jiBase.data.parentGuid;
        }
        else
        {
            //Create Folder ourself
            GameObject go = Instantiate(folderAsset, transform.parent);
            go.transform.SetSiblingIndex(transform.GetSiblingIndex());
            JIFolder folder = go.GetComponent<JIFolder>();
            folder.Start();
            data.parentGuid = folder.data.key;
        }
    }

    public void PutOutOfFolder()
    {
        InsideFolder = false;

        Transform topSibling = transform.parent.GetChild(transform.GetSiblingIndex() - 1);


        if (topSibling.TryGetComponent(out JIFolder jiFolder) ||
            topSibling.TryGetComponent(out JIInstantiator.JIInstantiatorBase jiBase) && !string.IsNullOrEmpty(jiBase.data.parentGuid))
        {
            //we could be surrounded by folder childs. Lets place ourself underneath them
            //Debug.Log("Sibling problem found");

            if (transform.parent.childCount <= transform.GetSiblingIndex())
            {
                transform.SetAsLastSibling();
                //Debug.Log("Moved to end");
                return;
            }

            int siblingIndex = transform.GetSiblingIndex();
            while (true)
            {
                siblingIndex++;

                //Check if next sibling is available
                if (siblingIndex >= transform.parent.childCount)
                {
                    //Debug.Log($"Out of Transforms. Sibling {siblingIndex} and there are {transform.parent.childCount}");
                    break;
                }

                Transform nextSibling = transform.parent.GetChild(siblingIndex);
                string nextParentId = nextSibling.GetComponent<JIInstantiator.JIInstantiatorBase>().data.parentGuid;

                if (string.IsNullOrEmpty(nextParentId))
                {
                    //Debug.Log("Next one Free");
                    siblingIndex -= 2;
                    break;
                }
            }

            transform.SetSiblingIndex(++siblingIndex);
        }

    }
}
