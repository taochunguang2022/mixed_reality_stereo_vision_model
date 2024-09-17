using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class ScoreMark : MonoBehaviour
{
    // Start is called before the first frame update
    //private static string path = @"D:\test\timeLog.txt";
   // private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test23_4d4kmw1bzqv36\LocalState\timeLog.txt";
    private static int block = 0;
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
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ep = (EndPoint)iep;
        }

        public void SendingData(string strMsg)
        {
            byteSendingArray = Encoding.Default.GetBytes(strMsg);
            socketClient.SendTo(byteSendingArray, ep);
        }

        SocketUdpTest SocketUdpSending = new SocketUdpTest();

        IEnumerator sendTime()
        {
            yield return new WaitForSeconds(1);
            DateTime timeSpan = DateTime.Now;
            string _timespan = timeSpan.ToString("HH:mm:ss.fffff");
            string _description = "第" + block + "个block评分开始：" + _timespan;
            SocketUdpSending.SendingData(_description);
        }

    }


}
