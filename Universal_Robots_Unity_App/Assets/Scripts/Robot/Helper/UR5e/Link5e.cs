using System;
using UnityEngine;

namespace Robot
{
    internal class Link5e : Links
    {
        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles = new Vector3(0.0f, -1 * ((float)(RobotPos.Current.jointRot[4] * (180.0 / Math.PI))), 0f);
        }

        protected override void Rotate(float amount)
        {
            RobotPos newPose = RobotPos.Current;
            newPose.jointRot[4] -= amount * oneDegree;
            newPose.jointRot[4] = Step.ClosestStep((float)(newPose.jointRot[4] / oneDegree), 5) * oneDegree; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}
