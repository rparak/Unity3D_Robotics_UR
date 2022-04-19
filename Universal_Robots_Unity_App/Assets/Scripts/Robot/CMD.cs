using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UR3;

namespace Robot
{
    public static class CMD
    {
        private static UTF8Encoding utf8 = new UTF8Encoding();


        public static void Stop() => Connection.Send("stopl(20)\n");


        /// <summary>
        /// Moves in a specific direction.
        /// </summary>
        public static void SpeedL(Vector3 dir, Vector3 rotDir, float acceleration = 1.4f, float time = .05f)
        {
            Connection.Send($"speedl(" +
                $"[{dir.x.ToString("0.00").Replace(",", ".")},{dir.y.ToString("0.00").Replace(",", ".")},{dir.z.ToString("0.00").Replace(",", ".")}" +
                $",{rotDir.x.ToString("0.00").Replace(",", ".")},{rotDir.y.ToString("0.00").Replace(",", ".")},{rotDir.z.ToString("0.00").Replace(",", ".")}]" +
                $", a ={acceleration}, t ={time})\n");
        }


        /// <summary>
        /// Moves to a Specific Point. Does not use inverse Kinematic
        /// </summary>
        public static void MoveJ(Vector3 pos, Vector3 rot, float acceleration = 1.4f, float speed = 1.05f, float time = 0, float radius = 0)
        {
            Connection.Send($"movej([{pos.x},{pos.y},{pos.z},{rot.x},{rot.y},{rot.z}], a={acceleration},v={speed},t={time},r={radius})\n");
        }

        /// <summary>
        /// Moves to a Specific Point. Uses inverse Kinematic
        /// </summary>
        public static void MoveJ(Pose pos, float acceleration = 1.4f, float speed = 1.05f, float time = 0, float radius = 0)
        {
            Debug.Log($"movej({pos.poseString}, a={acceleration},v={speed},t={time},r={radius})\n");
            Connection.Send($"movej({pos.poseString}, a={acceleration},v={speed},t={time},r={radius})\n");
        }


        /// <summary>
        /// Move Circular: Move to position (circular in tool-space)
        /// </summary>
        /// <param name="poseVia">path point (note: only position is used)</param>
        /// <param name="poseTo">target pose (note: only position is used in Fixed orientation mode).</param>
        /// <param name="a">tool acceleration [m/s^2]</param>
        /// <param name="v">tool speed [m/s]</param>
        /// <param name="mode">0: Unconstrained mode. Interpolate orientation from current pose to target pose(pose_to) 1: Fixed mode.Keep orientation constant relative to the tangent of the circular arc (starting from current pose)</param>
        public static void MoveC(Pose poseVia, Pose poseTo, float a = 1.2f, float v = 0.25f, int mode = 1)
        {
            Connection.Send($"movec({poseVia.poseString},{poseTo.poseString}, a={a}, v={v}, mode={mode})\n");
        }


        public static void MoveP(Pose pos, float acceleration = 1.2f, float speed = 0.25f, float radius = 0)
        {
            Connection.Send($"movep({pos.poseString}, a={acceleration}, v={speed}, r={radius})\n");
        }

        public static void MoveL(Pose pos, float acceleration = 1.2f, float speed = 0.25f, float time = 1, float radius = 0)
        {
            Connection.Send($"movel({pos.poseString}, a={acceleration}, v={speed}, t={time}, r={radius})\n");
        }

        public static void ServoC(Pose pos, float acceleration = 1.2f, float speed = 0.25f, float radius = 0)
        {
            Connection.Send($"servoc({pos.poseString}, a={acceleration}, v={speed}, r={radius})\n");
        }

        public static void Popup(string text, string title)
        {
            Connection.Send($" popup(\"{text}\", title=\"{title}\",blocking=True)\n");
        }

        public static void Shutdown()
        {
            Connection.Send("powerdown()\n");
        }
    }
}

