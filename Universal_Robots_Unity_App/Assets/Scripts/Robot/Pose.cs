using System.Collections.Generic;
using UnityEngine;

public class Pose
{
    public string poseString;

    public Pose(Vector3 pos) => poseString = $"p[{pos.x},{pos.y},{pos.z},0,0,0]";
    public Pose(float x, float y, float z) => poseString = $"p[{x},{y},{z},0,0,0]";
    public Pose(Vector3 pos, Vector3 rot) => poseString = $"p[{pos.x},{pos.y},{pos.z},{rot.x},{rot.y},{rot.z}]";
    public Pose(float x, float y, float z, float rx, float ry, float rz)
    {
        //rx = rx / 0.0174532925199f;
        //ry = ry / 0.0174532925199f;
        //rz = rz / 0.0174532925199f;
        poseString = $"p[{x},{y},{z},{rx},{ry},{rz}]";
    }

    public Pose(Data d, bool useJointData = true)
    {
        if(useJointData) poseString = $"[{d.jointRot[0]},{d.jointRot[1]},{d.jointRot[2]},{d.jointRot[3]},{d.jointRot[4]},{d.jointRot[5]}]";
        else poseString = $"p[{d.position.x},{d.position.y},{d.position.z},{d.rotation.x},{d.rotation.y},{d.rotation.z}]";
    }

    public Pose(double[] joint) => poseString = $"[{joint[0]},{joint[1]},{joint[2]},{joint[3]},{joint[4]},{joint[5]}]";
    public Pose(List<double> joint) => poseString = $"[{joint[0]},{joint[1]},{joint[2]},{joint[3]},{joint[4]},{joint[5]}]";
}
