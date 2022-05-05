using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Robot/ButtonScripts")]
public class RobotHelper : ScriptableObject
{
    public Data savedPos;

    public void MoveHome()
    {
        Robot.CMD.MoveJ(Data.Home.ToPose());
    }

    public void MoveZero() => Robot.CMD.MoveJ(Data.Zero.ToPose());

    public void SetAllAnalogActive()
    {
        for (int i = 0; i < 9; i++)
        {
            Robot.CMD.SetAnalogOutput(i, 1f);
        }
        
    }

    public void Test()
    {
        //Robot.CMD.Popup("Hi", "This should be closed");
        Robot.CMD.Control.PowerOn();
        
    }

    public void Test2()
    {
        //bot.CMD.Control.ClosePopup();
        Robot.CMD.Control.ReleaseBrake();
    }

    public void Test3()
    {
        //_ = Robot.CMD.Control.IsInRemoteControl();
        Robot.CMD.Control.PowerOff();
    }

    public void SavePosition()
    {
        savedPos.position = Data.Current.position;
        savedPos.rotation = Data.Current.rotation;
        savedPos.jointRot = Data.Current.jointRot;
    }

    public void MoveSavedPosition()
    {
        Robot.CMD.MoveJ(savedPos.ToPose());
    }
}
