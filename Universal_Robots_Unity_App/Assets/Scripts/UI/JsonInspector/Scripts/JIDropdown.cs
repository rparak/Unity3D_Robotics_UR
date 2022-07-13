using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class JIDropdown : JIInstantiator.JIInstantiatorBase
{
    public TMPro.TMP_Text nameTxt, selectedTxt;
    public Value value = new Value();

    [SerializeField] private Transform dropDown;
    [SerializeField] private GameObject optionAsset;


    public void NewValue(string newValue)
    {
        selectedTxt.text = newValue;
        //transform.parent.GetComponent<JIInstantiator>().saveFile.Add(data.key, newValue);
    }


    protected override void Read()
    {
        data.name = "Dropdown";
        value.name = nameTxt.text;
        value.selected = selectedTxt.text;

        value.options = new List<string>();
        foreach(Transform child in dropDown)
        {
            value.options.Add(
                child.GetComponent<JIDropdownHelper>().text.text);
        }

        data.json = value.ToJson();
    }

    public override void Write(Data data)
    {
        this.data = data;
        value = JsonConvert.DeserializeObject<Value>(data.json);
        nameTxt.text = value.name;
        selectedTxt.text = value.selected;
        dropDown.DeleteAllChilds();

        foreach(string option in value.options)
        {
            Instantiate(optionAsset, dropDown)
                 .GetComponent<JIDropdownHelper>()
                 .text.text = option;
        }
    }

    public class Value
    {
        public string name, selected;
        public List<string> options = new List<string>();

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}
