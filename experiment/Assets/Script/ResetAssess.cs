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
        
        
        //�ҵ�Assess���ʵ��
        GameObject assess1 = GameObject.Find("1");
        Assess assessScript = assess1.GetComponent<Assess>();

        //����duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_2()
    {
        
        //�ҵ�Assess���ʵ��
        GameObject assess2 = GameObject.Find("2");
        Assess assessScript = assess2.GetComponent<Assess>();

        //����duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_3()
    {
        

        //�ҵ�Assess���ʵ��
        GameObject assess3 = GameObject.Find("3");
        Assess assessScript = assess3.GetComponent<Assess>();

        //����duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_4()
    {
        
        //�ҵ�Assess���ʵ��
        GameObject assess4 = GameObject.Find("4");
        Assess assessScript = assess4.GetComponent<Assess>();

        //����duringTime 
        assessScript.duringTime = 0;
    }

    public void ResetAssess_5()
    {
        
        //�ҵ�Assess���ʵ��
        GameObject assess5 = GameObject.Find("5");
        Assess assessScript = assess5.GetComponent<Assess>();

        //����duringTime 
        assessScript.duringTime = 0;
    }

   
}
