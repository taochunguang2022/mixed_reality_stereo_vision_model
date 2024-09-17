using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeedRecordnamespace;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class RotateMove : MonoBehaviour
{
   // private SpeedControl speedControl = new SpeedControl();//һ��Ҫ���ó�˽�б���������������
    public float speed = 3.20f;

   // public List<float> speedList = new List<float> { 5.00f, 5.00f, 5.00f, 5.00f, 5.00f, 5.00f };//6���ٶ�
   // private float selectedSpeed;
   // public List<int> selectedIndices = new List<int>(); // ��¼��ѡ�������
   // private static float[] Temp = new float[7];//�ٶ�����
   // private static int i = 0;//�ٶȴ���




    public int numCycles = 2; // ����������
    float timer = 0f;//��ʱ��
    int cycleCount = 0; // �������ڼ���

    public int numEpochs = 20;//����epoch��
    private int currentEpoch = 0;//�Ѿ���ɵ�epoch��




    bool movingForward = true;//�Ƿ���ǰ�˶�
    // Start is called before the first frame update
    public Vector3 initialPosition;// ��ʼλ��
    public Vector3 endPosition; // ��ֹλ��

    private float totalDistance; // ������ֹλ��������ܾ���
    private float distanceTraveled; // �Ѿ��ƶ��ľ���

    private static int block = 0;
    //private static string path = @"D:\test\timeLog.txt";
   // private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test23_4d4kmw1bzqv36\LocalState\timeLog.txt";

    // Start is called before the first frame update
    void Start()
    {
        block++;
        initialPosition = transform.position;
        TestEnd end = GameObject.FindObjectOfType<TestEnd>();
        endPosition = end.endPosition;
        totalDistance = Vector3.Distance(initialPosition, endPosition);
        distanceTraveled = 0f;
        // speed = SelectRandomSpeed();

      //  speedControl.SelectRandomSpeed();//�������ѡ���ٶȷ���
     //   speed = speedControl.Speed;//���ٶȿ������е�����ѡ����ٶ� 
    }

    // Update is called once per frame
    void Update()
    {
        if (movingForward)
        {
            // ���������ǰ�˶���������ֹλ���˶�
            transform.Translate((endPosition - initialPosition).normalized * speed * Time.deltaTime, Space.World);

            // �����Ѿ��ƶ��ľ���
            distanceTraveled += speed * Time.deltaTime;

            // �ж��Ƿ��Ѿ��ƶ��˵�����ֹλ��������ܾ���
            if (distanceTraveled >= totalDistance)
            {


                // �����Ѿ��ƶ��ľ���
                distanceTraveled = 0f;

                // �л�Ϊ�����˶�״̬
                movingForward = false;
            }
        }
        else
        {
            // ������������˶��������ʼλ���˶�
            transform.Translate((initialPosition - endPosition).normalized * speed * Time.deltaTime, Space.World);

            // �����Ѿ��ƶ��ľ���
            distanceTraveled += speed * Time.deltaTime;

            // �ж��Ƿ��Ѿ��ƶ��˵�����ֹλ��������ܾ���
            if (distanceTraveled >= totalDistance)
            {


                // �����Ѿ��ƶ��ľ���
                distanceTraveled = 0f;

                // �л�Ϊ��ǰ�˶�״̬
                movingForward = true;

                // ���ڼ���������
                cycleCount++;

                // �ж��Ƿ�ﵽָ��������������
                if (cycleCount >= numCycles)
                {
                    //����cycleCount��ʼֵ������currentEpoch
                    cycleCount = 0;
                    currentEpoch++;

                    if(currentEpoch >= numEpochs)
                    {
                        
                       // DateTime timestamp = DateTime.Now;
                       // LogEndTime(timestamp);
                        enabled = false;
                        SceneManager.LoadScene(3);//��ת��ֳ���
                    }
                    
                    // UnityEditor.EditorApplication.isPlaying = false; // ����ʱʹ��
                    // Application.Quit(); // ��ʽʹ��       
                }
            }
        }
    }

   // private static void LogEndTime(DateTime timestamp)
   // {
       // using (StreamWriter writer = new StreamWriter(path, true))
       // {

          //  writer.WriteLine("��" + block + "��block" + "�̼��������ʱ�䣺" + timestamp.ToString());

           // writer.WriteLine("------------");
      //  }
   // }
}
