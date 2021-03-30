// ------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------- LIBRARIES -------------------------------------------------------- //
// ------------------------------------------------------------------------------------------------------------------------ //

// -------------------- System -------------------- //
using System.Text;
// -------------------- Unity -------------------- //
using UnityEngine.EventSystems;
using UnityEngine;

public class button_check: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // -------------------- String -------------------- //
    public string acceleration = "0.15";
    public string time = "0.03";
    public string[] speed_param = new string[6] {"0.0", "0.0", "0.0", "0.0","0.0","0.0"};
    // -------------------- Int -------------------- //
    public int index;
    // -------------------- UTF8Encoding -------------------- //
    private UTF8Encoding utf8 = new UTF8Encoding();

    // -------------------- Button -> Pressed -------------------- //
    public void OnPointerDown(PointerEventData eventData)
    {
        // create auxiliary command string for speed control UR robot
        GlobalVariables_TCP_IP_client.aux_command_str = "speedl([" + speed_param[0] +","+  speed_param[1] + "," + speed_param[2]
                                                                   + "," + speed_param[3] + "," + speed_param[4] + "," + speed_param[5] + "], a =" + acceleration + ", t =" + time + ")" + "\n";
        // get bytes from command string
        GlobalVariables_TCP_IP_client.command = utf8.GetBytes(GlobalVariables_TCP_IP_client.aux_command_str);
        // confirmation variable -> is pressed
        GlobalVariables_TCP_IP_client.button_pressed[index] = true;
    }

    // -------------------- Button -> Un-Pressed -------------------- //
    public void OnPointerUp(PointerEventData eventData)
    {
        // confirmation variable -> is un-pressed
        GlobalVariables_TCP_IP_client.button_pressed[index] = false;
    }
}
