using Treeka;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConnectionBTN : MonoBehaviour
{
    public bool memberOnlyUse;
    [Space]
    public Image buttonImage;
    public PopupItem connectionPanel;
    public TMPro.TMP_InputField input;
    [Space]
    public InputActionReference localOverrideKey;
    public InputActionReference grandOverrideKey;


    [Space]
    public Color connectedColor;
    public Color disconnectedColor;


    public async void StartButton()
    {
        if (Robot.Connection.unityState != Robot.Connection.UnityState.offline)
        {
            Robot.Connection.Disconnect();
            SetActiveConnectionPanel(false);
            Chat.SendLocalResponse("Connection", "Disconnecting");
            return;
        }
        else
        {
            if(grandOverrideKey.action.ReadValue<float>() > .3f || memberOnlyUse)
            {
                Robot.Connection.host = "192.168.0.102";

                if (await Robot.Connection.Connect()) Chat.SendLocalResponse("Connection", "Connected to Grand Garage Robot");
                else Chat.SendLocalResponse("Connection", "Failed to connect to Grand Garage Robot");
                return;
            }
            if(localOverrideKey.action.ReadValue<float>() > .3f)
            {
                Robot.Connection.host = "127.0.0.1";

                if (await Robot.Connection.Connect()) Chat.SendLocalResponse("Connection", "Connected to Local Robot");
                else Chat.SendLocalResponse("Connection", "Failed to connect to Local Robot");
                return;
            }
        }

        connectionPanel.Toggle();
    }

    public async void ConnectButton()
    {
        SetActiveConnectionPanel(false);

        //Set Host
        if (string.IsNullOrWhiteSpace(input.text)) Robot.Connection.host = "127.0.0.1";
        else Robot.Connection.host = input.text;

        //Connect
        if (await Robot.Connection.Connect()) Chat.SendLocalResponse("Connection", $"Connected to {Robot.Connection.host}.");
        else Chat.SendLocalResponse("Connection", $"Failed to connect to {Robot.Connection.host}.");
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
