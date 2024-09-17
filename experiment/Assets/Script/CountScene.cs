using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountScene : MonoBehaviour
{
    public static int countScene = 0;
    // Start is called before the first frame update
    void Start()
    {
        countScene++;
        // Debug.Log("countScene="+countScene);
        if (countScene >= 16)//说明15个block执行结束，跳转放松后场景
        {
            StartCoroutine("sendTime");

            SceneManager.LoadScene(4);//跳转休息场景
        }

       

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    /*
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
            yield return new WaitForSeconds(0);
            DateTime timeSpan = DateTime.Now;
            string _timespan = timeSpan.ToString("HH:mm:ss.fffff");
            string _description = "准备开始采集：" + _timespan;
            SocketUdpSending.SendingData(_description);
        }
    }
    */
}
