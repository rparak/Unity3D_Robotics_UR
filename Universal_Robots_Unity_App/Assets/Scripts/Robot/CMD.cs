using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GripperData = Robot.Gripper;

namespace Robot
{

    /// <summary>
    /// This Class knows a lot of different Commands for the Robot. Not all are included. For more info visit: https://s3-eu-west-1.amazonaws.com/ur-support-site/115824/scriptManual_SW5.11.pdf
    /// </summary>
    public static class CMD
    {
        private static UTF8Encoding utf8 = new UTF8Encoding();
        public static event Action<string> OnSend;

        public const float defaultAcceleration = 0.4f;
        public const float defaultSpeed = 0.4f;


        public static void Stop() => ConnectionSend.Send("stopl(20)\n");


        /// <summary>
        /// Moves in a specific direction.
        /// </summary>
        public static void SpeedL(Vector3 dir, Vector3 rotDir, float acceleration = defaultAcceleration, float time = .05f)
        {
            ConnectionSend.Send($"speedl(" +
                $"[{dir.x.ToString("0.00").Replace(",", ".")},{dir.y.ToString("0.00").Replace(",", ".")},{dir.z.ToString("0.00").Replace(",", ".")}" +
                $",{rotDir.x.ToString("0.00").Replace(",", ".")},{rotDir.y.ToString("0.00").Replace(",", ".")},{rotDir.z.ToString("0.00").Replace(",", ".")}]" +
                $", a ={acceleration.ToString("0.00").Replace(",", ".")}, t ={time.ToString("0.00").Replace(",", ".")})\n");
        }


        /// <summary>
        /// Moves to a Specific Point. Does not use inverse Kinematic
        /// </summary>
        public static void MoveJ(Vector3 pos, Vector3 rot, float acceleration = defaultAcceleration, float speed = defaultSpeed, float time = 0, float radius = 0)
        {
            ConnectionSend.Send($"movej([{pos.x},{pos.y},{pos.z},{rot.x},{rot.y},{rot.z}], a={acceleration.ToString().Replace(",", ".")},v={speed.ToString().Replace(",", ".")},t={time.ToString().Replace(",", ".")},r={radius.ToString().Replace(",", ".")})\n");
        }

        /// <summary>
        /// Moves to a Specific Point. Uses inverse Kinematic
        /// </summary>
        public static void MoveJ(Pose pos, float acceleration = defaultAcceleration, float speed = defaultSpeed, float time = 0, float radius = 0)
        {
            ConnectionSend.Send($"movej({pos.poseString}, a={acceleration.ToString().Replace(",", ".")},v={speed.ToString().Replace(",", ".")},t={time.ToString().Replace(",", ".")},r={radius.ToString().Replace(",", ".")})\n");
        }

        public static async Task<bool> MoveJAsync(Pose pos, float acceleration = defaultAcceleration, float speed = defaultSpeed, float time = 0, float radius = 0)
        {
           return await ConnectionSend.SendAsync($"movej({pos.poseString}, a={acceleration.ToString().Replace(",", ".")},v={speed.ToString().Replace(",", ".")},t={time.ToString().Replace(",", ".")},r={radius.ToString().Replace(",", ".")})\n");
        }

        /// <summary>
        /// Move Circular: Move to position (circular in tool-space)
        /// </summary>
        /// <param name="poseVia">path point (note: only position is used)</param>
        /// <param name="poseTo">target pose (note: only position is used in Fixed orientation mode).</param>
        /// <param name="a">tool acceleration [m/s^2]</param>
        /// <param name="v">tool speed [m/s]</param>
        /// <param name="mode">Unconstrained mode: Interpolate orientation from current pose to target pose(pose_to) Fixed (Contraint) mode: Keep orientation constant relative to the tangent of the circular arc (starting from current pose)</param>
        public static void MoveC(Pose poseVia, Pose poseTo, float a = defaultAcceleration, float v = defaultSpeed, ContraintMode mode = ContraintMode.contraint)
        {
            ConnectionSend.Send($"movec({poseVia.poseString},{poseTo.poseString}, a={a.ToString().Replace(",", ".")}, v={v.ToString().Replace(",", ".")}, mode={(int)mode})\n");
        }


