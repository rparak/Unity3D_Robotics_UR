using System;
using UnityEngine;

namespace Robot
{
    public class Link5 : Links
    { 
        private void FixedUpdate()
        {
            if (Connection.unityState == Connection.UnityState.offline) return;
            transform.localEulerAngles = new Vector3(0.0f, (-1.0f) * (float)(Data.Current.jointRot[4] * (180.0 / Math.PI)), 0f);
        }

        protected override void Rotate(float ammount)
        {
            Data newPose = Data.Current;
            newPose.jointRot[4] -= ammount * 0.0174532925199;
            newPose.jointRot[4] = Step.ClosestStep((float)(newPose.jointRot[4] / 0.0174532925199), 5) * 0.0174532925199; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

