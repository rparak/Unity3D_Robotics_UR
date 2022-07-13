using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class JIWait : JIInstantiator.JIInstantiatorBase, IJIExecute
{
    public TMPro.TMP_InputField inputField;
    [Space]
    public Image image;
    public Color inActiveColor, activeColor;

    public void ChangeWaitTime(string seconds)
    {
        data.json = seconds;
    }

    public override void Write(Data data)
    {
        this.data = data;
        InsideFolder = !string.IsNullOrEmpty(data.parentGuid);
        if (string.IsNullOrEmpty(data.json)) data.json = "1";
        inputField.text = data.json;
    }

    protected override void Read()
    {
        data.name = "Wait";
        data.json = inputField.text;
    }

    public async Task<bool> Execute()
    {
        image.color = activeColor;

        float seconds = float.Parse(data.json);
        int milliseconds = Mathf.FloorToInt(seconds * 1000);
        bool success = await Robot.Connection.WaitAsync(milliseconds);

        image.color = inActiveColor;
        return success;
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(data.key))
        {
            data.key = Guid.NewGuid().ToString();
            InsideFolder = !string.IsNullOrEmpty(data.parentGuid);
        }
    }
}
