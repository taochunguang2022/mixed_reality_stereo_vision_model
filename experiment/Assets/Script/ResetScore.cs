using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore_1()
    {
        //�ҵ�Score���ʵ��
        GameObject score1 = GameObject.Find("1");
        Score scoreScript = score1.GetComponent<Score>();

        //����duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_2()
    {
        //�ҵ�Score���ʵ��
        GameObject score2 = GameObject.Find("2");
        Score scoreScript = score2.GetComponent<Score>();

        //����duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_3()
    {
        //�ҵ�Score���ʵ��
        GameObject score3 = GameObject.Find("3");
        Score scoreScript = score3.GetComponent<Score>();

        //����duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_4()
    {
        //�ҵ�Score���ʵ��
        GameObject score4 = GameObject.Find("4");
        Score scoreScript = score4.GetComponent<Score>();

        //����duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_5()
    {
        //�ҵ�Score���ʵ��
        GameObject score5 = GameObject.Find("5");
        Score scoreScript = score5.GetComponent<Score>();

        //����duringTime 
        scoreScript.duringTime = 0;
    }
}
