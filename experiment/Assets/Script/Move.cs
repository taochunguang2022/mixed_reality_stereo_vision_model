using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpeedRecordnamespace;
public class Move : MonoBehaviour
{
    private SpeedControl speedControl = new SpeedControl();//һ��Ҫ���ó�˽�б���������������
    public float speed;

    public List<float> speedList = new List<float> { 1.3f, 1.95f, 2.6f, 3.25f, 4.45f,5.00f };//6���ٶ�
    private float selectedSpeed;
    public List<int> selectedIndices = new List<int>(); // ��¼��ѡ�������
    private static float[] Temp = new float[7];//�ٶ�����
    private static int i = 0;//�ٶȴ���



   
    public int numCycles = 2; // ����������
    float timer = 0f;//��ʱ��
    int cycleCount = 0; // �������ڼ���
    


    bool movingForward = true;//�Ƿ���ǰ�˶�
    // Start is called before the first frame update
    public Vector3 initialPosition;// ��ʼλ��
    public Vector3 endPosition; // ��ֹλ��

    private float totalDistance; // ������ֹλ��������ܾ���
    private float distanceTraveled; // �Ѿ��ƶ��ľ���

   





    void Start()
    {
      // if (SpeedRecordnamespace.SpeedRecord.i == 7) {
           // UnityEditor.EditorApplication.isPlaying = false; // ����ʱʹ��
           // Application.Quit(); // ��ʽʹ��   
      //}
        initialPosition = transform.position;
        TestEnd end = GameObject.FindObjectOfType<TestEnd>();
        endPosition = end.endPosition;
        totalDistance = Vector3.Distance(initialPosition, endPosition);
        distanceTraveled = 0f;
        // speed = SelectRandomSpeed();
        
        speedControl.SelectRandomSpeed();//�������ѡ���ٶȷ���
        speed = speedControl.Speed;//���ٶȿ������е�����ѡ����ٶ�
        //  SpeedRecordnamespace.SpeedRecord.SetSpeed(speed);
        // ���������� RotateWithConstSpeedDir �� speed

        //RotateWithConstSpeedDir rotateScript = FindObjectOfType<RotateWithConstSpeedDir>();
       // if (rotateScript != null)
       // {
            // ����speed��RotateWithConstSpeedDir
          //  rotateScript.SetSpeed(speed);
        //}




    }



   

    // Update is called once per frame
    void Update()
    {
        if (movingForward)
        {
            // ���������ǰ�˶���������ֹλ���˶�
            transform.Translate((endPosition - initialPosition).normalized * speed * Time.deltaTime, Space.World);

            // �����Ѿ��ƶ��ľ���
            distanceTraveled += speed * Time.deltaTime;

            // �ж��Ƿ��Ѿ��ƶ��˵�����ֹλ��������ܾ���
            if (distanceTraveled >= totalDistance)
            {


                // �����Ѿ��ƶ��ľ���
                distanceTraveled = 0f;

                // �л�Ϊ�����˶�״̬
                movingForward = false;
            }
        }
        else
        {
            // ������������˶��������ʼλ���˶�
            transform.Translate((initialPosition - endPosition).normalized * speed * Time.deltaTime, Space.World);

            // �����Ѿ��ƶ��ľ���
            distanceTraveled += speed * Time.deltaTime;

            // �ж��Ƿ��Ѿ��ƶ��˵�����ֹλ��������ܾ���
            if (distanceTraveled >= totalDistance)
            {


                // �����Ѿ��ƶ��ľ���
                distanceTraveled = 0f;

                // �л�Ϊ��ǰ�˶�״̬
                movingForward = true;

                // ���ڼ���������
                cycleCount++;

                // �ж��Ƿ�ﵽָ��������������
                if (cycleCount >= numCycles)
                {
                    enabled = false;
                    SceneManager.LoadScene(1);//��ת�۾���ʹ���ֳ���
                    // UnityEditor.EditorApplication.isPlaying = false; // ����ʱʹ��
                    // Application.Quit(); // ��ʽʹ��       
                }
            }
        }
    }

    
}
