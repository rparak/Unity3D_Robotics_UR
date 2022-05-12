using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Robot
{
    internal static class ConnectionGripper
    {
        public static TcpClient tcpClient;
        private static UTF8Encoding utf8 = new UTF8Encoding();
        private static Thread thread;
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
            if(returnMessage.Contains("POS")) Gripper.Position = int.Parse(returnMessage.Substring(4));
            //else if (returnMessage.Contains("FOR")) Gripper.Force = int.Parse(returnMessage.Substring(4));
            //else if (returnMessage.Contains("SPE")) Gripper.Speed = int.Parse(returnMessage.Substring(4));
            //else if (returnMessage.Contains("FLT")) Gripper.Fault = int.Parse(returnMessage.Substring(4));

            //Debug.Log(returnMessage);
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

        public static async Task Start(string host, int port)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(host, port);
            CMD.Gripper.Activate();

            thread = new Thread(new ThreadStart(FetchValues));
            thread.Start();
        }

        public static void Stop()
        {
            if (tcpClient == null) return;
            tcpClient.Close();
            tcpClient.Dispose();
        }



        private static void FetchValues()
        {
            while(Connection.unityState != Connection.UnityState.offline)
            {
                Thread.Sleep(200);
                BeginSend("GET POS\n");
                //BeginSend("GET SPE\n");
                //BeginSend("GET FOR\n");
                //BeginSend("GET FLT\n");
            }
        }

        
    }

    public static class Gripper
    {
        public static int Position;
        //public static int Force;
        //public static int Speed;
        //public static int Fault;
    }
}

