using System;
using System.Threading.Tasks; 
using UnityEngine;

namespace Robot
{
    public class Connection : MonoBehaviour
    {
        public static event Action OnConnected;
        public static event Action OnDisconnected;

        public static event Action OnIdle;

        public static string Host { get; private set; }


        public static UnityState unityState { get; internal set; }
        public static RoboSafety roboSafety { get; internal set; }
        public static RoboModes roboModes { get; internal set; }
        public static bool isMoving { get; private set; }
        public static double digitalOutput { get; internal set; }

        
        /// <summary>
        /// Starts all the other Connections
        /// </summary>
        /// <param name="readPort30013">While using 30013 is better. Some systems don't have this option</param>
        /// <returns>Only returns if it was successful.</returns>
        public static async Task Connect(string host = "127.0.0.1", bool readPort30013 = true)
        {
            Host = host;

            ConnectionRecieve.Port = readPort30013 ? 30013 : 30003;

            unityState = UnityState.online;

            await ConnectionSend.Start();
            await ConnectionRecieve.Start();
            await ConnectionDashboard.Start();
            
            OnConnected?.Invoke();
        }

        public static void Disconnect()
        {
            if (unityState == UnityState.offline) return;

            unityState = UnityState.offline;
            roboModes = RoboModes.noController;
            roboSafety = RoboSafety.noRobotDetected;

            ConnectionSend.Stop();
            ConnectionRecieve.Stop();
            ConnectionDashboard.Stop();

            OnDisconnected?.Invoke();
        }

        private void OnEnable() => Data.Current = ScriptableObject.CreateInstance<Data>();

        private void OnDestroy()
        {
            if (unityState != UnityState.offline) Disconnect();
            ConnectionSend.tcpWrite.Dispose();
            ConnectionRecieve.tcpRead.Dispose();
        }


        // ////////////////////////////////////////////////////////


        public static void Send(string cmd) => ConnectionSend.Send(cmd);
        public async Task<bool> SendAsync(string cmd) => await ConnectionSend.SendAsync(cmd);


        // //////////////////////////////////////////////////////

        internal static void SetMoving(bool state)
        {
            if(isMoving == true && !state) OnIdle?.Invoke(); //May be better if we just have a bool reading it instead of an event
            isMoving = state;

        }

        public enum UnityState
        {
            offline,
            online,
            emergencyStop
        }

        public enum RoboSafety
        {
            noRobotDetected = 0,
            normal = 1,
            reduced = 2,
            protectiveStop = 3,
            recovery = 4,
            safeGuardStop = 5,
            emergencyStopEuromap67 = 6,
            emergencyStopScreen = 7,
            violation = 8,
            fault = 9,
            validateJointId = 10,
            undefinedSafetyMode = 11,
            safeguardStop = 12,
            positionEnablingStop = 13
        }

        public enum RoboModes
        {
            noController = -1,
            disconnected = 0,
            confirmSafety = 1,
            booting = 2,
            powerOff = 3,
            powerOn = 4,
            idle = 5,
            backdrive = 6,
            running = 7,
            updatingFirmware = 8
        }
    }
}

