// ------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------- LIBRARIES -------------------------------------------------------- //
// ------------------------------------------------------------------------------------------------------------------------ //

// -------------------- System -------------------- //
using System.Text;
// -------------------- Unity -------------------- //
using UnityEngine;
using UnityEngine.InputSystem;
using UR3;

namespace Controls
{
    public class InputController : MonoBehaviour
    {
        public string acceleration = "1.0";
        public string time = "0.05";

        // -------------------- UTF8Encoding -------------------- //
        private UTF8Encoding utf8 = new UTF8Encoding();

        [Header("Input Settings")] 
        public PlayerInput playerInput;
        public float movementSmoothingSpeed = 1f;
        private Vector3 rawInputMovement;
        private Vector3 smoothInputMovement;
        
        //Update Loop - Used for calculating frame-based data
        void Update()
        {
        }

        public void moveUp()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.05,0.0,0.0,0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void moveDown()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,-0.05,0.0,0.0,0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }
        
        public void rotateLeftZ()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0,0.0,0.4], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void rotateRightZ()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0,0.0,-0.4], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        //INPUT SYSTEM ACTION METHODS --------------

        //This is called from PlayerInput; when a joystick or arrow keys has been pushed.

        public void OnTcpPositionMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);

            if (rawInputMovement == Vector3.zero)
            {
                Debug.Log("ZERO");
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }

            if (rawInputMovement.x != 0 || rawInputMovement.z != 0)
            {
                var x = rawInputMovement.x * 0.05;
                var z = rawInputMovement.z * 0.05;
                var x_formatted = x.ToString("0.00").Replace(",", ".");
                var z_formatted = z.ToString("0.00").Replace(",", ".");

                ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
                ur_data_processing.UR_Control_Data.aux_command_str = "speedl([" + x_formatted + "," + z_formatted +
                                                                     ",0.0,0.0,0.0,0.0], a =" + acceleration + ", t =" +
                                                                     time + ")" + "\n";
                ur_data_processing.UR_Control_Data.command =
                    utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
                Debug.Log("#INPUT: " + ur_data_processing.UR_Control_Data.aux_command_str);
            }
        }

        public void OnTcpOrientationMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);

            if (rawInputMovement == Vector3.zero)
            {
                Debug.Log("ZERO");
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }

            if (rawInputMovement.x != 0 || rawInputMovement.z != 0)
            {
                var x = rawInputMovement.x;
                var z = rawInputMovement.z;
                var x_formatted = x.ToString("0.00").Replace(",", ".");
                var z_formatted = z.ToString("0.00").Replace(",", ".");
                Debug.Log("x: " + x_formatted + "; z: " + z_formatted);

                ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
                ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0," + z_formatted + "," +
                                                                     x_formatted + ",0.0], a =" + acceleration +
                                                                     ", t =" +
                                                                     time + ")" + "\n";
                ur_data_processing.UR_Control_Data.command =
                    utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
                Debug.Log("#INPUT: " + ur_data_processing.UR_Control_Data.aux_command_str);
            }
        }

        public void OnTcpZPositionUp(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                moveUp();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
        }

        public void OnTcpZPositionDown(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                moveDown();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
        }
        
        public void OnTcpZRotationLeft(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                Debug.Log("Left shoulder performed");
                rotateLeftZ();
            }

            if (value.canceled)
            { 
                Debug.Log("Left shoulder canceld");
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
        }

        public void OnTcpZRotationRight(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                rotateRightZ();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
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