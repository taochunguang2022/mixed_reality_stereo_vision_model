using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeedRecordnamespace;
public class SpeedControl : MonoBehaviour
{
    private List<float> speedList = new List<float> { 5.00f, 5.00f, 5.00f, 5.00f, 5.00f, 5.00f };//6种速度
    private float selectedSpeed;
    private static int i = 0;//第一次
    public List<int> selectedIndices = new List<int>(); // 记录已选择的索引


    public float Speed { get => selectedSpeed; }//get到Speed属性的selectedSpeed值
                                                //通过定义这个属性，其他代码可以通过访问 Speed 属性来获取当前被选择的速度值。
   
    public void SelectRandomSpeed()
    {
      //  if (speedList.Count == 0)
     //   {
            // 所有速度都已选择完毕，可以添加相应逻辑处理
         //   Debug.Log("所有速度已选择完毕！");
          //  return;
      //  }

        int selectedIndex;
        selectedIndex = Random.Range(0, speedList.Count);
        if (i == 0)
        {
            
            SpeedRecordnamespace.SpeedRecord.SetSpeed(speedList[selectedIndex]);
            selectedSpeed = speedList[selectedIndex]; // 根据索引获取对应的速度值
            i++;
        }
        else
        {
            do
            {
                //selectedIndex = Random.Range(0, speedList.Count); // 随机选择一个索引
                selectedIndex = Random.Range(0, speedList.Count);
            }
            while (SpeedRecordnamespace.SpeedRecord.FindSpeed(speedList[selectedIndex])); // 如果索引已被选择过，则重新选择
            selectedSpeed = speedList[selectedIndex]; // 根据索引获取对应的速度值
            SpeedRecordnamespace.SpeedRecord.SetSpeed(selectedSpeed);
        }

        

       



        // selectedIndex = Random.Range(0, speedList.Count);// 随机选择一个索引
        // if (SpeedRecordnamespace.SpeedRecord.FindSpeed(speedList[selectedIndex]))//如果之前已经选过了该值
        //   {
        //  selectedIndex = Random.Range(0, 7);//重新选一个索引
        //  }
        //  else
        //  {
        // SpeedRecordnamespace.SpeedRecord.SetSpeed(speedList[selectedIndex]);

        //  }
       // selectedIndices.Add(selectedIndex); // 记录已选择的索引
       // selectedSpeed = speedList[selectedIndex]; // 根据索引获取对应的速度值
       // speedList.RemoveAt(selectedIndex); // 移除已选的速度
      //  SpeedRecordnamespace.SpeedRecord.SetSpeed(selectedSpeed);

    }

}
