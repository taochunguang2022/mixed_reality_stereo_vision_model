using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Assess : MonoBehaviour
{
    //ƣ�ͳ̶�������
    public float duringTime = 0;//������ʱ�䣬�۾���Ʈ�ᱻ����Ϊ0
   
   



    //private static string path = @"D:\test\timeLog.txt";
    //private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test23_4d4kmw1bzqv36\LocalState\timeLog.txt";

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Assess_1()
    {
     
        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��5���ӷ�1
        {
            Assessnamespace.GetAssess.setAssess(1);
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_2()
    {

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��5���ӷ�2
        {
            
            Assessnamespace.GetAssess.setAssess(2);
           
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_3()
    {
    

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��5���ӷ�3
        {
           
            Assessnamespace.GetAssess.setAssess(3);
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_4()
    {
       

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��5���ӷ�4
        {
           
            Assessnamespace.GetAssess.setAssess(4);
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_5()
    {
        

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//ѡ��5���ӷ�5
        {
            
            Assessnamespace.GetAssess.setAssess(5);
            SceneManager.LoadScene(1);
        }
    }


   



}
