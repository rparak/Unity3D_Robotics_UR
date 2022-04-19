using UnityEngine;

public class FaceCam : MonoBehaviour
{
    public bool invert;

    public void LateUpdate()
    {
        if (invert)
        {

            Vector3 awayDirection =  transform.position - Camera.main.transform.position;
            Quaternion awayRotation = Quaternion.LookRotation(awayDirection);
            transform.rotation = awayRotation;
        }


        else transform.LookAt(Camera.main.transform);
    }
}
