// ------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------- LIBRARIES -------------------------------------------------------- //
// ------------------------------------------------------------------------------------------------------------------------ //

// -------------------- System -------------------- //

using System;
using System.Text;
// -------------------- Unity -------------------- //
using UnityEngine;
using UnityEngine.InputSystem;
using UR3;
using UR3.Gripper;

namespace Controls
{
    public class InputController : MonoBehaviour
    {
        public string acceleration = "0.1";
        public string time = "0.05";
        public string speed = "0.05";

        [SerializeField] private GameObject _robot;
        [SerializeField] private GameObject _gripper;
        private GameObject _leftGripper;
        private GameObject _rightGripper;

        private int[] _cameraPositions { get; set; } = new int[] {0, 1, 2, 3, 4};
        private int _currentCameraPosition = 0;

        public bool collisionDetected;

        public bool GripperClosing { get; set; }
        public bool GripperOpening { get; set; }

        // -------------------- UTF8Encoding -------------------- //
        private UTF8Encoding utf8 = new UTF8Encoding();

        [Header("Input Settings")] public PlayerInput playerInput;
        private Vector3 rawInputMovement;

        void Start()
        {
            // Assign necessary gameObjects in scene
            if (!_robot)
            {
                _robot = GameObject.FindGameObjectWithTag("Robot");
                if (!_gripper)
                {
                    _gripper = FindChildWithTag(_robot, "Gripper");
                    _leftGripper = FindChildWithTag(_gripper, "LeftGripper");
                    _rightGripper = FindChildWithTag(_gripper, "RightGripper");
                }
            }
        }

        void Update()
        {
            if (collisionDetected)
            {
                //TODO stop all movement
                //ur_data_processing.UR_Control_Data.shouldMove = false;
            }

            if (GripperClosing && !collisionDetected)
            {
                CloseGripper();
            }

            if (GripperOpening)
            {
                OpenGripper();
            }
        }

        public void SetCollisionDetected(bool value)
        {
            Debug.Log("##### COLLISION #####   " + value);
            collisionDetected = value;
        }


        //----------- INPUT SYSTEM ACTION METHODS --------------
        //------- This is called from PlayerInput --------------
        //-- when a joystick or arrow keys has been pushed. ----

        public void OnTcpHorizontalMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);

            if (rawInputMovement == Vector3.zero)
            {
                ur_data_processing.UR_Control_Data.shouldMove = false;
            }

