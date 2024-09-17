using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scorenamespace 
{
    public static class GetScore
    {
        private static int[] Score = new int[100];//�۾���ʹ��������
        private static int i = 0;//���ִ�����ʼΪ0
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
           // Debug.Log("�۾���ʹ���֣�");
           // for (int j = 0; j <= i; j++)
           // { 
                //Debug.Log(Score[j]); 
            //}
                //Debug.Log("------------");
            i++;//ÿ�ε���˵�����ּ�1
        }

        private static void LogScores()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
               
                writer.WriteLine("��" + trail + "��trail" + "�۾���ʹ���֣�");
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
