using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System;

public class StimuliStart : MonoBehaviour
{
    // Start is called before the first frame update
    static int block = 0;
    void Start()
    {
        StartCoroutine("sendTime");
        block++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    class SocketUdpTest
    {
        public byte[] byteSendingArray;
        public EndPoint ep;
        public Socket socketClient;

        public SocketUdpTest()
        {
            byteSendingArray = new byte[100];
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("192.168.1.116"), 8002);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ep = (EndPoint)iep;
        }

        public void SendingData(string strMsg)
        {
            byteSendingArray = Encoding.Default.GetBytes(strMsg);
            socketClient.SendTo(byteSendingArray, ep);
        }

    }

    SocketUdpTest SocketUdpSending = new SocketUdpTest();

    IEnumerator sendTime()
    {
        yield return new WaitForSeconds(0);
        DateTime timeSpan = DateTime.Now;
        string _timespan = timeSpan.ToString("HH:mm:ss.fffff");
        string _description = "第"+block+"个block刺激开始：" + _timespan;
        SocketUdpSending.SendingData(_description);


    }
}
