using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace Treeka
{
    public class Popup : MonoBehaviour
    {
        public static Popup Instance;
        public static Transform popupParent;
        public Image bgButton;

        public InputActionReference escapeAction;


        private void CallEscape(CallbackContext context) => CallEscape();

        public void CallEscape()
        {
            if (transform.childCount == 0) { } //PauseMenu.Instance.Enable();
            else transform.GetChild(transform.childCount - 1).GetComponent<PopupItem>().EscapePressed();
        }


        // //////////////////////////

        private void Awake() => popupParent = transform;

        private void OnEnable()
        {
            Instance = this;
            escapeAction.action.performed += CallEscape;
            PopupItem.OnPopupChange += PopupItem_OnPopupChange;
        }

        private void PopupItem_OnPopupChange()
        {
            //if (transform.childCount == 0) bgButton.enabled = false;
            //else bgButton.enabled = true;
        }

        private void OnDisable()
        {
            //if (InputTerminal.IsTerminalInactive) return;
            escapeAction.action.performed -= CallEscape;
            PopupItem.OnPopupChange -= PopupItem_OnPopupChange;
        }

    }
}


