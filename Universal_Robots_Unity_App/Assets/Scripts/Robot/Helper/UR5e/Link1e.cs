using System;
using UnityEngine;

namespace Robot
{
    internal class Link1e : Links
    {

        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles = new Vector3(0, (-1.0f) * (float)(RobotPos.Current.jointRot[0] * (180.0 / Math.PI)), 0);
        }

        protected override void Rotate(float ammount)
        {
            RobotPos newPose = RobotPos.Current;
            newPose.jointRot[0] -= ammount * oneDegree;
            newPose.jointRot[0] = Step.ClosestStep((float)(newPose.jointRot[0] / oneDegree), 5) * oneDegree; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

