using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[System.Serializable]
public class DualButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CustomButton button;
    private bool isListening = false;


    public void OnSelect(BaseEventData eventData) => Selected();
    public void OnDeselect(BaseEventData eventData) => DeSelected();
    public void OnPointerEnter(PointerEventData eventData) => Selected();
    public void OnPointerExit(PointerEventData eventData) => DeSelected();
    private void OnDisable() => DeSelected();


    // //////////////////////////

    private void Selected()
    {
        if (isListening) return;
        isListening = true;

        button.Subscribe();
    }

    private void DeSelected()
    {
        if (!isListening) return;

        button.UnSubscribe();

        isListening = false;
    }



    [System.Serializable]
    public class CustomButton
    {
        public InputActionReference action;
        public UnityEvent onClick;


        public void InvokeClick(InputAction.CallbackContext obj)
        {
            onClick.Invoke();
        }

        public void Subscribe()
        {
            action.action.performed += InvokeClick;
        }

        public void UnSubscribe()
        {
            action.action.performed -= InvokeClick;
        }
    }
}
