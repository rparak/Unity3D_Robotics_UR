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
        if (string.IsNullOrWhiteSpace(input.text)) _ = Robot.Connection.Connect();
        else _ = Robot.Connection.Connect(input.text);
    }

    private void SetActiveConnectionPanel(bool state)
    {
        Debug.Log(state);
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
