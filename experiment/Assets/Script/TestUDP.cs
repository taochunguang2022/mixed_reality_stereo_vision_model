using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TestUDP : MonoBehaviour
{
    // Start is called before the first frame update
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    // 接收方的IP地址和端口号
    private const string ReceiverIp = "192.168.31.144"; // 接收方（笔记本电脑）的IP地址
    private const int ReceiverPort = 1111; // 可以选择其他的端口号，只要确保发送方和接收方使用相同的端口


    void Start()
    {
        udpClient = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(ReceiverIp), ReceiverPort);

        DateTime timestamp = DateTime.Now;
        SendTimestamp("Timestamp: " + timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));

    }

    private void OnDestroy()
    {
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }

    private void SendTimestamp(string message)
{
    try
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        udpClient.Send(data, data.Length, remoteEndPoint);
            Debug.Log("发送成功");
    }
    catch (Exception ex)
    {
        Debug.LogError("Failed to send UDP message: " + ex.Message);
    }
}

// Update is called once per frame
void Update()
    {
        
    }

   
}
