using Newtonsoft.Json;

public class JIText : JIInstantiator.JIInstantiatorBase
{
    public Value value;

    public TMPro.TMP_Text nameTxt, textTxt;


    public void NewValue()
    {
        value.text = textTxt.text;
        //transform.parent.GetComponent<JIInstantiator>().saveFile.Add(data.key, value.text);
    }

    protected override void Read()
    {
        data.name = "Text";
        data.json = value.ToJson();
    }

    public override void Write(Data data)
    {
        this.data = data;
        value = JsonConvert.DeserializeObject<Value>(data.json);
        nameTxt.text = value.name;
        textTxt.text = value.text;
    }

    public class Value
    {
        public string name;
        public string text;

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}
