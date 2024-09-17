using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    private Thread receiveThread;
    private UdpClient udpClient;
    private int port = 8002;

    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReceiveData()
    {
        udpClient = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port);
                byte[] data = udpClient.Receive(ref anyIP);

                string receivedText = Encoding.Default.GetString(data);
                Debug.Log("Received Data: " + receivedText);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
    }

    private void OnDisable()
    {
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}
