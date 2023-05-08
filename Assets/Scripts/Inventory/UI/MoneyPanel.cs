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
    /// 플레이어의 돈이 변경될 때 실행되는 함수
    /// </summary>
    /// <param name="money">현재 플레이어가 가지고 있는 돈 (표시할 돈)</param>
    public void Refresh(int money)
    {
        moneyText.text = $"{money:N0}";
        //moneyText.text = string.Format("{0:#,0}", money);

        //이 함수가 실행 될 때 마다 
        //moneyPanel에서 표시하는 돈이 money로 설정된다.
        //3자리마다 ,를 표시되게 한다. (10,000)
    }
}
