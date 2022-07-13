using System;
using UnityEngine;

namespace Robot
{
    internal class LinkUniversal : Links
    {
        [SerializeField] int joint;
        [SerializeField] Vector3 multiplier;

        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles = multiplier * (float)(RobotPos.Current.jointRot[joint] * (180 / Math.PI));
        }

        protected override void Rotate(float amount)
        {
            RobotPos newPose = RobotPos.Current;
            newPose.jointRot[0] -= amount * (180 / Math.PI);
            newPose.jointRot[0] = Step.ClosestStep((float)(newPose.jointRot[0] / (180 / Math.PI)), 5) * (180 / Math.PI); //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

