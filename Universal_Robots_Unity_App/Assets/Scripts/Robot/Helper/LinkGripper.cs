using UnityEngine;

internal class LinkGripper : MonoBehaviour, ICamRaycastHit
{
    public Transform leftGripper, rightGripper;
    

    public void Hit()
    {
        if (Robot.Gripper.Position < 10) Robot.CMD.Gripper.Close();
        else Robot.CMD.Gripper.Open();
    }

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
