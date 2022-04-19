using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static int maxId;
    public Data waypoint;

    public new TMPro.TMP_Text name;



    public void Setup(Data waypoint)
    {
        this.waypoint = waypoint;
        name.text = $"WP {++maxId}";
    }

    public void Goto()
    {
        Robot.CMD.MoveJ(waypoint.ToPose());
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
