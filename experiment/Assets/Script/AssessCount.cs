using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assessnamespace
{
    public static class GetAssess
    {
        private static int[] Assess = new int[100];//疲劳程度打分数组
        private static int i = 0;//评分次数初始为0
       // private static string path = @"D:\test\assessLog.txt";
        private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test30_4d4kmw1bzqv36\LocalState\assessLog.txt";
        private static int block = 0;

        public static void setAssess(int assess)
        {
            Assess[i] = assess;
            if (IsValidPath(path))
            {
                block++;

                LogAssess();
            }
            i++;//每次调用说明评分加1
        }

        private static void LogAssess()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {

                writer.WriteLine("第" + block + "个block" + "疲劳程度评分：");
                for (int j = 0; j <= i; j++)
                {
                    writer.WriteLine(Assess[j].ToString());
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
