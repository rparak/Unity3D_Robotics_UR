// -------------------- System -------------------- //

using Controls;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UR3.Gripper; // -------------------- Unity -------------------- //

namespace UI.Button
{
    public class GripperButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject _robot;
        [SerializeField] private GameObject _gripper;
        public TextMeshProUGUI gripperStatusText;

        void Start()
        {
            // Assign necessary gameObjects in scene
            if (!_robot)
            {
                _robot = GameObject.FindGameObjectWithTag("Robot");
                if (!_gripper)
                {
                    _gripper = FindChildWithTag(_robot, "Gripper");
                }
            }
        }

        private static GameObject FindChildWithTag(GameObject parent, string tag)
        {
            GameObject child = null;

            foreach (Transform transform in parent.transform)
            {
                if (transform.gameObject.CompareTag(tag))
                {
                    return transform.gameObject;
                }
                else
                {
                    // search recursively
                    child = FindChildWithTag(transform.gameObject, tag);
                }
            }

            return child;
        }


        // -------------------- Button -> Pressed -------------------- //
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_gripper.GetComponent<GripperHandler>().IsClosed)
            {
                // Open gripper
                _robot.GetComponent<InputController>().GripperOpening = true;
                _robot.GetComponent<InputController>().GripperClosing = false;
                while (_gripper.GetComponent<GripperHandler>().IsClosed)
                {
                    _robot.GetComponent<InputController>().OpenGripper();
                }

                gripperStatusText.text = "Close";
            }
            else
            {
                // Close gripper
                _robot.GetComponent<InputController>().GripperOpening = false;
                _robot.GetComponent<InputController>().GripperClosing = true;
                while (!_gripper.GetComponent<GripperHandler>().IsClosed)
                {
                    _robot.GetComponent<InputController>().CloseGripper();
                }

                gripperStatusText.text = "Open";
            }
        }

        // -------------------- Button -> Un-Pressed -------------------- //
        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}