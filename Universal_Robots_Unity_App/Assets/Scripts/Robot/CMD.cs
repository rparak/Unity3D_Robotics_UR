using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robot
{

    /// <summary>
    /// This Class knows a lot of different Commands for the Robot. Not all are included. For more info visit: https://s3-eu-west-1.amazonaws.com/ur-support-site/115824/scriptManual_SW5.11.pdf
    /// </summary>
    public static class CMD
    {
        private static UTF8Encoding utf8 = new UTF8Encoding();
        public static event Action<string> OnSend;


        public static void Stop() => ConnectionSend.Send("stopl(20)\n");


        /// <summary>
        /// Moves in a specific direction.
        /// </summary>
        public static void SpeedL(Vector3 dir, Vector3 rotDir, float acceleration = 1.4f, float time = .05f)
        {
            ConnectionSend.Send($"speedl(" +
                $"[{dir.x.ToString("0.00").Replace(",", ".")},{dir.y.ToString("0.00").Replace(",", ".")},{dir.z.ToString("0.00").Replace(",", ".")}" +
                $",{rotDir.x.ToString("0.00").Replace(",", ".")},{rotDir.y.ToString("0.00").Replace(",", ".")},{rotDir.z.ToString("0.00").Replace(",", ".")}]" +
                $", a ={acceleration}, t ={time})\n");
        }


        /// <summary>
        /// Moves to a Specific Point. Does not use inverse Kinematic
        /// </summary>
        public static void MoveJ(Vector3 pos, Vector3 rot, float acceleration = 1.4f, float speed = 1.05f, float time = 0, float radius = 0)
        {
            ConnectionSend.Send($"movej([{pos.x},{pos.y},{pos.z},{rot.x},{rot.y},{rot.z}], a={acceleration},v={speed},t={time},r={radius})\n");
        }

        /// <summary>
        /// Moves to a Specific Point. Uses inverse Kinematic
        /// </summary>
        public static void MoveJ(Pose pos, float acceleration = 1.4f, float speed = 1.05f, float time = 0, float radius = 0)
        {
            ConnectionSend.Send($"movej({pos.poseString}, a={acceleration},v={speed},t={time},r={radius})\n");
        }

        public static async Task<bool> MoveJAsync(Pose pos, float acceleration = 1.4f, float speed = 1.05f, float time = 0, float radius = 0)
        {
           return await ConnectionSend.SendAsync($"movej({pos.poseString}, a={acceleration},v={speed},t={time},r={radius})\n");
        }

        /// <summary>
        /// Move Circular: Move to position (circular in tool-space)
        /// </summary>
        /// <param name="poseVia">path point (note: only position is used)</param>
        /// <param name="poseTo">target pose (note: only position is used in Fixed orientation mode).</param>
        /// <param name="a">tool acceleration [m/s^2]</param>
        /// <param name="v">tool speed [m/s]</param>
        /// <param name="mode">Unconstrained mode: Interpolate orientation from current pose to target pose(pose_to) Fixed (Contraint) mode: Keep orientation constant relative to the tangent of the circular arc (starting from current pose)</param>
        public static void MoveC(Pose poseVia, Pose poseTo, float a = 1.2f, float v = 0.25f, ContraintMode mode = ContraintMode.contraint)
        {
            ConnectionSend.Send($"movec({poseVia.poseString},{poseTo.poseString}, a={a}, v={v}, mode={(int)mode})\n");
        }


        public static void MoveP(Pose pos, float acceleration = 1.2f, float speed = 0.25f, float radius = 0)
        {
            ConnectionSend.Send($"movep({pos.poseString}, a={acceleration}, v={speed}, r={radius})\n");
        }

        public static void MoveL(Pose pos, float acceleration = 1.2f, float speed = 0.25f, float time = 1, float radius = 0)
        {
            ConnectionSend.Send($"movel({pos.poseString}, a={acceleration}, v={speed}, t={time}, r={radius})\n");
        }

        public static void ServoC(Pose pos, float acceleration = 1.2f, float speed = 0.25f, float radius = 0)
        {
            ConnectionSend.Send($"servoc({pos.poseString}, a={acceleration}, v={speed}, r={radius})\n");
        }

        /// /////////////////////////////////Other Functions

        public static void SetAnalogOutput(int output, float strenght)
        {
            ConnectionSend.Send($"set_standard_analog_out({output},{strenght})\n");
        }

        public static void Popup(string text, string title)
        {
            ConnectionSend.Send($"popup(\"{text}\", title=\"{title}\",blocking=True)\n");
        }

        public static void Shutdown()
        {
            ConnectionSend.Send("powerdown()\n");
        }

        /// /////////////////////////////////////// Dashboard Functions

        public static class Control
        {
            public async static void ClosePopup()
            {
                string info = await ConnectionDashboard.Send("close popup\n");
                OnSend?.Invoke(info);
                Debug.Log(info);
            }

            public async static void CloseSafetyPopup()
            {
                string info = await ConnectionDashboard.Send("close safety popup\n");
                Debug.Log(info);
            }

            public async static Task<bool> IsInRemoteControl()
            {
                string info = await ConnectionDashboard.Send("is in remote control\n");
                Debug.Log(info);
                return bool.Parse(info);
            }

            public async static void PowerOn()
            {
                string info = await ConnectionDashboard.Send("power on\n");
                OnSend?.Invoke(info);
                Debug.Log(info);
            }

            public async static void PowerOff()
            {
                string info = await ConnectionDashboard.Send("power off\n");
                OnSend?.Invoke(info);
                Debug.Log(info);
            }

            public async static void ReleaseBrake()
            {
                string info = await ConnectionDashboard.Send("brake release\n");
                OnSend?.Invoke(info);
                Debug.Log(info);
            }
        }

        public enum ContraintMode
        {
            uncontraint = 0,
            contraint = 1
        }
    }
}

