using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scorenamespace 
{
    public static class GetScore
    {
        private static int[] Score = new int[100];//眼睛酸痛评分数组
        private static int i = 0;//评分次数初始为0
        //private static string path = Path.Combine(Application.persistentDataPath + @"/ASSESS1", "scoreLog.txt");
        private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test29_4d4kmw1bzqv36\LocalState\scoreLog.txt";
       //private static string path = @"D:\test\scoreLog.txt";
        private static int trail = 0;

        public static void setScore(int score)
        {
            Score[i] = score;
           if (IsValidPath(path))
           {
                trail++;

                LogScores();
           }
           // Debug.Log("眼睛酸痛评分：");
           // for (int j = 0; j <= i; j++)
           // { 
                //Debug.Log(Score[j]); 
            //}
                //Debug.Log("------------");
            i++;//每次调用说明评分加1
        }

        private static void LogScores()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
               
                writer.WriteLine("第" + trail + "次trail" + "眼睛酸痛评分：");
                for (int j = 0; j <= i; j++)
                {
                    writer.WriteLine(Score[j].ToString());
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
