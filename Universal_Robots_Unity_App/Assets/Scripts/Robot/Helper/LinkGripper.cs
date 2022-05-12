using UnityEngine;

internal class LinkGripper : MonoBehaviour
{
    public Transform leftGripper, rightGripper;

    private void Update()
    {
        //0 Open to 255 Closed
        float percentage = (float)Robot.Gripper.Position / 255;

        //0 == 0% && 255 == 100%
        //0 == 0% &&  25 == 100%

        float gripperPos = percentage * 25;

        leftGripper.transform.localPosition = new Vector3(0, gripperPos, 0);
        rightGripper.transform.localPosition = new Vector3(0, -gripperPos, 0);
    }


    
}
