using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Gradenamespace
{
   
    public static class GetGrade
    {
        private static int[] Grade = new int[100];//舒适度状况评分数组
        private static int i = 0;//舒适度评分次数
      private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test22_4d4kmw1bzqv36\LocalState\gradeLog.txt";
        //private static string path = Path.Combine(Application.persistentDataPath + @"/ASSESS2", "gradeLog.txt");
      //private static string path = @"D:\test\gradeLog.txt";
        private static int trail = 0;

        public static void setGrade(int grade)
        {
            Grade[i] = grade;
           if (IsValidPath(path))
           {
                trail++;

                LogGrade();
           }
          //  Debug.Log("舒适度评分：");
           // for (int j = 0; j <= i; j++)
           // {
               // Debug.Log(Grade[j]);   
           // }
            //Debug.Log("------------");
            i++;//每次调用即说明评分1次
        }

        private static void LogGrade()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
               
                writer.WriteLine("第" + trail + "次trail" + "舒适度评分：");
                for (int j = 0; j <= i; j++)
                {
                    writer.WriteLine(Grade[j].ToString());
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
