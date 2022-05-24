using System;
using System.Collections;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Robot
{

    /// <summary>
    /// No Need to access this. Connection will handle it. Here are some docs on what its doing https://s3-eu-west-1.amazonaws.com/ur-support-site/115824/scriptManual_SW5.11.pdf
    /// </summary>
    internal static class ConnectionSend
    {
        public static TcpClient tcpClient;
        public static UTF8Encoding utf8 = new UTF8Encoding();

        private static int task;



        public static void Send(string command)
        {
            if (Connection.unityState != Connection.UnityState.online) return;
            NewTask();

            NetworkStream stream = tcpClient.GetStream();
            byte[] data = utf8.GetBytes(command);
            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Sends the command and responds when its competed.
        /// </summary>
        /// <param name="command">The Command to send to the Robot. If you have no Idea then do it over Robot.CMD</param>
        /// <returns>True if successful. False if command got interupted</returns>
        public async static Task<bool> SendAsync(string command)
        {
            if (Connection.unityState == Connection.UnityState.emergencyStop) return false;
            int taskId = NewTask();

            NetworkStream stream = tcpClient.GetStream();
            byte[] data = utf8.GetBytes(command);
            stream.Write(data, 0, data.Length);

            while (task == taskId)
            {
                await Task.Delay(50);
                if (Robot.isMoving == false) return true;
            }
            return false;
        }

        private static int NewTask() => ++task;



        public static async Task Start(string host, int port)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(host, port);
        }

        public static void Stop()
        {
            tcpClient.Close();
            tcpClient.Dispose();
        }



    }
}

