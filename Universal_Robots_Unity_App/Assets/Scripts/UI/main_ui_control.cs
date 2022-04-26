/****************************************************************************
MIT License
Copyright(c) 2020 Roman Parak
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*****************************************************************************
Author   : Roman Parak
Email    : Roman.Parak @outlook.com
Github   : https://github.com/rparak
File Name: main_ui_control.cs
****************************************************************************/

// System 
using System;
using System.Text;
// Unity
using UnityEngine;
using UnityEngine.UI;
// TM 
using TMPro;
using UR3;

public class main_ui_control : MonoBehaviour
{
    // -------------------- GameObject -------------------- //
    public GameObject camera_obj;
    // -------------------- Image -------------------- //
    public Image connection_panel_img, diagnostic_panel_img, joystick_panel_img;
    public Image connection_info_img;
    // -------------------- TMP_InputField -------------------- //
    public TMP_InputField ip_address_txt;
    // -------------------- Float -------------------- //
    private float ex_param = 100f;
    // -------------------- TextMeshProUGUI -------------------- //
    public TextMeshProUGUI position_x_txt, position_y_txt, position_z_txt;
    public TextMeshProUGUI position_rx_txt, position_ry_txt, position_rz_txt;
    public TextMeshProUGUI position_j1_txt, position_j2_txt, position_j3_txt;
    public TextMeshProUGUI position_j4_txt, position_j5_txt, position_j6_txt;
    public TextMeshProUGUI connectionInfo_txt;
    // -------------------- UTF8Encoding -------------------- //
    private UTF8Encoding utf8 = new UTF8Encoding();

