using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGrade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGrade_1()
    {
        //�ҵ�Grade���ʵ��
        GameObject grade1 = GameObject.Find("1");
        Grade gradeScript = grade1.GetComponent<Grade>();

        //����duringTime
        gradeScript.duringTime = 0;
    }

    public void ResetGrade_2()
    {
        //�ҵ�Grade���ʵ��
        GameObject grade2 = GameObject.Find("2");
        Grade gradeScript = grade2.GetComponent<Grade>();

        //����duringTime
        gradeScript.duringTime = 0;
    }

    public void ResetGrade_3()
    {
        //�ҵ�Grade���ʵ��
        GameObject grade3 = GameObject.Find("3");
        Grade gradeScript = grade3.GetComponent<Grade>();

        //����duringTime
        gradeScript.duringTime = 0;
    }

    public void ResetGrade_4()
    {
        //�ҵ�Grade���ʵ��
        GameObject grade4 = GameObject.Find("4");
        Grade gradeScript = grade4.GetComponent<Grade>();

        //����duringTime
        gradeScript.duringTime = 0;
    }

    public void ResetGrade_5()
    {
        //�ҵ�Grade���ʵ��
        GameObject grade5 = GameObject.Find("5");
        Grade gradeScript = grade5.GetComponent<Grade>();

        //����duringTime
        gradeScript.duringTime = 0;
    }
}
