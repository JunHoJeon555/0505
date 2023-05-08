using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : BarBase
{

    private void Start()
    {
        Player player = GameManager.Inst.Player;    //�÷��̾� ã��
        maxValue = player.MaxMP;                    //�ִ� �� ����
        max.text = $"/ {maxValue}";                 //�ִ� �� ǥ��
        current.text = player.HP.ToString();        //���� HP ����

        slider.value = player.MP / maxValue;        //�����̴� �� ����
        player.onManaChange += onValueChange;    //HP ����� �� ����� �Լ� ���

    }

   

}
