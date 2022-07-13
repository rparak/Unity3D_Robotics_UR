using System;
using UnityEngine;

namespace Robot
{
    internal class Link4e : Links
    {
        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles  = new Vector3(0.0f, 0.0f, -90 + ( -1 * ((float)(RobotPos.Current.jointRot[3] * (180.0 / Math.PI)))));
        }

        protected override void Rotate(float ammount)
        {
            RobotPos newPose = RobotPos.Current;
            newPose.jointRot[3] += ammount * oneDegree;
            newPose.jointRot[3] = Step.ClosestStep((float)(newPose.jointRot[3] / oneDegree), 5) * oneDegree; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

