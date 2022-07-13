using Newtonsoft.Json;
using UnityEngine.UI;

public class JISlider : JIInstantiator.JIInstantiatorBase
{
    public TMPro.TMP_Text nameTxt, valueTxt;
    public float multiplier;

    public Slider slider;
    protected Value value = new Value();

    public void NewValue(float newValue)
    {
        value.value = newValue / value.multplication;
        valueTxt.text = newValue.ToString();
        //transform.parent.GetComponent<JIInstantiator>().saveFile.Add(data.key, newValue);
    }


    protected override void Read()
    {
        data.name = "Slider";
        value.name = nameTxt.text;

        value.value = slider.value / multiplier;
        value.multplication = multiplier;
        value.min = slider.minValue;
        value.max = slider.maxValue;
        value.wholeNumbers = slider.wholeNumbers;

        data.json = value.ToJson();
    }

    public override void Write(Data data)
    {
        this.data = data;
        value = JsonConvert.DeserializeObject<Value>(data.json);
        nameTxt.text = value.name;
        valueTxt.text = value.value.ToString();

        multiplier = value.multplication;
        slider.value = value.value * value.multplication;
        slider.maxValue = value.max;
        slider.minValue = value.min;
        slider.wholeNumbers = value.wholeNumbers;
    }


    public class Value
    {
        public string name;
        public float value;

        public float min, max, multplication;
        public bool wholeNumbers;

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}
