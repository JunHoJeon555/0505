using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpliterUI : MonoBehaviour
{
    /// <summary>
    /// 아이템 나눌 최소 갯수(나누기를 시도하면 최소1개를 나눈다)
    /// </summary>
    const int itemCountMin = 1;

    /// <summary>
    /// 아이템을 실제로 분리할 갯수
    /// </summary>
    uint itemSplitCount = itemCountMin;
    uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            if(itemSplitCount != value)
            {
                itemSplitCount = (uint)Mathf.Clamp((int)value,itemCountMin,(int)(targetSlot.ItemCount-1));

                inputField.text = itemSplitCount.ToString();
                slider.value = itemSplitCount;
            }
        }
    }
    /// <summary>
    /// 아이템을 나눌 슬롯
    /// </summary>
    ItemSlot targetSlot;

    /// <summary>
    /// 아이템을 분리할 갯수를 직접입력할 수 있는 인풋 필드
    /// </summary>
    TMP_InputField inputField;

    /// <summary>
    /// 아이템 분리 갯수를 조절할 수 있는 슬라이더
    /// </summary>
    Slider slider;

    /// <summary>
    /// 분리할 아이템의 아이콘
    /// </summary>
    Image itemImage;

    /// <summary>
    /// ok버튼을 눌렀을 때 실행되는 함수
    /// 파라메타(슬롯의 인덱스, 나눌갯수)
    /// </summary>
    public Action<uint, uint> onOkClick;


    // 1. awake에서 필요한 컴포넌트 찾기
    // 2. 인풋필드와 슬라이더를 연동시키기(하나가 바뀌면 다른 하나도 같이 변경되어야 한다.)
    // 3. OK 버튼을 누르면 ItemSplitCount를 디버그 창에 출력하기
    // 4. Cancel 버튼을 누르면 "취소"라고 디버그 창에 출력하기
    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        slider = GetComponentInChildren<Slider>();
        //inputField.onValueChanged.AddListener((text)) =>
            //if(uint.TryParse())


        Transform child = transform.GetChild(0);
        
        itemImage= child.GetComponent<Image>();
        
    }

    
    private void Start()
    {
        //slider.onValueChanged.AddListener(delegate { valueChanged(slider, inputField)});
    }

    private void Update()
    {
        
        slider.value = int.Parse(inputField.text);
    }

    public void Open(ItemSlot target)
    {
        targetSlot = target;
        ItemSplitCount = 1;
        itemImage.sprite = targetSlot.ItemData.itemIcon;
        slider.minValue = itemCountMin;
        slider.maxValue = target.ItemCount -1;
        gameObject.SetActive(true);
    }
        
    public void valueChanged(Slider slider , TMP_InputField inputField)
    {
        //int value = (int)(diff * slider.value);
    }






}
