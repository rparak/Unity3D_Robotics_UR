using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamControl : MonoBehaviour
{
    public static CamControl Instance;

    public InputActionReference toggleCamAction;
    private int camActive;
    public List<GameObject> cameras;

    public void ActivateOrbital() => SetCamActive(0);
    public void ActivateFirstPersonCam() => SetCamActive(1);




    // Helpers
    private void SetCamActive(int camID)
    {
        cameras[camActive].SetActive(false);
        cameras[camID].SetActive(true);
        camActive = camID;
    }

    private void OnEnable()
    {
        Instance = this;
        toggleCamAction.action.performed += ToggleCam;
    }

    private void OnDisable() => toggleCamAction.action.performed -= ToggleCam;

    private void ToggleCam(InputAction.CallbackContext ctx) => NextCam();

    public void NextCam()
    {
        if (camActive == cameras.Count - 1) SetCamActive(0);
        else SetCamActive(camActive + 1);
    }
}
