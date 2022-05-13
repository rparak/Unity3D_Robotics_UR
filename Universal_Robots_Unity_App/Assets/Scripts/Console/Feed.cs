using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Feed subscribes to other things and outputs them into the chat.
/// </summary>
public class Feed : MonoBehaviour
{
    private void OnEnable()
    {
        Robot.CMD.OnSend += OnConnectionDashboard;
    }

    private void OnDisable()
    {
        Robot.CMD.OnSend -= OnConnectionDashboard;
    }

    private void OnConnectionDashboard(string feed)
    {
        Debug.Log("Recieved Feed");
        Chat.SendLocalResponse("UR Dashboard", feed);
    }
}
