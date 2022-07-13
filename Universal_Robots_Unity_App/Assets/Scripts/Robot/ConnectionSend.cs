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
        public static TcpClient tcpClient;
        public static UTF8Encoding utf8 = new UTF8Encoding();

        //public static int task; Replaced by Connection TaskID



        public static void Send(string command)
        {
            
            if (!IsAllowed()) return;
            Connection.NewTask();

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
            if (!IsAllowed()) return false;
            ushort taskId = Connection.NewTask();

            //UnityEngine.Debug.Log($"New Task {taskId}");

            NetworkStream stream = tcpClient.GetStream();
            byte[] data = utf8.GetBytes(command);
            stream.Write(data, 0, data.Length);

            while (Connection.TaskID == taskId)
            {
                await Task.Delay(50);
                //UnityEngine.Debug.Log($"Moving? {Robot.isMoving}");
                if (Robot.isMoving == false) return true;
            }
            return false;
        }

        public async static Task<bool> WaitAsync(int milliseconds)
        {
            if (!IsAllowed()) return false;
            ushort taskId = Connection.NewTask();

            await Task.Delay(milliseconds);
            return Connection.TaskID == taskId;
        }

         // //////////////////////////////////////

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

        private static bool IsAllowed()
        {
            if (Connection.unityState != Connection.UnityState.online)
            {
                switch (Connection.unityState)
                {
                    case Connection.UnityState.offline:
                        Connection.Feedback("Der Cobot ist noch nicht mit der Application verbunden\n" +
                    "Unten Rechts kannst du dich mit ihn verbinden.");
                        break;

                    case Connection.UnityState.emergencyStop:
                        Connection.Feedback("Emergency Stop aktive. Bitte entschärfe ihn davor.");
                        break;
                }
                
            }
            else if (Robot.safety != Robot.RoboSafety.normal)
            {
                Connection.Feedback($"{Robot.safety}. Bitte betrachte das Tablet.");
            }
            else if (Robot.mode != Robot.RoboModes.running)
            {
                switch (Robot.mode)
                {
                    case Robot.RoboModes.powerOff:
                        Connection.Feedback($"Coby is zurzeit im ausgeschaltet. Du kannst ihn im status panel einschalten.");
                        break;

                    case Robot.RoboModes.idle:
                        Connection.Feedback("Coby ist nicht bereit. Im status panel kannst du die Bremsen Lösen.");
                        break;

                    default:
                        Connection.Feedback($"Coby is zurzeit im {Robot.mode} modus, sollte aber im running modus sein.");
                        break;
                }
            }
            else return true;

            Connection.NewTask(); //New Tasks force any active task to be marked as failed.
            return false;
        }
    }
}

