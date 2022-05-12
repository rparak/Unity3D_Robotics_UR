using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class TogglePopupBTN : MonoBehaviour
{
    public PopupItem popup;
    public InputActionReference inputAction;

    private void OnEnable() => inputAction.action.performed += TogglePopup;

    private void OnDisable() => inputAction.action.performed -= TogglePopup;

    public void TogglePopup(CallbackContext ctx) => popup.Toggle();
}
