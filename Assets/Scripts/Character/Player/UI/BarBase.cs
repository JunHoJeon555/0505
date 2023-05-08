using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//������ ǥ�ÿ� ����  ���� �θ� Ŭ����
public class BarBase : MonoBehaviour
{
    /// <summary>
    /// ǥ�õ� ����
    /// </summary>
    public Color color = Color.white;

    protected TextMeshProUGUI current;
    protected TextMeshProUGUI max;
    protected Slider slider;

    /// <summary>
    /// ǥ���� ���� �ִ밪 
    /// </summary>
    protected float maxValue;


    protected virtual void Awake()
    {      
        slider = GetComponent<Slider>();
        Transform child = transform.GetChild(2);
        current = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(3);
        max = child.GetComponentInChildren<TextMeshProUGUI>();

        //���������ϱ�. ��� ���� fill ���� ������ ���ĸ� ���� 
        child = transform.GetChild(1);
        Image fillImage = child.GetComponentInChildren<Image>();
        fillImage.color = color;

        child = transform.GetChild(0);
        Image bgImage = child.GetComponentInChildren<Image>();
        Color bgColor = new Color(color.r, color.g , color.b , color.a *0.5f);
        bgImage.color = bgColor;
    }
    /// <summary>
    /// ���� ����Ǹ� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="ratio">�����̴����� ǥ�õ� ����</param>
    protected void onValueChange(float ratio)
    {
        ratio = Mathf.Clamp01(ratio);               //ratio�� ������ 0~1 ���̷�
        slider.value = ratio;                       //�����̴� ����
        current.text = $"{(ratio * maxValue):f0}";  //�ؽ�Ʈ ���

    }
}
