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
        //找到Score类的实例
        GameObject score1 = GameObject.Find("1");
        Score scoreScript = score1.GetComponent<Score>();

        //重置duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_2()
    {
        //找到Score类的实例
        GameObject score2 = GameObject.Find("2");
        Score scoreScript = score2.GetComponent<Score>();

        //重置duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_3()
    {
        //找到Score类的实例
        GameObject score3 = GameObject.Find("3");
        Score scoreScript = score3.GetComponent<Score>();

        //重置duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_4()
    {
        //找到Score类的实例
        GameObject score4 = GameObject.Find("4");
        Score scoreScript = score4.GetComponent<Score>();

        //重置duringTime 
        scoreScript.duringTime = 0;
    }

    public void ResetScore_5()
    {
        //找到Score类的实例
        GameObject score5 = GameObject.Find("5");
        Score scoreScript = score5.GetComponent<Score>();

        //重置duringTime 
        scoreScript.duringTime = 0;
    }
}
