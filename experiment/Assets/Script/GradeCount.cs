using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Gradenamespace
{
   
    public static class GetGrade
    {
        private static int[] Grade = new int[100];//���ʶ�״����������
        private static int i = 0;//���ʶ����ִ���
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
          //  Debug.Log("���ʶ����֣�");
           // for (int j = 0; j <= i; j++)
           // {
               // Debug.Log(Grade[j]);   
           // }
            //Debug.Log("------------");
            i++;//ÿ�ε��ü�˵������1��
        }

        private static void LogGrade()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
               
                writer.WriteLine("��" + trail + "��trail" + "���ʶ����֣�");
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
