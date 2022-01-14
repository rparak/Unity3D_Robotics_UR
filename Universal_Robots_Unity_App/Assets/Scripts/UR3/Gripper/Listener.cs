using Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UR3.Gripper
{
    public class Listener : MonoBehaviour
    {
        private GameObject Robot;
        [SerializeField] private GameObject _gripper;
        [SerializeField] private Transform _playground;

        // Start is called before the first frame update
        void Start()
        {
            Robot = GameObject.FindGameObjectWithTag("Robot");
        }

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log("COLLISION ENTER " + _gripper.GetComponent<GripperHandler>().IsClosed);
            Robot.GetComponent<InputController>().SetCollisionDetected(true);

            if (_gripper.GetComponent<GripperHandler>().IsClosed)
            {
                collider.attachedRigidbody.isKinematic = true;
                collider.transform.parent = _gripper.transform;
            }

        }
        
        void OnTriggerExit(Collider collider)
        {
            Debug.Log("COLLISION EXIT " + _gripper.GetComponent<GripperHandler>().IsClosed);
            Robot.GetComponent<InputController>().SetCollisionDetected(false);
            if (!_gripper.GetComponent<GripperHandler>().IsClosed)
            {
                collider.attachedRigidbody.isKinematic = false;
                collider.transform.parent = _playground;
            }
        }
    }
}
