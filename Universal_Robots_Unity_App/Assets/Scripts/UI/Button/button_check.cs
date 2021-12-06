// ------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------- LIBRARIES -------------------------------------------------------- //
// ------------------------------------------------------------------------------------------------------------------------ //

// -------------------- System -------------------- //
using System.Text;
// -------------------- Unity -------------------- //
using UnityEngine.EventSystems;
using UnityEngine;
using UR3;

public class button_check: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // -------------------- String -------------------- //
    public string acceleration = "1.0";
    public string time = "0.05";
    public string[] speed_param      = new string[6] {"0.0", "0.0", "0.0", "0.0","0.0","0.0"};
    public string[] speed_param_null = new string[6] { "0.0", "0.0", "0.0", "0.0", "0.0", "0.0" };
    // -------------------- Int -------------------- //
    public int index;
    // -------------------- UTF8Encoding -------------------- //
    private UTF8Encoding utf8 = new UTF8Encoding();

    // -------------------- Button -> Pressed -------------------- //
    public void OnPointerDown(PointerEventData eventData)
    {
        // create auxiliary command string for speed control UR robot
        ur_data_processing.UR_Control_Data.aux_command_str = "speedl([" + speed_param[0] +","+  speed_param[1] + "," + speed_param[2]
                                                                   + "," + speed_param[3] + "," + speed_param[4] + "," + speed_param[5] + "], a =" + acceleration + ", t =" + time + ")" + "\n";
        Debug.Log(ur_data_processing.UR_Control_Data.aux_command_str);
        // get bytes from command string
        ur_data_processing.UR_Control_Data.command = utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
        // confirmation variable -> is pressed
        ur_data_processing.UR_Control_Data.button_pressed[index] = true;
    }

    // -------------------- Button -> Un-Pressed -------------------- //
    public void OnPointerUp(PointerEventData eventData)
    {
        // confirmation variable -> is un-pressed
        ur_data_processing.UR_Control_Data.button_pressed[index] = false;
    }
}
