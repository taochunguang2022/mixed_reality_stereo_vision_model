using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scorenamespace;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class Score : MonoBehaviour
{
    //�۾���ʹ������
    public float duringTime = 0;
    //bool isSelect=true;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void Score_1()
    {

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��3���ӷ�1
        {
            Scorenamespace.GetScore.setScore(1);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_2()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��3���ӷ�2
        {
            Scorenamespace.GetScore.setScore(2);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_3()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��3���ӷ�3
        {
            Scorenamespace.GetScore.setScore(3);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_4()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��3���ӷ�4
        {
            Scorenamespace.GetScore.setScore(4);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_5()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��3���ӷ�5
        {
            Scorenamespace.GetScore.setScore(5);
            SceneManager.LoadScene(2);
        }
    }
}