    // ------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------ INITIALIZATION {START} ------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------ //
    void Start()
    {
        // Connection information {image} -> Connect/Disconnect
        connection_info_img.GetComponent<Image>().color = new Color32(255, 0, 48, 50);
        // Connection information {text} -> Connect/Disconnect
        connectionInfo_txt.text = "Disconnect";

        // Panel Initialization -> Connection/Diagnostic/Joystick Panel
        //connection_panel_img.transform.localPosition = new Vector3(1215f + (ex_param), 0f, 0f);
        //diagnostic_panel_img.transform.localPosition = new Vector3(780f + (ex_param), 0f, 0f);
        //joystick_panel_img.transform.localPosition = new Vector3(1550f + (ex_param), 0f, 0f);

        // Position {Cartesian} -> X..Z
        position_x_txt.text = "0.00";
        position_y_txt.text = "0.00";
        position_z_txt.text = "0.00";
        // Position {Rotation} -> EulerAngles(RX..RZ)
        position_rx_txt.text = "0.00";
        position_ry_txt.text = "0.00";
        position_rz_txt.text = "0.00";
        // Position Joint -> 1 - 6
        position_j1_txt.text = "0.00";
        position_j2_txt.text = "0.00";
        position_j3_txt.text = "0.00";
        position_j4_txt.text = "0.00";
        position_j5_txt.text = "0.00";
        position_j6_txt.text = "0.00";

        // Robot IP Address
        ip_address_txt.text = "127.0.0.1";

        // Auxiliary first command -> Write initialization position/rotation with acceleration/time to the robot controller
        // command (string value)
        ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0,0.0,0.0], a = 0.15, t = 0.03)" + "\n";
        // get bytes from string command
        ur_data_processing.UR_Control_Data.command = utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
    }

    // ------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------ MAIN FUNCTION {Cyclic} ------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------ //
    void FixedUpdate()
    {
        // Robot IP Address (Read) -> TCP/IP 
        ur_data_processing.UR_Stream_Data.ip_address = ip_address_txt.text;
        // Robot IP Address (Write) -> TCP/IP 
        ur_data_processing.UR_Control_Data.ip_address = ip_address_txt.text;

        // ------------------------ Connection Information ------------------------//
        // If the button (connect/disconnect) is pressed, change the color and text
        if (ur_data_processing.GlobalVariables_Main_Control.connect == true)
        {
            // green color
            connection_info_img.GetComponent<Image>().color = new Color32(135, 255, 0, 50);
            connectionInfo_txt.text = "Connect";
        }
        else if(ur_data_processing.GlobalVariables_Main_Control.disconnect == true)
        {
            // red color
            connection_info_img.GetComponent<Image>().color = new Color32(255, 0, 48, 50);
            connectionInfo_txt.text = "Disconnect";
        }

        // ------------------------ Cyclic read parameters {diagnostic panel} ------------------------ //
        // Position {Cartesian} -> X..Z
        position_x_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Position[0] * (1000f), 2)).ToString();
        position_y_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Position[1] * (1000f), 2)).ToString();
        position_z_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Position[2] * (1000f), 2)).ToString();
        // Position {Rotation} -> EulerAngles(RX..RZ)
        position_rx_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Orientation[0] * (180 / Math.PI), 2)).ToString();
        position_ry_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Orientation[1] * (180 / Math.PI), 2)).ToString();
        position_rz_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Orientation[2] * (180 / Math.PI), 2)).ToString();
        // Position Joint -> 1 - 6
        position_j1_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[0] * (180 / Math.PI), 2)).ToString();
        position_j2_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[1] * (180 / Math.PI), 2)).ToString();
        position_j3_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[2] * (180 / Math.PI), 2)).ToString();
        position_j4_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[3] * (180 / Math.PI), 2)).ToString();
        position_j5_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[4] * (180 / Math.PI), 2)).ToString();
        position_j6_txt.text = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[5] * (180 / Math.PI), 2)).ToString();
    }

    // ------------------------------------------------------------------------------------------------------------------------//
    // -------------------------------------------------------- FUNCTIONS -----------------------------------------------------//
    // ------------------------------------------------------------------------------------------------------------------------//

    // -------------------- Destroy Blocks -------------------- //
    void OnApplicationQuit()
    {
        // Destroy all
        Destroy(this);
    }

    // -------------------- Connection Panel -> Visible On -------------------- //
    public void TaskOnClick_ConnectionBTN()
    {
        // visible on
        connection_panel_img.transform.localPosition = new Vector3(0f, 0f, 0f);
        // visible off
        diagnostic_panel_img.transform.localPosition = new Vector3(780f + (ex_param), 0f, 0f);
        joystick_panel_img.transform.localPosition = new Vector3(1550f + (ex_param), 0f, 0f);
    }

    // -------------------- Connection Panel -> Visible off -------------------- //
    public void TaskOnClick_EndConnectionBTN()
    {
        connection_panel_img.transform.localPosition = new Vector3(1215f + (ex_param), 0f, 0f);
    }

    // -------------------- Diagnostic Panel -> Visible On -------------------- //
    public void TaskOnClick_DiagnosticBTN()
    {
        // visible on
        diagnostic_panel_img.transform.localPosition = new Vector3(0f, 0f, 0f);
        // visible off
        connection_panel_img.transform.localPosition = new Vector3(1215f + (ex_param), 0f, 0f);
        joystick_panel_img.transform.localPosition = new Vector3(1550f + (ex_param), 0f, 0f);
    }

    // -------------------- Diagnostic Panel -> Visible Off -------------------- //
    public void TaskOnClick_EndDiagnosticBTN()
    {
        diagnostic_panel_img.transform.localPosition = new Vector3(780f + (ex_param), 0f, 0f);
    }

    // -------------------- Joystick Panel -> Visible On -------------------- //
    public void TaskOnClick_JoystickBTN()
    {
        // visible on
        joystick_panel_img.transform.localPosition = new Vector3(-265f, -129f, 0f);
        // visible off
        connection_panel_img.transform.localPosition = new Vector3(1215f + (ex_param), 0f, 0f);
        diagnostic_panel_img.transform.localPosition = new Vector3(780f + (ex_param), 0f, 0f);
    }

    // -------------------- Joystick Panel -> Visible Off -------------------- //
    public void TaskOnClick_EndJoystickBTN()
    {
        joystick_panel_img.transform.localPosition = new Vector3(1550f + (ex_param), 0f, 0f);
    }

    // -------------------- Camera Position -> Right -------------------- //
    public void TaskOnClick_CamViewRBTN()
    {
        camera_obj.transform.localPosition    = new Vector3(0.114f, 2.64f, -2.564f);
        camera_obj.transform.localEulerAngles = new Vector3(10f, -30f, 0f);
    }

    // -------------------- Camera Position -> Left -------------------- //
    public void TaskOnClick_CamViewLBTN()
    {
        camera_obj.transform.localPosition = new Vector3(-3.114f, 2.64f, -2.564f);
        camera_obj.transform.localEulerAngles = new Vector3(10f, 30f, 0f);
    }

    // -------------------- Camera Position -> Home (in front) -------------------- //
    public void TaskOnClick_CamViewHBTN()
    {
        camera_obj.transform.localPosition = new Vector3(-1.5f, 2.2f, -3.5f);
        camera_obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    // -------------------- Camera Position -> Top -------------------- //
    public void TaskOnClick_CamViewTBTN()
    {
        camera_obj.transform.localPosition = new Vector3(-1.2f, 4f, 0f);
        camera_obj.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
    }

    // -------------------- Connect Button -> is pressed -------------------- //
    public void TaskOnClick_ConnectBTN()
    {
        ur_data_processing.GlobalVariables_Main_Control.connect    = true;
        ur_data_processing.GlobalVariables_Main_Control.disconnect = false;
    }

    // -------------------- Disconnect Button -> is pressed -------------------- //
    public void TaskOnClick_DisconnectBTN()
    {
        ur_data_processing.GlobalVariables_Main_Control.connect    = false;
        ur_data_processing.GlobalVariables_Main_Control.disconnect = true;
    }

}
