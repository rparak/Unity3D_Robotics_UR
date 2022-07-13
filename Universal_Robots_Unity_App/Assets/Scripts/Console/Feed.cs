using UnityEngine;

/// <summary>
/// Feed subscribes to other things and outputs them into the chat.
/// </summary>
public class Feed : MonoBehaviour
{
    private void OnEnable()
    {
        Robot.CMD.OnSend += OnConnectionDashboard;
        Robot.Connection.OnFeedback += OnConnectionFeedback;
    }

    private void OnDisable()
    {
        Robot.CMD.OnSend -= OnConnectionDashboard;
        Robot.Connection.OnFeedback -= OnConnectionFeedback;
    }

    private void OnConnectionFeedback(string feed)
    {
        Chat.SendLocalResponse("Cobot", feed);
    }

    private void OnConnectionDashboard(string feed)
    {
        Chat.SendLocalResponse("UR Dashboard", feed);
    }
}
