using System;
using UnityEngine;

namespace Robot
{
    internal class Link2e : Links
    {
        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles = new Vector3(0, 0,-1 *( 90 + (float)(RobotPos.Current.jointRot[1] * (180.0 / Math.PI))));
        }

        protected override void Rotate(float ammount)
        {
            RobotPos newPose = RobotPos.Current;
            newPose.jointRot[1] += ammount * oneDegree;
            newPose.jointRot[1] = Step.ClosestStep((float)(newPose.jointRot[1] / oneDegree), 5) * oneDegree; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

