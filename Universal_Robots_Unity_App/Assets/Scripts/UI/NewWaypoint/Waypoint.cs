using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


[System.Serializable]
public class Waypoint
{
    [JsonIgnore] public static int maxId;
    [DefaultValue("")] public string name;
    public List<double> jointRot;


    #region Contructors

    public Waypoint(double[] jointRot)
    {
        name = $"Wegpunkt {++maxId}";
        this.jointRot = jointRot.ToList();
    }

    public Waypoint(string name, double[] jointRot)
    {
        this.name = name;
        this.jointRot = jointRot.ToList();
    }

    [JsonConstructor]
    public Waypoint() { }

    #endregion

    public void MoveTowards()
    {
        Pose pose = new Pose(jointRot);
        Robot.CMD.MoveJ(pose);
    }

    public async Task<bool> MoveTowardsAsync()
    {
        Pose pose = new Pose(jointRot);
        return await Robot.CMD.MoveJAsync(pose);
    }

    public string ToJson() => JsonConvert.SerializeObject(this);
}
