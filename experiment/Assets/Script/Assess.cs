using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Assess : MonoBehaviour
{
    //疲劳程度量表打分
    public float duringTime = 0;//方块打分时间，眼睛乱飘会被重置为0
   
   



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
        if (duringTime >= 3.0f)//选中5秒后加分1
        {
            Assessnamespace.GetAssess.setAssess(1);
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_2()
    {

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中5秒后加分2
        {
            
            Assessnamespace.GetAssess.setAssess(2);
           
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_3()
    {
    

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中5秒后加分3
        {
           
            Assessnamespace.GetAssess.setAssess(3);
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_4()
    {
       

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中5秒后加分4
        {
           
            Assessnamespace.GetAssess.setAssess(4);
            SceneManager.LoadScene(1);
        }
    }

    public void Assess_5()
    {
        

        duringTime += Time.deltaTime;
        if (duringTime >= 3.0f)//选中5秒后加分5
        {
            
            Assessnamespace.GetAssess.setAssess(5);
            SceneManager.LoadScene(1);
        }
    }


   



}
