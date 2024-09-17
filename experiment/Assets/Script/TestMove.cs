using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TestMove : MonoBehaviour
{
    public float speed = 0.65f;
    
    public int numCycles = 2; // 运行周期数
    float timer = 0f;//计时器
    int cycleCount = 0; // 运行周期计数

    bool movingForward = true;//是否往前运动
    bool isLookedAt = false; // 是否被注视
                            
    public Vector3 initialPosition; // 初始位置
    public Vector3 endPosition; // 终止位置

    private float totalDistance; // 到达终止位置所需的总距离
    private float distanceTraveled; // 已经移动的距离

    

    void Start()
    {
        initialPosition = transform.position;
        TestEnd end = GameObject.FindObjectOfType<TestEnd>();
        endPosition = end.endPosition;
        totalDistance = Vector3.Distance(initialPosition, endPosition);
        distanceTraveled = 0f;
    }



    // Update is called once per frame
    void Update()
    {
        if (!isLookedAt) return; // 如果没有被注视，则直接返回

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
                    enabled = false;
                    //SceneManager.LoadScene(1);//跳转眼睛酸痛评分场景
                    // UnityEditor.EditorApplication.isPlaying = false; // 测试时使用
                    Application.Quit(); // 正式使用       
                }
            }
        }
    }

    // 被注视时调用的函数
    public void WhileLookingAtTarget()
    {
        isLookedAt = true;
    }

    // 眼睛离开目标时调用的函数
    public void OnLookAway()
    {
        isLookedAt = false;
    }

}
