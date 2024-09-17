using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;

public class TestUdp1 : MonoBehaviour
{
    private Socket udpSocket;
    private EndPoint remoteEndPoint;
    // Start is called before the first frame update
    void Start()
    {
        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        remoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.31.144"), 8002);

        DateTime timestamp = DateTime.Now;
        string timestampStr = timestamp.ToString("HH:mm:ss.fff");

        byte[] data = Encoding.UTF8.GetBytes(timestampStr);
        udpSocket.SendTo(data, remoteEndPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (udpSocket != null)
        {
            udpSocket.Close();
        }
    }
}
