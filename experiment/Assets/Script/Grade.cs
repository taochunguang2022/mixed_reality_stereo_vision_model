using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gradenamespace;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class Grade : MonoBehaviour
{
    //舒适度打分
    public float duringTime = 0;
   // bool isSelect = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grade_1()
    {
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分1
        {
            Gradenamespace.GetGrade.setGrade(1);
           
            SceneManager.LoadScene(3);
        }
    }

    public void Grade_2()
    {
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分2
        {
            Gradenamespace.GetGrade.setGrade(2);
           
            SceneManager.LoadScene(3);
        }
    }

    public void Grade_3()
    {
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分3
        {
            Gradenamespace.GetGrade.setGrade(3);
            
            SceneManager.LoadScene(3);
        }
    }

    public void Grade_4()
    {
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分4
        {
            Gradenamespace.GetGrade.setGrade(4);
            
            SceneManager.LoadScene(3);
        }
    }

    public void Grade_5()
    {
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中3秒后加分5
        {
            Gradenamespace.GetGrade.setGrade(5);
            
            SceneManager.LoadScene(3);
        }
    }


}
