using Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UR3.Gripper
{
    public class Listener : MonoBehaviour
    {
        private GameObject Robot;
        
        // Start is called before the first frame update
        void Start()
        {
            Robot = GameObject.FindGameObjectWithTag("Robot");
        }

        // Update is called once per frame
        void Update()
        {
        }
    
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("COLLISION ENTER");
            Robot.GetComponent<InputController>().SetCollisionDetected(true);
            ur_data_processing.UR_Control_Data.shouldMove = false;
        }
        
        void OnCollisionExit(Collision collision)
        {
            Debug.Log("COLLISION LEAVE");
            Robot.GetComponent<InputController>().SetCollisionDetected(false);
        }
    }
}
