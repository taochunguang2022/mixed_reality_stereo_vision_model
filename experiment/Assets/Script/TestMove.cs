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
    
    public int numCycles = 2; // ����������
    float timer = 0f;//��ʱ��
    int cycleCount = 0; // �������ڼ���

    bool movingForward = true;//�Ƿ���ǰ�˶�
    bool isLookedAt = false; // �Ƿ�ע��
                            
    public Vector3 initialPosition; // ��ʼλ��
    public Vector3 endPosition; // ��ֹλ��

    private float totalDistance; // ������ֹλ��������ܾ���
    private float distanceTraveled; // �Ѿ��ƶ��ľ���

    

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
        if (!isLookedAt) return; // ���û�б�ע�ӣ���ֱ�ӷ���

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
                    enabled = false;
                    //SceneManager.LoadScene(1);//��ת�۾���ʹ���ֳ���
                    // UnityEditor.EditorApplication.isPlaying = false; // ����ʱʹ��
                    Application.Quit(); // ��ʽʹ��       
                }
            }
        }
    }

    // ��ע��ʱ���õĺ���
    public void WhileLookingAtTarget()
    {
        isLookedAt = true;
    }

    // �۾��뿪Ŀ��ʱ���õĺ���
    public void OnLookAway()
    {
        isLookedAt = false;
    }

}
