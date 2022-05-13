using UnityEngine;
using UnityEngine.InputSystem;

public class CamFloating : MonoBehaviour
{
    public float sensetivity = 20f;
    public Vector2 clamp = new Vector2(-90, 90);
    private Vector2 rotation;

    [Space]
    public InputActionReference mouseAction;
    public InputActionReference mouseActivator;

    private Transform child;
    public Vector2 scrollClamp = new Vector2(-90, 90);
    public InputActionReference mouseScroll;
    private float scroll;


    public void Update()
    {
        Rotate();
        Scroll();
    }

    private void Rotate()
    {
        if(InputTerminal.playerInput.currentControlScheme == "Controller")
        {
            rotation += sensetivity * Time.deltaTime * mouseAction.action.ReadValue<Vector2>() * -2;
        }
        else if (mouseActivator.action.ReadValue<float>() < .3f) return;
        else
        {
            rotation += sensetivity * Time.deltaTime * mouseAction.action.ReadValue<Vector2>();
        }

        rotation.y = Mathf.Clamp(rotation.y, clamp.x, clamp.y);

        transform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotation.y, Vector3.left);
    }

    private void Scroll()
    {
        scroll += mouseScroll.action.ReadValue<Vector2>().y / 200;
        scroll = Mathf.Clamp(scroll, scrollClamp.x, scrollClamp.y);

        child.localPosition = new Vector3(child.localPosition.x, child.localPosition.y, scroll);
    }

    private void OnEnable()
    {
        child = transform.GetChild(0);
        scroll = child.localPosition.z;
        rotation.x = transform.rotation.eulerAngles.y;
        rotation.y = transform.rotation.eulerAngles.x;
    }
}
