using Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UR3.Gripper
{
    public class Listener : MonoBehaviour
    {
        private GameObject Robot;
        [SerializeField] private GameObject gripper;
        [SerializeField] private Transform playground;

        // Start is called before the first frame update
        void Start()
        {
            Robot = GameObject.FindGameObjectWithTag("Robot");
        }

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log("COLLISION ENTER");
            Robot.GetComponent<InputController>().SetCollisionDetected(true);
            ur_data_processing.UR_Control_Data.shouldMove = false;
            collider.attachedRigidbody.isKinematic = true;
            collider.transform.parent = gripper.transform;
            
        }
        
        void OnTriggerExit(Collider collider)
        {
            Debug.Log("COLLISION LEAVE");
            Robot.GetComponent<InputController>().SetCollisionDetected(false);
            collider.attachedRigidbody.isKinematic = false;
            collider.transform.parent = playground;
        }
    }
}
