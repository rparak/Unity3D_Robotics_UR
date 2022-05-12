using SFB;
using System.Collections.Generic;
using Treeka;
using UnityEngine;

public class WaypointMenu : MonoBehaviour
{
    public static WaypointMenu Instance;

    public GameObject waypointAsset;
    public Transform content;
    [Space]
    public List<Waypoint> waypoints;


    public async void AddWaypoint()
    {
        bool gripperState = await Robot.CMD.Gripper.GetPosition() < 10;
        AddWaypoint(new Waypoint(Data.Current.jointRot, gripperState));
    }

    public void AddWaypoint(Waypoint waypoint)
    {
        WaypointItem wp = Instantiate(waypointAsset, content).GetComponent<WaypointItem>();
        wp.Setup(waypoint);
        waypoints.Add(waypoint);
    }

    public void ClearAllWaypoints()
    {
        waypoints.Clear();
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }


    public async void PlayAllWaypoints()
    {
        //Debug.Log("Checking all Waypoints");
        foreach(Waypoint wp in waypoints)
        {
            //Debug.Log($"Going {wp.name} as {wp.guid}");
            if (await wp.GotoAsync() == false) return;
        }
        //Debug.Log("Done");
    }

    // /////////////////////////////////// Mananananage stuff

    public void Save()
    {
        var extensionList = new[] {
            new ExtensionFilter("Waypoint Map", "wp"),
            new ExtensionFilter("Shitty Files", "shit")
        };

        StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", extensionList, (string path) => 
        {
            ReadWrite.Write(waypoints, path);
        });
    }

    public void Load()
    {
        var extensionList = new[] {
            new ExtensionFilter("Waypoint Map", "wp")
        };

        StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", extensionList, false, (string[] paths) => 
        {
            List<Waypoint> waypoints = ReadWrite.Read<List<Waypoint>>(paths[0]);

            ClearAllWaypoints();

            foreach(Waypoint waypoint in waypoints)
            {
                AddWaypoint(waypoint);
            }
        });
    }

    private void OnEnable()
    {
        Instance = this;
    }
}
