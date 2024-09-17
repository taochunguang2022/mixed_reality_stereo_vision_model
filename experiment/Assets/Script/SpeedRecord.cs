using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace SpeedRecordnamespace
{
    public static class SpeedRecord 
    {
        private static float[] SpeedList = new float[7];//记录速度
        public static int i = 0;
       private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test17_4d4kmw1bzqv36\LocalState\speedLog.txt";
       //private static string path = @"D:\test\speedLog.txt";
        private static int trail = 0;
        
        public static void SetSpeed(float speed)
        {
            SpeedList[i] = speed;
          if (IsValidPath(path))
         {
                trail++;
               
                LogSpeed();
         }
          //  Debug.Log("当前速度："+ SpeedList[i]);
            i++;
        }
        public static bool FindSpeed(float findspeed) 
        {
            if (i == 0) { 
                return false; 
            }
            for (int j = 0; j < i; j++)
            {
                if(findspeed== SpeedList[j]) return true;
            }
            return false;
        }
        //public static float GetSpeed()
        //{
        //    Debug.Log("取出速度：" + SpeedList[i-1]);
        //    return SpeedList[i-1];
        //}

        private static void LogSpeed()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
               
                writer.WriteLine( "第"+trail+"次trail"+"记录的速度：");
                for(int j = 0; j <= i; j++)
                {
                    writer.WriteLine(SpeedList[j].ToString());
                }
                writer.WriteLine("------------");
            }
        }

        private static bool IsValidPath(string filePath)
        {
            try
            {
                // 检查文件是否存在，而不是创建新文件
                if (File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    // 如果文件不存在，尝试创建文件来测试路径是否有效
                    using (FileStream fs = File.Create(filePath))
                    {
                        fs.Close();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}
