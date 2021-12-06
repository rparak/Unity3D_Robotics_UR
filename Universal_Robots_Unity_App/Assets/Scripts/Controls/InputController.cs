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

namespace Controls
{
    public class InputController : MonoBehaviour
    {
        public string acceleration = "0.1";
        public string time = "0.05";
        public string speed = "0.05";

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
            // ...
        }
        
        

        //INPUT SYSTEM ACTION METHODS --------------
        //This is called from PlayerInput; when a joystick or arrow keys has been pushed.

        public void OnTcpHorizontalMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);

            if (rawInputMovement == Vector3.zero)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }

            if (rawInputMovement.x != 0 || rawInputMovement.z != 0)
            {
                horizontalPosition(rawInputMovement.x, rawInputMovement.z);
            }
        }

        public void OnTcpVerticalMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);

            if (rawInputMovement == Vector3.zero)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }

            if (rawInputMovement.x != 0 || rawInputMovement.z != 0)
            {
                verticalPosition(rawInputMovement.x, rawInputMovement.z);
            }
        }

        public void OnOrientationForward(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                forwardOrientation();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
        }

        public void OnOrientationBackward(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                backwardOrientation();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
        }
        
        public void OnOrientationLeft(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                leftOrientation();
            }

            if (value.canceled)
            { 
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
        }

        public void OnOrientationRight(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                rightOrientation();
            }

            if (value.canceled)
            {
                ur_data_processing.UR_Control_Data.gamepadButtonPressed = false;
            }
        }
        
        
        
        
        //MOVEMENT METHODS --------------
        //Does moving the robot
        
        public void forwardOrientation()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,"+speed+",0.0,0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void backwardOrientation()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,-"+speed+",0.0,0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }
        
        public void leftOrientation()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0,"+speed+",0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }

        public void rightOrientation()
        {
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0,-"+speed+",0.0], a =" +
                                                                 acceleration + ", t =" + time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }
        
        public void horizontalPosition(double x, double y)
        {
            // Add speed factor and convert into string
            var xFormatted =  (x * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");
            var zFormatted = (y * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");
            
            // Prepare for UR api and send
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str = "speedl([" + xFormatted + "," + zFormatted +
                                                                 ",0.0,0.0,0.0,0.0], a =" + acceleration + ", t =" +
                                                                 time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        }
        
        public void verticalPosition(double rz, double z)
        {
            // Add speed factor and convert into string
            var rzFormatted = (rz * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");
            var zFormatted = (z * Convert.ToDouble(speed)).ToString("0.00").Replace(",", ".");
            
            // Prepare for UR api and send
            ur_data_processing.UR_Control_Data.gamepadButtonPressed = true;
            ur_data_processing.UR_Control_Data.aux_command_str =  "speedl([0.0,0.0," + zFormatted + ", 0.0,0.0, " +
                                                                  rzFormatted + "], a =" + acceleration +
                                                                  ", t =" +
                                                                  time + ")" + "\n";
            ur_data_processing.UR_Control_Data.command =
                utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
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