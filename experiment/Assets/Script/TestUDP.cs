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

    // ���շ���IP��ַ�Ͷ˿ں�
    private const string ReceiverIp = "192.168.31.144"; // ���շ����ʼǱ����ԣ���IP��ַ
    private const int ReceiverPort = 1111; // ����ѡ�������Ķ˿ںţ�ֻҪȷ�����ͷ��ͽ��շ�ʹ����ͬ�Ķ˿�


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
            Debug.Log("���ͳɹ�");
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
