using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DisableKeyOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] InputActionReference actionRef;

    public void OnPointerEnter(PointerEventData eventData)
    {
        actionRef.action.Disable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        actionRef.action.Enable();
    }
}
