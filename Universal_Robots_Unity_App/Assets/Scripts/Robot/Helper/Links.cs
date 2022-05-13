using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

internal class Links : MonoBehaviour, ICamRaycastHit
{
    public static Links selected;

    public LinkIndikator visualIndikator;
    public MonoBehaviour outline;


    /// //////////////////////////////////// Selection Stuff


    public void Hit() => Select();

    private void Select()
    {
        if(selected == this)
        {
            DeSelect();
            selected = null;
            return;
        }

        if(selected != null) selected.DeSelect();

        selected = this;
        listenForModifiers = outline.enabled = true;
        visualIndikator.gameObject.SetActive(true);

        rotateAction.action.performed += KeyIsPressed;
        rotateAction.action.canceled += KeyIsPressed;
    }

    public void DeSelect()
    {
        listenForModifiers = outline.enabled = false;
        visualIndikator.gameObject.SetActive(false);

        rotateAction.action.performed -= KeyIsPressed;
        rotateAction.action.canceled -= KeyIsPressed;
    }


    /// ///////////////////////////////////// Modifiers

    [Space]
    public InputActionReference rotateAction;
    public float sensitivity = .1f;

    private bool listenForModifiers;
    private float startValue;
    private float newMouseOffset;


    private void KeyIsPressed(CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            visualIndikator.HideRequestedDegree();
            Rotate(Step.ClosestStep(-newMouseOffset, 5));
        }
        else
        {
            startValue = Mouse.current.position.ReadValue().x;
            visualIndikator.ShowRequestedDegree();
        }
        
    }

    private void Update()
    {
        if (!listenForModifiers) return;
        
        if (rotateAction.action.ReadValue<float>() < .3f) return;

        newMouseOffset = (startValue - Mouse.current.position.ReadValue().x) * sensitivity;
        visualIndikator.wantedDegrees = Step.ClosestStep(-newMouseOffset, 5);
    }

   

    protected virtual void Rotate(float ammount)
    {
        Debug.Log($"Request to turn by {ammount}");
    }
}
