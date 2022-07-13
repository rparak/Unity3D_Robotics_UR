public class JIHeader : JIInstantiator.JIInstantiatorBase
{
    public TMPro.TMP_Text header;

    protected override void Read()
    {
        data.name = "Header";
        data.json = header.text;
    }

    public override void Write(Data data)
    {
        name = data.name;
        this.data = data;
        header.text = data.json;
    }


    public void Save()
    {
        NewWaypointMenu.Instance.Save();
    }

    public void Load()
    {
        NewWaypointMenu.Instance.Load();
    }

    public void New()
    {
        NewWaypointMenu.Instance.New();
    }
}