using System;
using UnityEngine;

namespace Robot
{
    internal class Link2 : Links
    {
        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles = new Vector3(0, 0, 90 + (float)(RobotPos.Current.jointRot[1] * (180.0 / Math.PI)));
        }

        protected override void Rotate(float ammount)
        {
            RobotPos newPose = RobotPos.Current;
            newPose.jointRot[1] += ammount * 0.0174532925199;
            newPose.jointRot[1] = Step.ClosestStep((float)(newPose.jointRot[1] / 0.0174532925199), 5) * 0.0174532925199; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

