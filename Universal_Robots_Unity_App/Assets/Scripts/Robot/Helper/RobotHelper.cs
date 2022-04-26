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

    public void ToggleGripper() => Robot.CMD.ToggleGripper();
}
