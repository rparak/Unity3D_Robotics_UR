using UnityEngine;
using UnityEngine.UI;

namespace Robot
{
    public class LinkIndikator : MonoBehaviour
    {
        public int jointID;

        public Image indikatorImageReal;
        public Image indikatorImageFake;
        public TMPro.TMP_Text indikatorText;

        public bool moveAgainst;

        public bool invertWantedDegreesFull;
        public bool invertWantedDegrees;
        public float wantedDegrees;


        private void Update()
        {
            double rot = RobotPos.Current.jointRot[jointID];
            float rotDeg = (float)(rot / Links.oneDegree);

            Real();
            if (indikatorImageFake.enabled) Config();



            void Real()
            {
                indikatorImageReal.fillClockwise = rotDeg > 0;
                indikatorText.text = $"{rotDeg:0.##}° \n[R] Rotate";

                if (rotDeg < 0) indikatorImageReal.fillAmount = -rotDeg / 360;
                else indikatorImageReal.fillAmount = rotDeg / 360;

                if (moveAgainst) indikatorImageReal.transform.rotation = Quaternion.Euler(0, 0, -rotDeg);
            }

            void Config()
            {
                if (invertWantedDegreesFull) wantedDegrees *= -1;
                indikatorImageFake.fillClockwise = wantedDegrees > 0;
                if (invertWantedDegrees) indikatorText.text = $"{rotDeg:0.##}° + {-wantedDegrees:0.##}°";
                else indikatorText.text = $"{rotDeg:0.##}° + {wantedDegrees:0.##}° \n[R] Rotate";

                if (wantedDegrees < 0) wantedDegrees *= -1;

                indikatorImageFake.fillAmount = wantedDegrees / 360;

                if (moveAgainst) indikatorImageReal.transform.rotation = Quaternion.Euler(0, 0, -rotDeg);
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
}

