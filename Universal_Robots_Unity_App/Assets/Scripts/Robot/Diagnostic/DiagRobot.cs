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
            roboModes.text = Robot.mode.ToString();
            roboSafety.text = Robot.safety.ToString();

            switch (Connection.unityState)
            {
                case Connection.UnityState.emergencyStop: unityStateImage.color = ok; break;
                case Connection.UnityState.offline: unityStateImage.color = bad; break;
                case Connection.UnityState.online: unityStateImage.color = good; break;
            }

            switch (Robot.mode)
            {
                case Robot.RoboModes.confirmSafety:
                case Robot.RoboModes.noController:
                case Robot.RoboModes.powerOff:
                case Robot.RoboModes.disconnected: roboModeImage.color = bad; break;

                case Robot.RoboModes.booting:
                case Robot.RoboModes.backdrive:
                case Robot.RoboModes.idle:
                case Robot.RoboModes.updatingFirmware: roboModeImage.color = ok; break;

                default: roboModeImage.color = good; break;
            }

            switch (Robot.safety)
            {
                case Robot.RoboSafety.normal: roboSafetyImage.color = good; break;
                case Robot.RoboSafety.reduced: roboSafetyImage.color = ok; break;
                default: roboSafetyImage.color = bad; break;
            }

            if(Connection.unityState == Connection.UnityState.offline)
            {
                control.interactable = false;
                controlText.text = "";
            }
            else switch (Robot.mode)
            {
                case Robot.RoboModes.powerOff:
                    control.interactable = true;
                    controlText.text = "Power ON";
                    break;

                case Robot.RoboModes.idle:
                    control.interactable = true;
                    controlText.text = "Release Brakes";
                    break;

                case Robot.RoboModes.running:
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
            switch (Robot.mode)
            {
                case Robot.RoboModes.powerOff:
                    CMD.Control.PowerOn();
                    break;

                case Robot.RoboModes.idle:
                    CMD.Control.ReleaseBrake();
                    break;

                case Robot.RoboModes.running:
                    CMD.Control.PowerOff();
                    break;

                default:
                    break;
            }
        }
    }
}

