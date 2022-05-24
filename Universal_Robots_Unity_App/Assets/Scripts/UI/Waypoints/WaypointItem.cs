using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class WaypointItem : MonoBehaviour
{
    
    public TMPro.TMP_InputField nameText;


    public Waypoint waypoint;




    public void Setup(Waypoint waypoint)
    {
        this.waypoint = waypoint;
        nameText.text = waypoint.name;
    }

    public void MoveToWaypoint() => waypoint.Goto();

    public void EditName(string newName)
    {
        waypoint.name = newName;
        //int index = WaypointMenu.GetWaypointIndex(waypoint);
        //WaypointMenu.Instance.waypoints[index].name = newName;
    }


    public void Delete()
    {
        WaypointMenu.Instance.waypoints.Remove(waypoint);
        Destroy(gameObject);
    }
}
