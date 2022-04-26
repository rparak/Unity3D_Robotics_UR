using UnityEngine;
using UnityEngine.UI;
using Robot;

[RequireComponent(typeof(Button))]
public class RobotBTNOffline : MonoBehaviour
{
    

    private void OnEnable()
    {
        Connection.OnConnected += OnGoingOnline;
        Connection.OnDisconnected += OnGoingOffline;

        if (Connection.unityState == Connection.UnityState.offline) OnGoingOffline();
        else OnGoingOnline();
    }

    private void OnDisable()
    {
        Connection.OnConnected -= OnGoingOnline;
        Connection.OnDisconnected -= OnGoingOffline;
    }

    private void OnGoingOffline()
    {
        GetComponent<Button>().interactable = false;
    }

    private void OnGoingOnline()
    {
        GetComponent<Button>().interactable = true;
    }
}
