using System.Collections.Generic;
using UnityEngine;

public class Pose
{
    public string poseString;

    public Pose(Vector3 pos) => poseString = $"p[{pos.x.ToString().Replace(",", ".")},{pos.y.ToString().Replace(",", ".")},{pos.z.ToString().Replace(",", ".")},0,0,0]";
    public Pose(float x, float y, float z) => poseString = $"p[{x.ToString().Replace(",", ".")},{y.ToString().Replace(",", ".")},{z.ToString().Replace(",", ".")},0,0,0]";
    public Pose(Vector3 pos, Vector3 rot) => poseString = $"p[{pos.x.ToString().Replace(",", ".")},{pos.y.ToString().Replace(",", ".")},{pos.z.ToString().Replace(",", ".")},{rot.x.ToString().Replace(",", ".")},{rot.y.ToString().Replace(",", ".")},{rot.z.ToString().Replace(",", ".")}]";
    public Pose(float x, float y, float z, float rx, float ry, float rz)
    {
        //rx = rx / 0.0174532925199f;
        //ry = ry / 0.0174532925199f;
        //rz = rz / 0.0174532925199f;
        poseString = $"p[{x.ToString().Replace(",", ".")},{y.ToString().Replace(",", ".")},{z.ToString().Replace(",", ".")},{rx.ToString().Replace(",", ".")},{ry.ToString().Replace(",", ".")},{rz.ToString().Replace(",", ".")}]";
    }

    public Pose(Robot.RobotPos d, bool useJointData = true)
    {
        if(useJointData) poseString = $"[{d.jointRot[0].ToString().Replace(",", ".")},{d.jointRot[1].ToString().Replace(",", ".")},{d.jointRot[2].ToString().Replace(",", ".")},{d.jointRot[3].ToString().Replace(",", ".")},{d.jointRot[4].ToString().Replace(",", ".")},{d.jointRot[5].ToString().Replace(",", ".")}]";
        else poseString = $"p[{d.position.x.ToString().Replace(",", ".")},{d.position.y.ToString().Replace(",", ".")},{d.position.z.ToString().Replace(",", ".")},{d.rotation.x.ToString().Replace(",", ".")},{d.rotation.y.ToString().Replace(",", ".")},{d.rotation.z.ToString().Replace(",", ".")}]";
    }

    public Pose(double[] joint) => poseString = $"[{joint[0].ToString().Replace(",", ".")},{joint[1].ToString().Replace(",", ".")},{joint[2].ToString().Replace(",", ".")},{joint[3].ToString().Replace(",", ".")},{joint[4].ToString().Replace(",", ".")},{joint[5].ToString().Replace(",", ".")}]";
    public Pose(List<double> joint) => poseString = $"[{joint[0].ToString().Replace(",", ".")},{joint[1].ToString().Replace(",", ".")},{joint[2].ToString().Replace(",", ".")},{joint[3].ToString().Replace(",", ".")},{joint[4].ToString().Replace(",", ".")},{joint[5].ToString().Replace(",", ".")}]";
}
