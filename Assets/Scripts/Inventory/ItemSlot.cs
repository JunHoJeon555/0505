using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class ItemSlot
{
    //절대로 변하지 않는다
    /// <summary>
    /// 이 슬롯의 인덱스(인벤토리에서 몇번째 슬롯인지)
    /// </summary>
    uint slotIndex;
    public uint Index => slotIndex; //오로지 읽기 전용

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 종류
    /// </summary>
    ItemData slotItemData = null;
    public ItemData ItemData
    {
        get => slotItemData;
        private set     //쓰기는 이 클래스만 가능
        {
            if(slotItemData != value)   //아이템 종류가 변경되었을 때만  대입
            {
                slotItemData= value;
            }
        }
    }

    /// <summary>
    /// 슬롯이 비었는지 확인한느 프로퍼티. true면 비어있고 false면 아이템이 들어있다.
    /// </summary>
    public bool IsEmpty => slotItemData == null;

    /// <summary>
    /// 이 슬롯에 들어있는 아이템의 갯수  ISEmpty면 0개
    /// </summary>
    uint itemCount = 0;
    public uint ItemCount
    {
        get => itemCount;
        private set
        {
            if(itemCount != value)
            {
                itemCount= value;
            }
        }
    }

    /// <summary>
    /// 생성자 
    /// </summary>
    /// <param name="index">이 슬롯이 인벤토리에서 몇번째 슬롯인지 (인덱스)</param>
    public ItemSlot(uint index)
    {
        slotIndex = index;  //sloatIndex는 이 이후로 절대 변경하지 않아야한다.
    }


    /// <summary>
    /// 아이템 설정하는 함수
    /// </summary>
    /// <param name="data">설정할 아이템 종류</param>
    /// <param name="count">설정할 아이템 갯수</param>
    public void AssignSlotItem(ItemData data, uint count =1) //uint count =1 디폴트 파라메터  
    {
        if(data != null)
        {
            ItemData = data;        //data가 null이 아니면 파라메터대로 설정
            ItemCount = count;
            Debug.Log($"인벤토리{slotIndex}슬롯에 \"{ItemData.itemName}\" 아이템이 {ItemCount}개 설정");
        }
        else
        {
            ClearSlotItem();        //data가 null이면 해당 슬롯은 초기화
        }
    }
    //아이템 비우기
    public void ClearSlotItem()
    {
        ItemCount= 0;
        ItemData = null;
        Debug.Log($"인벤토리 {slotIndex}슬롯을 비웁니다.");

    }

    //아이템 갯수 증가시키기
    /// <summary>
    /// 아이템 갯수 증가시키는 함수
    /// </summary>
    /// <param name="overCount">추가하다가 넘친 갯수</param>
    /// <param name="increseCount">증가시킬 갯수</param>
    /// <returns>증가 성공여부(true면 성공, flase면 넘쳤다)</returns>
    public bool IncreaseSlotItem(out uint overCount, uint increseCount = 1)
    {
        bool result;
        int over;

        uint newCount = ItemCount + increseCount;
        over =  (int)newCount - (int)ItemData.maxStackCount;

        // 넘치느냐 안넘치느냐를 판별하기
        if (over > 0)
        {
            // 넘친 경우 대한 처리
            ItemCount = ItemData.maxStackCount;
            overCount = (uint)over;
            result = false;
            Debug.Log($"인벤토리 {slotIndex}\"{ItemData.itemName}\" 아이템이 최대치까지 증가. 현재 {ItemCount}.{over}개 넘침.");
        }

        else
        {
            // 안 넘친 경우에 대한 처리
            ItemCount = newCount;
            overCount = 0;
            Debug.Log($"인벤토리 {slotIndex}\"{ItemData.itemName}\" 개 증가. 현재 {ItemCount}개.");
            result = true;
        }


        return result;
    }



    //아이템 갯수 감소시키기
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lowCount"></param>
    /// <param name="decreaseCount"></param>
    public void DecreaseSlotItem(uint decreaseCount = 1)
    {
        int newCount = (int)ItemCount - (int)decreaseCount;

        if (newCount <1) 
        {
            //새 갯수가 0개 이하
            ClearSlotItem();
        }
        else
        {
            ItemCount = (uint)newCount;
            Debug.Log($"인벤토리 {slotIndex}\"{ItemData.itemName}\"아이템이{decreaseCount}개 감소 현재{ItemCount}개.");
        }
    }


}
