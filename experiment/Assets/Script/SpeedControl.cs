using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeedRecordnamespace;
public class SpeedControl : MonoBehaviour
{
    private List<float> speedList = new List<float> { 5.00f, 5.00f, 5.00f, 5.00f, 5.00f, 5.00f };//6���ٶ�
    private float selectedSpeed;
    private static int i = 0;//��һ��
    public List<int> selectedIndices = new List<int>(); // ��¼��ѡ�������


    public float Speed { get => selectedSpeed; }//get��Speed���Ե�selectedSpeedֵ
                                                //ͨ������������ԣ������������ͨ������ Speed ��������ȡ��ǰ��ѡ����ٶ�ֵ��
   
    public void SelectRandomSpeed()
    {
      //  if (speedList.Count == 0)
     //   {
            // �����ٶȶ���ѡ����ϣ����������Ӧ�߼�����
         //   Debug.Log("�����ٶ���ѡ����ϣ�");
          //  return;
      //  }

        int selectedIndex;
        selectedIndex = Random.Range(0, speedList.Count);
        if (i == 0)
        {
            
            SpeedRecordnamespace.SpeedRecord.SetSpeed(speedList[selectedIndex]);
            selectedSpeed = speedList[selectedIndex]; // ����������ȡ��Ӧ���ٶ�ֵ
            i++;
        }
        else
        {
            do
            {
                //selectedIndex = Random.Range(0, speedList.Count); // ���ѡ��һ������
                selectedIndex = Random.Range(0, speedList.Count);
            }
            while (SpeedRecordnamespace.SpeedRecord.FindSpeed(speedList[selectedIndex])); // ��������ѱ�ѡ�����������ѡ��
            selectedSpeed = speedList[selectedIndex]; // ����������ȡ��Ӧ���ٶ�ֵ
            SpeedRecordnamespace.SpeedRecord.SetSpeed(selectedSpeed);
        }

        

       



        // selectedIndex = Random.Range(0, speedList.Count);// ���ѡ��һ������
        // if (SpeedRecordnamespace.SpeedRecord.FindSpeed(speedList[selectedIndex]))//���֮ǰ�Ѿ�ѡ���˸�ֵ
        //   {
        //  selectedIndex = Random.Range(0, 7);//����ѡһ������
        //  }
        //  else
        //  {
        // SpeedRecordnamespace.SpeedRecord.SetSpeed(speedList[selectedIndex]);

        //  }
       // selectedIndices.Add(selectedIndex); // ��¼��ѡ�������
       // selectedSpeed = speedList[selectedIndex]; // ����������ȡ��Ӧ���ٶ�ֵ
       // speedList.RemoveAt(selectedIndex); // �Ƴ���ѡ���ٶ�
      //  SpeedRecordnamespace.SpeedRecord.SetSpeed(selectedSpeed);

    }

}
