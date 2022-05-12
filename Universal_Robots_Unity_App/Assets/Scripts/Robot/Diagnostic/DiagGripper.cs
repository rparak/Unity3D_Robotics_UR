using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiagGripper : MonoBehaviour
{
    public TMP_Text positionText;
    [Space]
    public Button control;
    public TMP_Text controlText;


    public void Update()
    {
        

        if (Robot.Gripper.Position < 10)
        {
            positionText.text = Robot.Gripper.Position.ToString() + " (0-255) OPEN";
            controlText.text = "Close";
        }
        else
        {
            positionText.text = Robot.Gripper.Position.ToString() + " (0-255) CLOSE";
            controlText.text = "Open";
        }
    }


    public void OnControlBTNPress()
    {
        if(Robot.Gripper.Position < 10)
        {
            Robot.CMD.Gripper.Close();
        }
        else
        {
            Robot.CMD.Gripper.Open();
        }
    }
}
