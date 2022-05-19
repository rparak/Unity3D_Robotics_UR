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

        inputMapsActive.Add(inputAsset.FindActionMap("Interface"));
        inputMapsActive.Add(inputAsset.FindActionMap("Robot"));

        emergencyStopAction.action.performed += EmergencyStop;
    }

    private void OnDisable() => emergencyStopAction.action.performed -= EmergencyStop;

    private void EmergencyStop(InputAction.CallbackContext ctx) => Robot.CMD.Stop();



    public static CursorLockMode cursorMode = CursorLockMode.None;

    public static List<InputReg> registeredInput = new List<InputReg>(); //RegisterInputs to Disable everything except that
    public static List<InputActionMap> inputMapsActive = new List<InputActionMap>(); //Entire maps currently in use. Includes all but Special. (Special should never be disabled)

    public static void DisableInput(InputReg allowedInput)
    {
        foreach (InputActionMap inputMap in inputMapsActive) { inputMap.Disable(); }

        registeredInput.Add(allowedInput);
        allowedInput.InputReconfigure();

        Cursor.lockState = CursorLockMode.None;
    }

    public static void ReleaseInput(InputReg removeInput)
    {
        registeredInput.Remove(removeInput);
        if (registeredInput.Count == 0)
        {
            //All InputRegs are released. You may use input again.
            foreach (InputActionMap inputMap in inputMapsActive) { inputMap.Enable(); }

            SetCurser();
            return;
        }

        foreach (InputActionMap inputMap in inputMapsActive) { inputMap.Disable(); }
        registeredInput[registeredInput.Count - 1].InputReconfigure();
    }


    public static bool IsTerminalInactive { get { return playerInput == null; } }

    public static void SetCurser(CursorLockMode mode) => Cursor.lockState = cursorMode = mode;
    public static void SetCurser() => Cursor.lockState = cursorMode;
}
