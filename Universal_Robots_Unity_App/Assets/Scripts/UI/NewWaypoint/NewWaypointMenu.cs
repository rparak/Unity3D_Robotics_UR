using SFB;
using UnityEngine;
using static JIInstantiator;

public class NewWaypointMenu : MonoBehaviour
{
    public static NewWaypointMenu Instance;
    public JIInstantiator ji;
    public GameObject objectObj;


    public void AddObject()
    {
        int siblingIndex = JIInstantiatorBase.selected == null ? ji.content.childCount : JIInstantiatorBase.selected.transform.GetSiblingIndex() + 1;
        GameObject go = Instantiate(objectObj, ji.content);
        go.transform.SetSiblingIndex(siblingIndex);
        JIObject jiObj = go.GetComponent<JIObject>();
        jiObj.Select();
        jiObj.PutOutOfFolder();
    }

    public void AddObjectInFolder()
    {
        int siblingIndex = JIInstantiatorBase.selected == null ? ji.content.childCount : JIInstantiatorBase.selected.transform.GetSiblingIndex() + 1;
        GameObject go = Instantiate(objectObj, ji.content);
        go.transform.SetSiblingIndex(siblingIndex);
        JIObject jiObj = go.GetComponent<JIObject>();
        jiObj.Select();
        jiObj.PutInFolder();
    }

    public async void MoveToAllWaypoints()
    {

        foreach(Transform child in ji.content)
        {
            if(child.TryGetComponent(out IJIExecute executable))
            {
                bool sucess = await executable.Execute();
                if (!sucess) return;
            }
        }
    }

    public void Save()
    {
        var extensionList = new[] {
            new ExtensionFilter("Waypoint Map", "wp")
        };

        InputReg inputReg = new InputReg();
        InputTerminal.DisableInput(inputReg);

        StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", extensionList, (string path) =>
        {
            InputTerminal.ReleaseInput(inputReg);
            Treeka.ReadWrite.WriteExact(ji.ReadJsonInfo(), path);
        });
    }

    public void Load()
    {
        var extensionList = new[] {
            new ExtensionFilter("Waypoint Map", "wp")
        };

        InputReg inputReg = new InputReg();
        InputTerminal.DisableInput(inputReg);

        StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", extensionList, false, (string[] paths) =>
        {
            InputTerminal.ReleaseInput(inputReg);
            string json = Treeka.ReadWrite.ReadExact(paths[0]);
            ji.Refresh(json);
        });
    }

    public void New()
    {
        ji.Refresh("[{\"name\":\"Header\",\"key\":\"\", \"json\":\"Wegpunkte\", \"parentGuid\":\"\"}]");
    }

    private void OnEnable() => Instance = this;
}
