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

    public void Test()
    {
        Pose pose = new Pose(0.5f, 0.1f, 0.0f, 2.221f, 2.221f, 0);
        Robot.CMD.MoveJ(pose);
    }

    public void Test2()
    {
        Pose pose = new Pose(0.7f, 0.1f, .5f, 2.221f, 2.221f, 0);
        Robot.CMD.MoveJ(pose);
    }

    public void Test3()
    {
        Pose pose = new Pose(-0.7f, 0.1f, 0.5f, 2.221f, 2.221f, 0);
        Robot.CMD.MoveJ(pose);
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