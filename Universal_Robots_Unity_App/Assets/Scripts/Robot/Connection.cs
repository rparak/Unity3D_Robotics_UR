using System;
using System.Threading.Tasks; 
using UnityEngine;

namespace Robot
{
    public class Connection : MonoBehaviour
    {
        public static event Action OnConnected;
        public static event Action OnDisconnected;

        public static string host = "127.0.0.1";
        public static int sendPort = 30003;
        public static int recievePort = 30013;
        public static int dashbordPort = 29999;
        public static int gripperPort = 63352;


        public static UnityState unityState { get; internal set; }



        /// <summary>Starts all the other Connections</summary>
        /// <returns>Only returns if it was successful.</returns>
        public static async Task<bool> Connect()
        {
            unityState = UnityState.online;

            _ = ConnectionSend.Start(host, sendPort);
            _ = ConnectionRecieve.Start(host, recievePort);
            _ = ConnectionDashboard.Start(host, dashbordPort);
            _ = ConnectionGripper.Start(host, gripperPort);


            //Check if all of them connect otherwise timeout.
            for (int i = 0; i < 20; i++)
            {
                if(ConnectionSend.tcpClient.Connected &&
                    ConnectionRecieve.tcpRead.Connected &&
                    ConnectionDashboard.tcpClient.Connected)
                    //We could also add Gripper but is it requiered? -No
                {
                    OnConnected?.Invoke();
                    return true;
                }
                await Task.Delay(20);
            }
            

            unityState = UnityState.offline;
            return false;
        }

        public static void Disconnect()
        {
            if (unityState == UnityState.offline) return;

            unityState = UnityState.offline;

            ConnectionSend.Stop();
            ConnectionRecieve.Stop();
            ConnectionDashboard.Stop();
            ConnectionGripper.Stop();

            OnDisconnected?.Invoke();
        }

        private void OnEnable() => Data.Current = ScriptableObject.CreateInstance<Data>();

        private void OnDestroy()
        {
            if (unityState != UnityState.offline) Disconnect();
        }


        // ////////////////////////////////////////////////////////


        public static void SendCommand(string cmd) => ConnectionSend.Send(cmd);
        public static async Task<bool> SendCommandAsync(string cmd) => await ConnectionSend.SendAsync(cmd);

        public static async Task<string> SendDashboardAsync(string cmd) => await ConnectionDashboard.Send(cmd);

        public static async Task<string> SendGripper(string cmd) => await ConnectionGripper.Send(cmd);


        // //////////////////////////////////////////////////////

        public enum UnityState
        {
            offline,
            online,
            emergencyStop
        }
    }
}

