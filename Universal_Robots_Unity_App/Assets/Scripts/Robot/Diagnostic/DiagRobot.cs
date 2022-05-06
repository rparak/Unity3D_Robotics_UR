using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Robot
{
    internal class DiagRobot : MonoBehaviour
    {
        public TMP_Text unityState, roboModes, roboSafety;
        public Image unityStateImage, roboModeImage, roboSafetyImage;

        public Button control;
        public TMP_Text controlText;
        [Space]
        public Color good, ok, bad;

        private void Update()
        {
            unityState.text = Connection.unityState.ToString();
            roboModes.text = Connection.roboModes.ToString();
            roboSafety.text = Connection.roboSafety.ToString();

            switch (Connection.unityState)
            {
                case Connection.UnityState.emergencyStop: unityStateImage.color = ok; break;
                case Connection.UnityState.offline: unityStateImage.color = bad; break;
                case Connection.UnityState.online: unityStateImage.color = good; break;
            }

            switch (Connection.roboModes)
            {
                case Connection.RoboModes.confirmSafety:
                case Connection.RoboModes.noController:
                case Connection.RoboModes.powerOff:
                case Connection.RoboModes.disconnected: roboModeImage.color = bad; break;

                case Connection.RoboModes.booting:
                case Connection.RoboModes.backdrive:
                case Connection.RoboModes.idle:
                case Connection.RoboModes.updatingFirmware: roboModeImage.color = ok; break;

                default: roboModeImage.color = good; break;
            }

            switch (Connection.roboSafety)
            {
                case Connection.RoboSafety.normal: roboSafetyImage.color = good; break;
                case Connection.RoboSafety.reduced: roboSafetyImage.color = ok; break;
                default: roboSafetyImage.color = bad; break;
            }

            if(Connection.unityState == Connection.UnityState.offline)
            {
                control.interactable = false;
                controlText.text = "";
            }
            else switch (Connection.roboModes)
            {
                case Connection.RoboModes.powerOff:
                    control.interactable = true;
                    controlText.text = "Power ON";
                    break;

                case Connection.RoboModes.idle:
                    control.interactable = true;
                    controlText.text = "Release Brakes";
                    break;

                case Connection.RoboModes.running:
                    control.interactable = true;
                    controlText.text = "Power OFF";
                    break;

                default:
                    control.interactable = false;
                    controlText.text = "";
                    break;
            }
        }

        public void OnControlBTNPress()
        {
            switch (Connection.roboModes)
            {
                case Connection.RoboModes.powerOff:
                    Robot.CMD.Control.PowerOn();
                    break;

                case Connection.RoboModes.idle:
                    Robot.CMD.Control.ReleaseBrake();
                    break;

                case Connection.RoboModes.running:
                    Robot.CMD.Control.PowerOff();
                    break;

                default:
                    break;
            }
        }
    }
}

