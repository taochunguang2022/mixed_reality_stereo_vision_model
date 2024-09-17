using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeColor : MonoBehaviour
{
    private static readonly int _baseColor = Shader.PropertyToID("_BaseColor");
    public Color highlightColor = Color.red;
    public float animationTime = 0.1f;
    //float duringTime = 0;
    private Renderer _renderer;
    private Color _originalColor;
    private Color _targetColor;

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void ColorChanged()
    {
      // duringTime += Time.deltaTime;
       // if(duringTime >= 4.0f)
       // {
            _targetColor = highlightColor;
            //�ȴ���ӳʱ���ܿ�������ɫ���У��м���һ��ʱ��
          //  StartCoroutine(WaitAndDoSomething());
           // SceneManager.LoadScene(6);//��ת��������
       // }
            //SceneManager.LoadScene(1);//��ת����
        //print(duringTime);
    }

    // IEnumerator WaitAndDoSomething()
    // {
    //   yield return new WaitForSeconds(1.0f); // �ȴ�1.0��
    // ������д�ȴ�����Ҫִ�еĴ���
    // }

    public void ColorReset()
    {
        _targetColor = _originalColor;
    }

    private void Start()
    {
         _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        
        _targetColor = _originalColor;
    }

    private void Update()
    {
        //This lerp will fade the color of the object
        if (_renderer.material.HasProperty(_baseColor)) // new rendering pipeline (lightweight, hd, universal...)
        {
            _renderer.material.SetColor(_baseColor, Color.Lerp(_renderer.material.GetColor(_baseColor), _targetColor, Time.deltaTime * (1 / animationTime)));
        }
        else // old standard rendering pipline
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / animationTime));
        }
    }
}
