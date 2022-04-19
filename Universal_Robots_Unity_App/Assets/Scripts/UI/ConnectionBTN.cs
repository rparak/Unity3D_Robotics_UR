using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionBTN : MonoBehaviour
{

    public Image buttonImage;
    public GameObject connectionPanel;
    public TMPro.TMP_InputField input;

    [Space]
    public Color connectedColor;
    public Color disconnectedColor;


    public void StartButton()
    {
        if (Robot.Connection.IsActive)
        {
            Robot.Connection.Disconnect();
            SetActiveConnectionPanel(false);
        }
        else SetActiveConnectionPanel(connectionPanel.transform.localScale.y != 1); //Toggle ConnectionPanel
    }

    public void ConnectButton()
    {
        SetActiveConnectionPanel(false);
        if (string.IsNullOrWhiteSpace(input.text)) _ = Robot.Connection.Connect();
        else _ = Robot.Connection.Connect(input.text);
    }

    private void SetActiveConnectionPanel(bool state)
    {
        if (state) connectionPanel.transform.LeanScaleY(1, .1f);
        else connectionPanel.transform.LeanScaleY(0, .1f);
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
