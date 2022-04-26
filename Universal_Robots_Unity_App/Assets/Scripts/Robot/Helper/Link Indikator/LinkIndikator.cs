using UnityEngine;
using UnityEngine.UI;

public class LinkIndikator : MonoBehaviour
{
    public int jointID;

    public Image indikatorImageReal;
    public Image indikatorImageFake;
    public TMPro.TMP_Text indikatorText;

    public bool invertWantedDegreesFull;
    public bool invertWantedDegrees;
    public float wantedDegrees;


    private void Update()
    {
        double rot = Data.Current.jointRot[jointID];
        float rotDeg = (float)(rot / 0.0174532925199);

        Real();
        if(indikatorImageFake.enabled) Config();



        void Real()
        {
            indikatorImageReal.fillClockwise = rotDeg > 0;
            indikatorText.text = $"{rotDeg:0.##}°";

            if (rotDeg < 0) indikatorImageReal.fillAmount = -rotDeg / 360;
            else indikatorImageReal.fillAmount = rotDeg / 360;
        }

        void Config()
        {
            if (invertWantedDegreesFull) wantedDegrees *= -1;
            indikatorImageFake.fillClockwise = wantedDegrees > 0;
            if(invertWantedDegrees) indikatorText.text = $"{rotDeg:0.##}° + {-wantedDegrees:0.##}°";
            else indikatorText.text = $"{rotDeg:0.##}° + {wantedDegrees:0.##}°";

            if (wantedDegrees < 0) wantedDegrees *= -1;

            indikatorImageFake.fillAmount = wantedDegrees / 360;
        }
    }

    public void ShowRequestedDegree()
    {
        indikatorImageFake.enabled = true;
    }

    public void HideRequestedDegree()
    {
        indikatorImageFake.enabled = false;
    }
}