            if (rawInputMovement.x != 0 || rawInputMovement.z != 0)
            {
                HorizontalPosition(rawInputMovement.x, rawInputMovement.z);
            }
        }

        public void OnTcpVerticalMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);

            if (rawInputMovement == Vector3.zero)
            {
                ur_data_processing.UR_Control_Data.shouldMove = false;
            }

            if (rawInputMovement.x != 0 || rawInputMovement.z != 0)
            {
                VerticalPosition(rawInputMovement.x, rawInputMovement.z);
            }
        }

        public void OnOrientationForward(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                ForwardOrientation();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.shouldMove = false;
            }
        }

        public void OnOrientationBackward(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                BackwardOrientation();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.shouldMove = false;
            }
        }

        public void OnOrientationLeft(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                LeftOrientation();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.shouldMove = false;
            }
        }

        public void OnOrientationRight(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                RightOrientation();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.shouldMove = false;
            }
        }

        public void OnGripperClose(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                GripperClosing = true;
            }

            if (value.canceled)
            {
                GripperClosing = false;
            }
        }

        public void OnGripperOpen(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                GripperOpening = true;
            }

            if (value.canceled)
            {
                GripperOpening = false;
            }
        }

        public void OnAddWaypoint(InputAction.CallbackContext value)
        {
            Debug.Log("In waypoint function");
        }

        // -------------------- Camera Position -------------------- //
        public void OnSwitchCamera(InputAction.CallbackContext value)
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            GameObject firstPersonCamera = GameObject.FindGameObjectWithTag("FirstPersonCamera");
            int count = _cameraPositions.Length;
            //Debug.Log("Positions: " + count + "; current: " + _currentCameraPosition);

            if (value.canceled)
            {
                if (_currentCameraPosition == _cameraPositions.Length - 1)
                {
                    // Reset
                    _currentCameraPosition = 0;
                    firstPersonCamera.GetComponent<Camera>().enabled = false;
                    mainCamera.GetComponent<Camera>().enabled = true;
                }
                else
                {
                    _currentCameraPosition++;
                }

                switch (_currentCameraPosition)
                {
                    case 0:
                        // Right
                        mainCamera.transform.localPosition = new Vector3(0.114f, 2.64f, -2.564f);
                        mainCamera.transform.localEulerAngles = new Vector3(10f, -30f, 0f);
                        break;
                    case 1:
                        // Left
                        mainCamera.transform.localPosition = new Vector3(-3.114f, 2.64f, -2.564f);
                        mainCamera.transform.localEulerAngles = new Vector3(10f, 30f, 0f);
                        break;
                    case 2:
                        // Front
                        mainCamera.transform.localPosition = new Vector3(-1.5f, 2.2f, -3.5f);
                        mainCamera.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        break;
                    case 3:
                        // Top 
                        mainCamera.transform.localPosition = new Vector3(-1.2f, 4f, 0f);
                        mainCamera.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        break;
                    case 4:
                        // FirstPerson 
                        mainCamera.transform.localPosition = new Vector3(-1.2f, 4f, 0f);
                        mainCamera.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        firstPersonCamera.GetComponent<Camera>().enabled = true;
                        mainCamera.GetComponent<Camera>().enabled = false;
                        break;
                }
            }
        }

        private bool _leftGripperReachedOpenPosition()
        {
            return !(_leftGripper.transform.localPosition.y >= 0);
        }

        private bool _rightGripperReachedOpenPosition()
        {
            return !(_rightGripper.transform.localPosition.y <= 0);
        }

        private bool _leftGripperReachedClosePosition()
        {
            return !(_leftGripper.transform.localPosition.y <= 22);
        }

        private bool _rightGripperReachedClosePosition()
        {
            return !(_rightGripper.transform.localPosition.y >= -22);
        }


        // ------------ MOVEMENT METHODS --------------
        // ------------ Moves the robot  --------------
        public void OpenGripper()
        {
            if (!_leftGripperReachedOpenPosition())
            {
                _leftGripper.transform.Translate(Vector3.down * Time.deltaTime);
                _gripper.GetComponent<GripperHandler>().IsClosed = false;
            }

            if (!_rightGripperReachedOpenPosition())
            {
                _rightGripper.transform.Translate(Vector3.up * Time.deltaTime);
                _gripper.GetComponent<GripperHandler>().IsClosed = false;
            }

            collisionDetected = false;
        }

        public void CloseGripper()
        {
            if (!_leftGripperReachedClosePosition() && !_rightGripperReachedClosePosition())
            {
                _leftGripper.transform.Translate(Vector3.up * Time.deltaTime);
                _rightGripper.transform.Translate(Vector3.down * Time.deltaTime);
                _gripper.GetComponent<GripperHandler>().IsClosed = true;
            }
        }

        public void ForwardOrientation()
        {
            ur_data_processing.UR_Control_Data.shouldMove = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0," + speed + ",0.0,0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void BackwardOrientation()
        {
            ur_data_processing.UR_Control_Data.shouldMove = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,-" + speed + ",0.0,0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void LeftOrientation()
        {
            ur_data_processing.UR_Control_Data.shouldMove = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0," + speed + ",0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void RightOrientation()
        {
            ur_data_processing.UR_Control_Data.shouldMove = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0,-" + speed + ",0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void HorizontalPosition(double x, double y)
        {
            // Add speed factor and convert into string
            var xFormatted = (x * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");
            var zFormatted = (y * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");

            // Prepare for UR api and send
            ur_data_processing.UR_Control_Data.shouldMove = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([" + xFormatted + "," + zFormatted +
                                                                 ",0.0,0.0,0.0,0.0], a =" + acceleration + ", t =" +
                                                                 time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void VerticalPosition(double rz, double z)
        {
            // Add speed factor and convert into string
            var rzFormatted = (rz * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");
            var zFormatted = (z * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");

            // Prepare for UR api and send
            ur_data_processing.UR_Control_Data.shouldMove = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0," + zFormatted + ", 0.0,0.0, " +
                                                                 rzFormatted + "], a =" + acceleration +
                                                                 ", t =" +
                                                                 time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        private static GameObject FindChildWithTag(GameObject parent, string tag)
        {
            GameObject child = null;

            foreach (Transform transform in parent.transform)
            {
                if (transform.gameObject.CompareTag(tag))
                {
                    Debug.Log(tag + " found");
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

        public InputActionAsset GetActionAsset()
        {
            return playerInput.actions;
        }

        public PlayerInput GetPlayerInput()
        {
            return playerInput;
        }
    }
}