using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UR3;

public class GameController : MonoBehaviour
{
    
    public string acceleration = "0.1";
    public string time = "0.05";
    public string speed = "0.05";
    
    public Transform[] Waypoints;
    
    [SerializeField] private GameObject _robot;
    [SerializeField] private GameObject _gripper;
    private GameObject _leftGripper;
    private GameObject _rightGripper;
    
    public TextMeshProUGUI DemoStatusText;
    private UTF8Encoding utf8 = new UTF8Encoding();

    // -------------------- TextMeshProUGUI -------------------- //
    public string position_x, position_y, position_z;
    public string position_rx, position_ry, position_rz;
    public string position_j1, position_j2, position_j3;
    public string position_j4, position_j5, position_j6;
    
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

    void FixedUpdate()
    {
        // ------------------------ Get live coordinates from robot ------------------------ //
        // Position {Cartesian} -> X..Z
        position_x = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Position[0] * (1000f), 2)).ToString();
        position_y = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Position[1] * (1000f), 2)).ToString();
        position_z = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Position[2] * (1000f), 2)).ToString();
        // Position {Rotation} -> EulerAngles(RX..RZ)
        position_rx = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Orientation[0] * (180 / Math.PI), 2)).ToString();
        position_ry = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Orientation[1] * (180 / Math.PI), 2)).ToString();
        position_rz = ((float)Math.Round(ur_data_processing.UR_Stream_Data.C_Orientation[2] * (180 / Math.PI), 2)).ToString();
        // Position Joint -> 1 - 6
        position_j1 = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[0] * (180 / Math.PI), 2)).ToString();
        position_j2 = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[1] * (180 / Math.PI), 2)).ToString();
        position_j3 = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[2] * (180 / Math.PI), 2)).ToString();
        position_j4 = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[3] * (180 / Math.PI), 2)).ToString();
        position_j5 = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[4] * (180 / Math.PI), 2)).ToString();
        position_j6 = ((float)Math.Round(ur_data_processing.UR_Stream_Data.J_Orientation[5] * (180 / Math.PI), 2)).ToString();
    }

    public void FollowWaypoints()
    {
        StartCoroutine(MoveToWaypoints());
    }

    IEnumerator MoveToWaypoints()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        DemoStatusText.text = "Moving...";
        DemoStatusText.color = new Color(34, 139, 204, 255);
        ur_data_processing.UR_Control_Data.shouldMove = true;
        
        // TODO: Move until robo Coordiantes are equal with waypoints
        for (int i = 0; i < Waypoints.Length-1; i++)
        {
            // while (!EqualCoordinates())
            // {
            //     ur_data_processing.UR_Control_Data.aux_command_str = "speedl([0.0,0.0,0.0,0.0,0.0,0.0], " +
            //                                                          "a =" + acceleration + ", t =" + time + ")" + "\n";
            //     ur_data_processing.UR_Control_Data.command =
            //         utf8.GetBytes(ur_data_processing.UR_Control_Data.aux_command_str);
            // }
        }
        
        yield return new WaitForSeconds(5);
        ur_data_processing.UR_Control_Data.shouldMove = false;
        DemoStatusText.text = "Start";
        DemoStatusText.color = Color.white;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    
    private bool EqualCoordinates(float[] robotCoordinates)
    {
        return false;
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
}