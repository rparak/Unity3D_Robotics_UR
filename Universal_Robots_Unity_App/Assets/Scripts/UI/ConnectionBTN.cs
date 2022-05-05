using Treeka;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionBTN : MonoBehaviour
{

    public Image buttonImage;
    public PopupItem connectionPanel;
    public TMPro.TMP_InputField input;


    [Space]
    public Color connectedColor;
    public Color disconnectedColor;


    public void StartButton()
    {
        if (Robot.Connection.unityState != Robot.Connection.UnityState.offline)
        {
            Robot.Connection.Disconnect();
            SetActiveConnectionPanel(false);
        }
        else connectionPanel.Toggle();
    }

    public void ConnectButton()
    {
        SetActiveConnectionPanel(false);
        if (string.IsNullOrWhiteSpace(input.text)) _ = Robot.Connection.Connect("127.0.0.1", false);
        else _ = Robot.Connection.Connect(input.text, true);
    }

    public void ConnectGG()
    {
        SetActiveConnectionPanel(false);
        _ = Robot.Connection.Connect("192.168.0.102", true);
    }

    private void SetActiveConnectionPanel(bool state)
    {
        if (state) connectionPanel.Enable();
        else connectionPanel.Disable();
    }

    private void Start()
    {
        Robot.Connection.OnConnected += () =>{
            buttonImage.color = connectedColor;
        };

        Robot.Connection.OnDisconnected += () =>
        {
            buttonImage.color = disconnectedColor;
        };
    }
}
