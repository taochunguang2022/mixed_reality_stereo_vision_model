using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAssess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAssess_1()
    {
        
        
        //找到Assess类的实例
        GameObject assess1 = GameObject.Find("1");
        Assess assessScript = assess1.GetComponent<Assess>();

        //重置duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_2()
    {
        
        //找到Assess类的实例
        GameObject assess2 = GameObject.Find("2");
        Assess assessScript = assess2.GetComponent<Assess>();

        //重置duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_3()
    {
        

        //找到Assess类的实例
        GameObject assess3 = GameObject.Find("3");
        Assess assessScript = assess3.GetComponent<Assess>();

        //重置duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_4()
    {
        
        //找到Assess类的实例
        GameObject assess4 = GameObject.Find("4");
        Assess assessScript = assess4.GetComponent<Assess>();

        //重置duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_5()
    {
        
        //找到Assess类的实例
        GameObject assess5 = GameObject.Find("5");
        Assess assessScript = assess5.GetComponent<Assess>();

        //重置duringTime 
        assessScript.duringTime = 0;
    }

   
}
