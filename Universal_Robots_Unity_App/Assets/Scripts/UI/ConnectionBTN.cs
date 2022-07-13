using Newtonsoft.Json;
using Treeka;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConnectionBTN : MonoBehaviour
{
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
            // Debug Helpers --------------------------------------------------
            if (grandOverrideKey.action.ReadValue<float>() > .3f)
            {
                Robot.Connection.host = "192.168.0.102";

                if (await Robot.Connection.Connect()) Chat.SendLocalResponse("Connection", "Connected to Grand Garage Robot");
                else Chat.SendLocalResponse("Connection", "Failed to connect to Grand Garage Robot");
                return;
            }
            if (localOverrideKey.action.ReadValue<float>() > .3f)
            {
                Robot.Connection.host = "127.0.0.1";

                if (await Robot.Connection.Connect()) Chat.SendLocalResponse("Connection", "Connected to Local Robot");
                else Chat.SendLocalResponse("Connection", "Failed to connect to Local Robot");
                return;
            }

            // Natural Connection ---------------------------------------------
            ConnectionData data = new ConnectionData();
            if (ReadWrite.Exists("Connection.gg")) data = ReadWrite.Read<ConnectionData>("Connection.gg");
            else ReadWrite.Write(data.ToJson(), "Connection.gg");

            if (data.isLocked)
            {
                Robot.Connection.host = data.hostname;
                if (await Robot.Connection.Connect()) Chat.SendLocalResponse("Connection", "Verbindung wurde mit dem Cobot hergestellt.");
                else Chat.SendLocalResponse("Connection", "Keine Verbindung konnte mit dem Cobot hergestellt werden.");
                return;
            }

            input.text = data.hostname;
        }

        connectionPanel.Toggle();
    }

    public void SetNewHostname()
    {
        ConnectionData data = ReadWrite.Read<ConnectionData>("Connection.gg");
        data.hostname = input.text;
        ReadWrite.Write(data.ToJson(), "Connection.gg");
    }

    public async void ConnectButton()
    {
        SetNewHostname();
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
            if (buttonImage == null) return;
            buttonImage.color = disconnectedColor;
        };
    }

    public class ConnectionData
    {
        public string hostname = "127.0.0.1";
        public bool isLocked;

        public static ConnectionData FromJson(string json) => JsonConvert.DeserializeObject<ConnectionData>(json);

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}
