using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class JIGripper : JIInstantiator.JIInstantiatorBase, IJIExecute
{
    public TMPro.TMP_Text nameTxt;
    [Space]
    public Image image;
    public Color inActiveColor, activeColor;

    public void ToggleGripperState()
    {
        if(data.json == "1")
        {
            data.json = "0";
            nameTxt.text = "Greifer schließen";
        }
        else
        {
            data.json = "1";
            nameTxt.text = "Greifer öffnen";
        }
    }

    public override void Write(Data data)
    {
        this.data = data;
        InsideFolder = !string.IsNullOrEmpty(data.parentGuid);
        nameTxt.text = data.json == "1" ? "Greifer öffnen" : "Greifer schließen";
    }

    protected override void Read()
    {
        data.name = "Gripper";
        data.json ??= "1";
    }

    public async Task<bool> Execute()
    {
        image.color = activeColor;

        ushort task = Robot.Connection.NewTask();
        if(data.json == "1") await Robot.CMD.Gripper.Open(); //Can we wait and tell Robot to wait?
        else await Robot.CMD.Gripper.Close();

        image.color = inActiveColor;
        return task == Robot.Connection.TaskID; //If no new Task came in then its all good.
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(data.key))
        {
            data.key = Guid.NewGuid().ToString();
            data.json = Robot.Gripper.Position > 10 ? "1" : "0";
            nameTxt.text = data.json == "1" ? "Greifer öffnen" : "Greifer schließen";
            InsideFolder = !string.IsNullOrEmpty(data.parentGuid);
        }
    }
}
