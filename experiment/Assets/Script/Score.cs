using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scorenamespace;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class Score : MonoBehaviour
{
    //眼睛酸痛情况打分
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
        if (duringTime >= 3.0f)//选中3秒后加分1
        {
            Scorenamespace.GetScore.setScore(1);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_2()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分2
        {
            Scorenamespace.GetScore.setScore(2);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_3()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分3
        {
            Scorenamespace.GetScore.setScore(3);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_4()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分4
        {
            Scorenamespace.GetScore.setScore(4);
            SceneManager.LoadScene(2);
        }
    }
    public void Score_5()
    {
       
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分5
        {
            Scorenamespace.GetScore.setScore(5);
            SceneManager.LoadScene(2);
        }
    }
}
