using System;
using UnityEngine;

namespace Robot
{
    internal class Link3e : Links
    {
        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles = new Vector3(0, 0, -1 * ((float)(RobotPos.Current.jointRot[2] * (180.0 / Math.PI))));
        }

        protected override void Rotate(float ammount)
        {
            RobotPos newPose = RobotPos.Current;
            newPose.jointRot[2] += ammount * oneDegree;
            newPose.jointRot[2] = Step.ClosestStep((float)(newPose.jointRot[2] / oneDegree), 5) * oneDegree; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

