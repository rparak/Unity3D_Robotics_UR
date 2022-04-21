using UnityEngine;

public class Pose
{
    public string poseString;

    public Pose(Vector3 pos) => poseString = $"p[{pos.x},{pos.y},{pos.z},0,0,0]";
    public Pose(float x, float y, float z) => poseString = $"p[{x},{y},{z},0,0,0]";
    public Pose(Vector3 pos, Vector3 rot) => poseString = $"p[{pos.x},{pos.y},{pos.z},{rot.x},{rot.y},{rot.z}]";
    public Pose(float x, float y, float z, float rx, float ry, float rz) => poseString = $"p[{x},{y},{z},{rx},{ry},{rz}]";
    public Pose(Data d, bool useJointData = true)
    {
        if(useJointData) poseString = $"[{d.jointRot[0]},{d.jointRot[1]},{d.jointRot[2]},{d.jointRot[3]},{d.jointRot[4]},{d.jointRot[5]}]";
        else poseString = $"p[{d.position.x},{d.position.y},{d.position.z},{d.rotation.x},{d.rotation.y},{d.rotation.z}]";
    }
}
