using UnityEngine;
using Command = CMD;


namespace Robot
{

    [CreateAssetMenu(menuName = "Robot/ButtonScripts")]
    public class RobotHelper : ScriptableObject
    {
        public void MoveHome()
        {
            CMD.MoveJ(RobotPos.Home.ToPose());
        }

        public void MoveZero() => CMD.MoveJ(RobotPos.Zero.ToPose());

        public void EmergencyStop() => CMD.Stop();

        public void Test()
        {
            Pose pose = new Pose(0.4f, 0.13f, 0.5f, 2.221f, 2.221f, 0);

            CMD.MoveJ(pose);
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
            CMD.MoveJ(pose);
        }


        [Command("Move")] //Feel free to delete
        public static void Move(string[] param)
        {
            if (param[0] == "get")
            {
                Chat.SendLocalResponse("To lazy to look in diag panel", RobotPos.Current.position.ToString());
                return;
            }

            Pose pose = new Pose(float.Parse(param[0]), float.Parse(param[1]), float.Parse(param[2]), 2.221f, 2.221f, 0);

            CMD.MoveJ(pose);
        }
    }
}