        public static void MoveP(Pose pos, float acceleration = defaultAcceleration, float speed = defaultSpeed, float radius = 0)
        {
            ConnectionSend.Send($"movep({pos.poseString}, a={acceleration.ToString().Replace(",", ".")}, v={speed.ToString().Replace(",", ".")}, r={radius.ToString().Replace(",", ".")})\n");
        }

        public static void MoveL(Pose pos, float acceleration = defaultAcceleration, float speed = defaultSpeed, float time = 1, float radius = 0)
        {
            ConnectionSend.Send($"movel({pos.poseString}, a={acceleration.ToString().Replace(",", ".")}, v={speed.ToString().Replace(",", ".")}, t={time.ToString().Replace(",", ".")}, r={radius.ToString().Replace(",", ".")})\n");
        }

        public static void ServoC(Pose pos, float acceleration = defaultAcceleration, float speed = defaultSpeed, float radius = 0)
        {
            ConnectionSend.Send($"servoc({pos.poseString}, a={acceleration.ToString().Replace(",", ".")}, v={speed.ToString().Replace(",", ".")}, r={radius.ToString().Replace(",", ".")})\n");
        }

        public static void FreeDrive()
        {
            //Freedrive is not working. Keeps exiting once entered. Probably not allowed as external control.
            //await Task.Delay(2000);
            ConnectionSend.Send("freedrive_mode (freeAxes=[1, 1, 1, 1, 1, 1], feature=p[0, 0,0, 0, 0, 0])");
            ConnectionSend.Send("sleep(20000000.0)\n");
        }

        /// /////////////////////////////////Other Functions

        public static void SetAnalogOutput(int output, float strenght)
        {
            ConnectionSend.Send($"set_standard_analog_out({output},{strenght.ToString().Replace(",", ".")})\n");
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
                return bool.Parse(info);
            }

            public async static void PowerOn()
            {
                string info = await ConnectionDashboard.Send("power on\n");
                OnSend?.Invoke(info);
            }

            public async static void PowerOff()
            {
                string info = await ConnectionDashboard.Send("power off\n");
                OnSend?.Invoke(info);
            }

            public async static void ReleaseBrake()
            {
                string info = await ConnectionDashboard.Send("brake release\n");
                OnSend?.Invoke(info);
            }
        }

        public static class Gripper
        {
            public async static void Activate()
            {
                string state = await ConnectionGripper.Send("GET STA\n");
                if (state == "STA 3") return;
                _ = ConnectionGripper.Send("SET ACT 1\n");
            }
            
            public static async Task Open(int speed = 20, int force = 20)
            {
                if (GripperData.isRunning)
                {
                    _ = ConnectionGripper.Send(
                    $"SET SPE {speed}\n" +
                    $"SET FOR {force}\n" +
                    $"SET POS 0" +
                    $"SET GTO 1\n"
                    );

                    //We can wait until values aren't changing anymore
                    int lastValue;
                    while (true)
                    {
                        lastValue = GripperData.Position;
                        await Task.Delay(500);
                        //Debug.Log($"Current {GripperData.Position} Last {lastValue}");
                        if (lastValue == GripperData.Position) return;
                    }
                }
                else
                {
                    GripperData.Position = 0;
                    await Task.Delay(500); //Act like we are waiting half a sec for this lol
                }
            }
            public static async Task Close(int speed = 20, int force = 20)
            {
                if (GripperData.isRunning)
                {
                    _ = ConnectionGripper.Send(
                    $"SET SPE {speed}\n" +
                    $"SET FOR {force}\n" +
                    $"SET POS 255" +
                    $"SET GTO 1\n"
                    );

                    //We can wait until values aren't changing anymore
                    int lastValue;
                    while (true)
                    {
                        lastValue = GripperData.Position;
                        await Task.Delay(500);
                        //Debug.Log($"Current {GripperData.Position} Last {lastValue}");
                        if (lastValue == GripperData.Position) return;
                    }
                }
                else
                {
                    GripperData.Position = 255;
                    await Task.Delay(500); //Act like we are waiting half a sec for this lol
                }
            }
        }

        public enum ContraintMode
        {
            uncontraint = 0,
            contraint = 1
        }
    }
}

