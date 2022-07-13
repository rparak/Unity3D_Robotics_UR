using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class JIWaypoint : JIInstantiator.JIInstantiatorBase, IJIExecute
{
    public Waypoint waypoint;
    public TMPro.TMP_InputField nameTxt;
    [Space]
    public Image executeImage;
    public Color inActiveColor, activeColor;


    public override void Write(Data data)
    {
        this.data = data;
        waypoint = JsonConvert.DeserializeObject<Waypoint>(data.json);
        InsideFolder = !string.IsNullOrEmpty(data.parentGuid);
        nameTxt.text = waypoint.name;
    }

    public void SetWaypoint(Waypoint wp)
    {
        waypoint = wp;
        InsideFolder = !string.IsNullOrEmpty(data.parentGuid);
        nameTxt.text = waypoint.name;
    }

    protected override void Read()
    {
        data.name = "Waypoint";
        if (waypoint == null) return;

        waypoint.name = nameTxt.text;
        data.json = waypoint.ToJson();
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(data.key))
        {
            data.key = Guid.NewGuid().ToString();
            waypoint = new Waypoint(Robot.RobotPos.Current.jointRot);
            nameTxt.text = "Wegpunkt";
            InsideFolder = !string.IsNullOrEmpty(data.parentGuid);
        }
    }



    public async Task<bool> MoveToWaypoint()
    {
        if (waypoint == null) return true; //Maybe throw execption?

        executeImage.color = activeColor;
        bool success = await waypoint.MoveTowardsAsync();
        executeImage.color = inActiveColor;
        return success;
    }

    public void MoveToWP() => _ = MoveToWaypoint();



    public async Task<bool> Execute()
    {
        return await MoveToWaypoint();
    }
}
