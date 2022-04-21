using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTerminal : MonoBehaviour
{
    public static PlayerInput playerInput;
    public static InputActionAsset inputAsset;
    [SerializeField] private InputActionAsset _inputAsset;

    [Space]
    public InputActionReference emergencyStopAction;

    private void OnEnable()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        inputAsset = _inputAsset;
        inputAsset.FindActionMap("Special").Enable();
        inputAsset.FindActionMap("Interface").Enable();
        inputAsset.FindActionMap("Robot").Enable();

        emergencyStopAction.action.performed += EmergencyStop;
    }

    private void OnDisable() => emergencyStopAction.action.performed -= EmergencyStop;

    private void EmergencyStop(InputAction.CallbackContext ctx) => Robot.CMD.Stop();
}
