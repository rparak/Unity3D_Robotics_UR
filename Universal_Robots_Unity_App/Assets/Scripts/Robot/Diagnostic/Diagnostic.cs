using UnityEngine;
using TMPro;

public class Diagnostic : MonoBehaviour
{
    public TMP_Text x;
    public TMP_Text y;
    public TMP_Text z;
    [Space]
    public TMP_Text rx;
    public TMP_Text ry;
    public TMP_Text rz;
    [Space]
    public TMP_Text j1;
    public TMP_Text j2;
    public TMP_Text j3;
    public TMP_Text j4;
    public TMP_Text j5;
    public TMP_Text j6;

    void Update()
    {
        x.text = Data.Current.position.x.ToString();
        y.text = Data.Current.position.y.ToString();
        z.text = Data.Current.position.z.ToString();

        rx.text = Data.Current.rotation.x.ToString();
        ry.text = Data.Current.rotation.y.ToString();
        rz.text = Data.Current.rotation.z.ToString();

        j1.text = Data.Current.jointRot[0].ToString();
        j2.text = Data.Current.jointRot[1].ToString();
        j3.text = Data.Current.jointRot[2].ToString();
        j4.text = Data.Current.jointRot[3].ToString();
        j5.text = Data.Current.jointRot[4].ToString();
        j6.text = Data.Current.jointRot[5].ToString();
    }
}
