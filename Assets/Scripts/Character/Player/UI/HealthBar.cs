using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : BarBase
{
    private void Start()
    {
        Player player = GameManager.Inst.Player;    //�÷��̾� ã��
        maxValue = player.MaxHP;                    //�ִ� �� ����
        max.text = $"/ {maxValue}";                 //�ִ� �� ǥ��
        current.text = player.HP.ToString();        //���� HP ����
            
        slider.value = player.HP / maxValue;        //�����̴� �� ����
        player.onHealthChange += onValueChange;    //HP ����� �� ����� �Լ� ���

    }

}
