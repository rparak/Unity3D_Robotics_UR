using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{

    /// <summary>
    /// No Need to access this. Connection will handle it. Here are some docs on what its doing https://s3-eu-west-1.amazonaws.com/ur-support-site/115824/scriptManual_SW5.11.pdf
    /// </summary>
    internal static class ConnectionSend
    {
        public static TcpClient tcpWrite = new TcpClient();
        private static UTF8Encoding utf8 = new UTF8Encoding();

        private static int task;


        public static void Send(string command)
        {
            if (Connection.unityState == Connection.UnityState.emergencyStop) return;
            NewTask();

            NetworkStream stream = tcpWrite.GetStream();
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

            NetworkStream stream = tcpWrite.GetStream();
            byte[] data = utf8.GetBytes(command);
            stream.Write(data, 0, data.Length);

            while(task == taskId)
            {
                await Task.Delay(50);
                if (Connection.isMoving == false) return true;
            }
            return false;
        }

        private static int NewTask() => ++task;

        public static async Task Start() => await tcpWrite.ConnectAsync(Connection.Host, Connection.PortWrite);

        public static void Stop() => tcpWrite.Close();

    }
}

