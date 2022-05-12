using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


[System.Serializable]
public class Waypoint
{
    [JsonIgnore] public static int maxId;

    public string name;
    public string guid;
    public List<double> jointRot;
    [DefaultValue(false)] public bool gripperOpen;

    public Waypoint(double[] jointRot, bool gripperClosed = true)
    {
        name = $"Waypoint + {++maxId}";
        guid = Guid.NewGuid().ToString();

        this.jointRot = jointRot.ToList();
        this.gripperOpen = gripperClosed;
    }

    public Waypoint(string name, string guid, double[] jointRot, bool gripperOpen = true)
    {
        this.name = name;
        this.guid = guid;
        this.jointRot = jointRot.ToList();
        this.gripperOpen = gripperOpen;
    }

    [JsonConstructor]
    public Waypoint() { }


    public void Goto()
    {
        Pose pose = new Pose(jointRot);
        Robot.CMD.MoveJ(pose);
        if (gripperOpen) Robot.CMD.Gripper.Open();
        else Robot.CMD.Gripper.Close();
    }

    public async Task<bool> GotoAsync()
    {
        Pose pose = new Pose(jointRot);
        return await Robot.CMD.MoveJAsync(pose);
    }


    public enum Type
    {
        position,
        gripper
    }
}
