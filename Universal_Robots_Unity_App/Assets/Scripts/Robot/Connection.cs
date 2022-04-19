using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Robot
{
    public class Connection : MonoBehaviour
    {
        public static event Action OnConnected;
        public static event Action OnDisconnected;

        public static string Host { get; private set; }
        public static int Port { get; private set; }
        public static bool IsActive { get; private set; }
        public static Data Data { get; private set; }


        private static TcpClient tcpWrite;
        private static TcpClient tcpRead;
        private static UTF8Encoding utf8 = new UTF8Encoding();

        public static async Task Connect(string host = "127.0.0.1", int port = 30003)
        {
            Host = host;
            Port = port;

            tcpWrite = new TcpClient();
            tcpRead = new TcpClient();
            await tcpWrite.ConnectAsync(host, port);
            await tcpRead.ConnectAsync(host, port);

            thread = new Thread(new ThreadStart(FetchValues));
            thread.Start();
            OnConnected?.Invoke();
        }

        public static void Disconnect()
        {
            if (!IsActive) return;
            IsActive = false;
            tcpWrite.Close();     
            tcpRead.Close();
            OnDisconnected?.Invoke();
        }

        private void OnEnable()
        {
            Data = ScriptableObject.CreateInstance<Data>();
            Data.Current = Data;
        }

        private void OnDestroy()
        {
            if (!IsActive) return;
            Disconnect();
            tcpWrite.Dispose();
            tcpRead.Dispose();
        }

        public static void Send(string command)
        {
            NetworkStream stream = tcpWrite.GetStream();
            byte[] data = utf8.GetBytes(command);
            stream.Write(data, 0, data.Length);
            //Debug.Log(command);
        }



        private static Thread thread;

        private static readonly byte[] packet = new byte[1116];
        private static readonly byte firstPacketSize = 4;
        private static readonly byte offset = 8;
        private static readonly UInt32 totalMsgLenght = 3288596480;
        //  Communication speed: CB-Series 125 Hz (8 ms), E-Series 500 Hz (2 ms)
        private static readonly int timeStep = 8;


        private static void FetchValues()
        {
            IsActive = true;

            while (IsActive)
            {
                try
                {
                    NetworkStream stream = tcpRead.GetStream();
                    var t = new Stopwatch();


                    if (stream.Read(packet, 0, packet.Length) != 0)
                    {
                        if (BitConverter.ToUInt32(packet, firstPacketSize - 4) == totalMsgLenght)
                        {
                            t.Start();
                            Array.Reverse(packet);


                            Data.jointRot[0] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (32 * offset));
                            Data.jointRot[1] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (33 * offset));
                            Data.jointRot[2] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (34 * offset));
                            Data.jointRot[3] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (35 * offset));
                            Data.jointRot[4] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (36 * offset));
                            Data.jointRot[5] = BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (37 * offset));

                            Data.position = new Vector3((float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (56 * offset)),
                                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (57 * offset)),
                                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (58 * offset)));

                            Data.rotation = new Vector3((float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (59 * offset)),
                                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (60 * offset)),
                                                        (float)BitConverter.ToDouble(packet, packet.Length - firstPacketSize - (61 * offset)));

                            
                            t.Stop();
                            if (t.ElapsedMilliseconds < timeStep)
                            {
                                Thread.Sleep(timeStep - (int)t.ElapsedMilliseconds);
                            }
                        }
                    }
                }
                catch (SocketException e)
                {
                    Debug.Log("Socket Exception:" + e);
                }
            }
        }
    }
}

