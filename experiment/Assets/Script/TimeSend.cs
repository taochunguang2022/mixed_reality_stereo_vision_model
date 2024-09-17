using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;
using System;

public class TimeSend : MonoBehaviour
{
    

    void Start()
    {
        
        StartCoroutine("sendTime");
    }


    class SocketUdpTest
    {
        public byte[] byteSendingArray;
        public EndPoint ep;
        public Socket socketClient;

        public SocketUdpTest()
        {
            byteSendingArray = new byte[100];
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator sendTime()
    {
            yield return new WaitForSeconds(0);
            DateTime timeSpan = DateTime.Now;
            string _timespan = timeSpan.ToString("HH:mm:ss.fffff");
            string _description =  "刺激程序结束，"+"静息态采集开始："+_timespan;
            SocketUdpSending.SendingData(_description);
            
        
    }
}
