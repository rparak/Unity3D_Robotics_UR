using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Controller : MonoBehaviour
{
    
    public float speed = 0.05f;
    public float time = 0.05f;
    public float acceleration = 1;

    private Vector3 movement;
    private Vector3 rotation;

    [Space]
    public InputActionReference movementAction;
    public InputActionReference altiduteAction;
    public InputActionReference rotationAction;
    [Space]
    public InputActionReference gripperAction;
    public InputActionReference waypointAction;
    public InputActionReference playWaypointsAction;


    private void Update()
    {
        if (Robot.Connection.unityState == Robot.Connection.UnityState.offline) return;
        
        Movement();
        Altidute();
        Rotation();

        if (movement == Vector3.zero && rotation == Vector3.zero) return;

        movement =  Camera.main.transform.forward * movement.y + //Forward
                    Camera.main.transform.up * movement.x + //Right
                    Vector3.forward * movement.z;   //UP

        Robot.CMD.SpeedL(movement, rotation, acceleration, time);
        movement = rotation = Vector3.zero;
    }


    private void Movement()
    {
        Vector2 inputV = movementAction.action.ReadValue<Vector2>();

        movement.x += inputV.x * speed;
        movement.y += inputV.y * speed;
    }

    private void Altidute()
    {
        //Vector2 inputV = altiduteAction.action.ReadValue<Vector2>();
        float input = altiduteAction.action.ReadValue<float>();

        //movement.y += inputV.x * speed;
        //rotation.z += inputV.y * speed;
        movement.z += input * speed;
    }

    private void Rotation()
    {
        Vector2 inputV = rotationAction.action.ReadValue<Vector2>();

        rotation.x += inputV.x * speed;
        rotation.y += inputV.y * speed;
    }

    private void AddWaypoint(CallbackContext ctx)
    {
        WaypointMenu.Instance.AddWaypoint();
    }

    private void PlayAllWaypoints(CallbackContext ctx)
    {
        WaypointMenu.Instance.PlayAllWaypoints();
    }

    private void Gripper(CallbackContext ctx)
    {
        if (Robot.Gripper.Position < 10) Robot.CMD.Gripper.Close();
        else Robot.CMD.Gripper.Open();
    }

    // ///////////////////////////////////////////////// 

    private void OnEnable()
    {
        waypointAction.action.performed += AddWaypoint;
        playWaypointsAction.action.performed += PlayAllWaypoints;
        gripperAction.action.performed += Gripper;
    }

    private void OnDisable()
    {
        waypointAction.action.performed -= AddWaypoint;
        playWaypointsAction.action.performed -= PlayAllWaypoints;
        gripperAction.action.performed -= Gripper;
    }
}
