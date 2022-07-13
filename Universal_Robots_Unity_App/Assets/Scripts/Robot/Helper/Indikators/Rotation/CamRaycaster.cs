using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CamRaycaster : MonoBehaviour
{

    public InputActionReference shootAction;
    private const int robotLayer = 1 << 7;

    private void OnEnable() => shootAction.action.performed += Shoot;
    private void OnDisable() => shootAction.action.performed -= Shoot;

    private void Shoot(CallbackContext ctx)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out RaycastHit hit, 20f, robotLayer))
        {
            

            if(hit.collider.gameObject.TryGetComponent(out ICamRaycastHit foundObj))
            {
                foundObj.Hit();
            }
            else
            {
                /*if (Links.selected == null) return; Currently UI also calls this code
                Links.selected.DeSelect();
                Links.selected = null;*/
            }
        }
    }
}

public interface ICamRaycastHit
{
    public void Hit();
}
