using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMenu : MonoBehaviour
{
    public static WaypointMenu Instance;

    public GameObject waypointAsset;
    public Transform content;




    public void AddWaypoint()
    {
        Waypoints wp = Instantiate(waypointAsset, content).GetComponent<Waypoints>();
        wp.transform.SetAsFirstSibling();

        wp.Setup(Instantiate(Data.Current));
    }



    private void OnEnable() => Instance = this;
}
