using UnityEngine;
using CobyPos = Robot.RobotPos;



[CreateAssetMenu(menuName = "Robot/ButtonScripts")]
public class RobotHelper : ScriptableObject
{
    public Robot.RobotPos savedPos;

    public void MoveHome()
    {
        Robot.CMD.MoveJ(CobyPos.Home.ToPose());
    }

    public void MoveZero() => Robot.CMD.MoveJ(CobyPos.Zero.ToPose());

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
        savedPos.position = CobyPos.Current.position;
        savedPos.rotation = CobyPos.Current.rotation;
        savedPos.jointRot = CobyPos.Current.jointRot;
    }

    public void MoveSavedPosition()
    {
        Robot.CMD.MoveJ(savedPos.ToPose());
    }
}