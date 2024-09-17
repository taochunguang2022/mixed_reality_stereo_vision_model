using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assessnamespace
{
    public static class GetAssess
    {
        private static int[] Assess = new int[100];//ƣ�ͳ̶ȴ������
        private static int i = 0;//���ִ�����ʼΪ0
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
            i++;//ÿ�ε���˵�����ּ�1
        }

        private static void LogAssess()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {

                writer.WriteLine("��" + block + "��block" + "ƣ�ͳ̶����֣�");
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
                // ����ļ��Ƿ���ڣ������Ǵ������ļ�
                if (File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    // ����ļ������ڣ����Դ����ļ�������·���Ƿ���Ч
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
