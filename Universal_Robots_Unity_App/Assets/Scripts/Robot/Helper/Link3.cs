using System;
using UnityEngine;

namespace Robot
{
    public class Link3 : Links
    {
        private void FixedUpdate()
        {
            if (!Connection.IsActive) return;
            transform.localEulerAngles = new Vector3(0, 0, (float)(Data.Current.jointRot[2] * (180.0 / Math.PI)));
        }

        protected override void Rotate(float ammount)
        {
            Data newPose = Data.Current;
            newPose.jointRot[2] += ammount * 0.0174532925199;
            newPose.jointRot[2] = Step.ClosestStep((float)(newPose.jointRot[2] / 0.0174532925199), 5) * 0.0174532925199; //Forces Steps to be 5
            CMD.MoveJ(newPose.ToPose());
        }
    }
}

