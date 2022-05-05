using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Robot
{
    /// <summary>
    /// No Need to access this. Connection will handle it. Here are some docs on what its doing https://s3-eu-west-1.amazonaws.com/ur-support-site/16496/ClientInterfaces_Realtime.pdf
    /// </summary>
    internal static class ConnectionRecieve
    {
        public static TcpClient tcpRead = new TcpClient();
        private static Thread thread;

        internal static int Port = 30013;

        private static readonly byte[] packet = new byte[1116];//1116
        private static readonly byte firstPacketSize = 4;
        private static readonly byte offset = 8;
        private static readonly UInt32 totalMsgLenght = 3288596480;
        private static readonly int timeStep = 2;   //  Communication speed: CB-Series 125 Hz (8 ms), E-Series 500 Hz (2 ms)


        public static async Task Start()
        {
            Debug.Log($"Connecting to {Connection.Host}:{Port}");
            await tcpRead.ConnectAsync(Connection.Host, Port);

            thread = new Thread(new ThreadStart(FetchValues));
            thread.Start();  
        }

        public static void Stop()
        {
            tcpRead.Close();
        }

        private static void FetchValues()
        {
            NetworkStream stream = tcpRead.GetStream();
            var t = new Stopwatch();

            Debug.Log("Start");
            try
            {
                while (Connection.unityState != Connection.UnityState.offline)
                {
                    Debug.Log("Reading");

                    if (stream.Read(packet, 0, packet.Length) != 0)
                    {
                        Debug.Log(BitConverter.ToUInt32(packet, firstPacketSize - 4));
                        if (BitConverter.ToUInt32(packet, firstPacketSize - 4) == totalMsgLenght ||
                           BitConverter.ToUInt32(packet, firstPacketSize - 4) == 1543766016)
                        {
                            t.Start();

                            ReadRobotInfo();

                            t.Stop();
                            if (t.ElapsedMilliseconds < timeStep)
                            {
                                Thread.Sleep(timeStep - (int)t.ElapsedMilliseconds);
                            }
                        }
                    }
                }
                
            }
            catch (SocketException e)
            {
                Connection.Disconnect();
                Debug.Log("Socket Exception:" + e);
            }

            Debug.Log("End");
        }

        private static void ReadRobotInfo()
        {
            Array.Reverse(packet);

            //Target Joint Positions 2 - 7


            //Joint Velocity 8 - 13
            Connection.SetMoving(CheckIfMoving());


            //Actual joint posistions 32 - 37
            Data.Current.jointRot[0] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (32 * offset));
            Data.Current.jointRot[1] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (33 * offset));
            Data.Current.jointRot[2] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (34 * offset));
            Data.Current.jointRot[3] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (35 * offset));
            Data.Current.jointRot[4] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (36 * offset));
            Data.Current.jointRot[5] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (37 * offset));

            //Actual Cartesian Coord of tool 56 - 61
            Data.Current.position = new Vector3((float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (56 * offset)),
                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (57 * offset)),
                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (58 * offset)));

            Data.Current.rotation = new Vector3((float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (59 * offset)),
                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (60 * offset)),
                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (61 * offset)));

            //87 => Base Temp
            //88 => Shoulder Temp
            //89 => Elbow Temp
            //90 => Wrist 1 Temp
            //91 => Wrist 2 Temp
            //92 => Wrist 3 Temp

            //94 => 2 Might be control mode
            //95 => Robot Mode Off = 2, Boot = 5, On = 7
            //96 - 101 => Joint Mode

            //Modes mode 95
            Connection.roboModes = (Connection.RoboModes)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (95 * offset));

            //Saftey mode 102
            Connection.roboSafety = (Connection.RoboSafety)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (102 * offset));

            //Digital Outputs 121
            Connection.digitalOutput = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (131 * offset));





            bool CheckIfMoving()
            {
                for (int i = 0; i < 6; i++)
                {

                    if (BitConverter.ToDouble(packet, packet.Length - firstPacketSize - ((8 + i) * offset)) == 0) continue;
                    else return true;
                }
                return false;
            }
        }
    }
}