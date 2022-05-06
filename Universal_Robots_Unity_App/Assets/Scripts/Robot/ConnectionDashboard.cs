using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robot
{
    /// <summary>
    /// Here are the docs to this one https://s3-eu-west-1.amazonaws.com/ur-support-site/42728/DashboardServer_e-Series.pdf
    /// </summary>
    internal static class ConnectionDashboard
    {
        public static TcpClient tcpClient;

        private static UTF8Encoding utf8 = new UTF8Encoding();
        private static string returnMessage;

        public async static Task<string> Send(string command)
        {
            returnMessage = string.Empty;
            
            BeginRead();
            BeginSend(command);

            while (string.IsNullOrEmpty(returnMessage)) await Task.Delay(20);
            return returnMessage;
        }

        public static void BeginRead()
        {
            var buffer = new byte[4096];
            var ns = tcpClient.GetStream();
            ns.BeginRead(buffer, 0, buffer.Length, EndRead, buffer);
        }
        public static void EndRead(IAsyncResult result)
        {
            var buffer = (byte[])result.AsyncState;
            var ns = tcpClient.GetStream();
            var bytesAvailable = ns.EndRead(result);

            returnMessage = Encoding.ASCII.GetString(buffer, 0, bytesAvailable);
            BeginRead();
        }
        public static void BeginSend(string cmd)
        {
            byte[] bytes = utf8.GetBytes(cmd);
            var ns = tcpClient.GetStream();
            ns.BeginWrite(bytes, 0, bytes.Length, EndSend, bytes);
        }

        public static void EndSend(IAsyncResult result)
        {
            var bytes = (byte[])result.AsyncState;
           // Debug.Log($"Sent {bytes.Length} bytes to server.");
           // Debug.Log($"Sent: {Encoding.ASCII.GetString(bytes)}");
        }


        /// Inits

        public static async Task Start(string host, int port)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(host, port);
            BeginRead();
        }

        public static void Stop()
        {
            tcpClient.Close();
            tcpClient.Dispose();
        }
    }
}

