using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
    internal class DiagDigital : MonoBehaviour
    {
        public TMPro.TMP_Text outputRaw;
        public List<TMPro.TMP_Text> outputTexts;


        private void Update()
        {
            //outputRaw.text = Robot.Connection.digitalOutput.ToString();
        }
    }
}

