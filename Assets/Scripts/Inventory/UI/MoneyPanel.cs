using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    TextMeshProUGUI moneyText;

    private void Awake()
    {
        moneyText= GetComponentInChildren<TextMeshProUGUI>();
    }
    

    /// <summary>
    /// �÷��̾��� ���� ����� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="money">���� �÷��̾ ������ �ִ� �� (ǥ���� ��)</param>
    public void Refresh(int money)
    {
        moneyText.text = $"{money:N0}";
        //moneyText.text = string.Format("{0:#,0}", money);

        //�� �Լ��� ���� �� �� ���� 
        //moneyPanel���� ǥ���ϴ� ���� money�� �����ȴ�.
        //3�ڸ����� ,�� ǥ�õǰ� �Ѵ�. (10,000)
    }
}
