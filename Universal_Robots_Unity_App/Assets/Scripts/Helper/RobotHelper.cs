using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Robot/ButtonScripts")]
public class RobotHelper : ScriptableObject
{
    public void MoveHome()
    {
        Robot.CMD.MoveJ(Data.Home.ToPose());
    }

    public void MoveZero() => Robot.CMD.MoveJ(Data.Zero.ToPose());

    public void EmergencyStop() => Robot.CMD.Stop();

    public void Test()
    {
        Pose pose = new Pose(0.5f, 0.1f, 0.0f, 2.221f, 2.221f, 0);
        Robot.CMD.MoveJ(pose);
        Chat.SendLocalResponse("Test", "MoveJ has been send");
    }

    public void Test2()
    {
        //Pose pose = new Pose(0.7f, 0.1f, .5f, 2.221f, 2.221f, 0);
        //Robot.CMD.MoveJ(pose);
        MoveHome();
        Chat.SendLocalResponse("Test", "Home has been send");
    }

    public void Test3()
    {
        Pose pose = new Pose(-0.7f, 0.1f, 0.5f, 2.221f, 2.221f, 0);
        Robot.CMD.MoveJ(pose);
    }
}