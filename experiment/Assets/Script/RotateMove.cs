using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeedRecordnamespace;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class RotateMove : MonoBehaviour
{
   // private SpeedControl speedControl = new SpeedControl();//一定要设置成私有变量！！！！！！
    public float speed = 3.20f;

   // public List<float> speedList = new List<float> { 5.00f, 5.00f, 5.00f, 5.00f, 5.00f, 5.00f };//6种速度
   // private float selectedSpeed;
   // public List<int> selectedIndices = new List<int>(); // 记录已选择的索引
   // private static float[] Temp = new float[7];//速度数组
   // private static int i = 0;//速度次数




    public int numCycles = 2; // 运行周期数
    float timer = 0f;//计时器
    int cycleCount = 0; // 运行周期计数

    public int numEpochs = 20;//运行epoch数
    private int currentEpoch = 0;//已经完成的epoch数




    bool movingForward = true;//是否往前运动
    // Start is called before the first frame update
    public Vector3 initialPosition;// 初始位置
    public Vector3 endPosition; // 终止位置

    private float totalDistance; // 到达终止位置所需的总距离
    private float distanceTraveled; // 已经移动的距离

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

      //  speedControl.SelectRandomSpeed();//调用随机选择速度方法
     //   speed = speedControl.Speed;//从速度控制器中调用已选择的速度 
    }

    // Update is called once per frame
    void Update()
    {
        if (movingForward)
        {
            // 如果正在往前运动，则向终止位置运动
            transform.Translate((endPosition - initialPosition).normalized * speed * Time.deltaTime, Space.World);

            // 计算已经移动的距离
            distanceTraveled += speed * Time.deltaTime;

            // 判断是否已经移动了到达终止位置所需的总距离
            if (distanceTraveled >= totalDistance)
            {


                // 重置已经移动的距离
                distanceTraveled = 0f;

                // 切换为往回运动状态
                movingForward = false;
            }
        }
        else
        {
            // 如果正在往回运动，则向初始位置运动
            transform.Translate((initialPosition - endPosition).normalized * speed * Time.deltaTime, Space.World);

            // 计算已经移动的距离
            distanceTraveled += speed * Time.deltaTime;

            // 判断是否已经移动了到达终止位置所需的总距离
            if (distanceTraveled >= totalDistance)
            {


                // 重置已经移动的距离
                distanceTraveled = 0f;

                // 切换为往前运动状态
                movingForward = true;

                // 周期计数器递增
                cycleCount++;

                // 判断是否达到指定的运行周期数
                if (cycleCount >= numCycles)
                {
                    //重置cycleCount初始值并增加currentEpoch
                    cycleCount = 0;
                    currentEpoch++;

                    if(currentEpoch >= numEpochs)
                    {
                        
                       // DateTime timestamp = DateTime.Now;
                       // LogEndTime(timestamp);
                        enabled = false;
                        SceneManager.LoadScene(3);//跳转打分场景
                    }
                    
                    // UnityEditor.EditorApplication.isPlaying = false; // 测试时使用
                    // Application.Quit(); // 正式使用       
                }
            }
        }
    }

   // private static void LogEndTime(DateTime timestamp)
   // {
       // using (StreamWriter writer = new StreamWriter(path, true))
       // {

          //  writer.WriteLine("第" + block + "个block" + "刺激程序结束时间：" + timestamp.ToString());

           // writer.WriteLine("------------");
      //  }
   // }
}
