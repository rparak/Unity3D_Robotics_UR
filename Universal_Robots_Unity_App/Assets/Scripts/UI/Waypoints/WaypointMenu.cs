using UnityEngine;

public class WaypointMenu : MonoBehaviour
{
    public static WaypointMenu Instance;

    public GameObject waypointAsset;
    public Transform content;




    public void AddWaypoint()
    {
        Waypoint wp = Instantiate(waypointAsset, content).GetComponent<Waypoint>();
        wp.transform.SetAsFirstSibling();
        
        wp.Setup(Instantiate(Data.Current));
    }


    public async void MoveToAll()
    {
        foreach(Transform child in content)
        {
            //Incase there is an error we will won't move to the next one
            if(child.TryGetComponent(out Waypoint waypoint))
            {
                Debug.Log("Executing " + waypoint.name);
                if (await child.GetComponent<Waypoint>().GotoAsync() == false) return;
            }
        }

    }

    private void OnEnable() => Instance = this;
}
