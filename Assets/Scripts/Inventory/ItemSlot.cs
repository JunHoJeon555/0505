using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class ItemSlot
{
    //����� ������ �ʴ´�
    /// <summary>
    /// �� ������ �ε���(�κ��丮���� ���° ��������)
    /// </summary>
    uint slotIndex;
    public uint Index => slotIndex; //������ �б� ����

    /// <summary>
    /// �� ���Կ� ����ִ� ������ ����
    /// </summary>
    ItemData slotItemData = null;
    public ItemData ItemData
    {
        get => slotItemData;
        private set     //����� �� Ŭ������ ����
        {
            if(slotItemData != value)   //������ ������ ����Ǿ��� ����  ����
            {
                slotItemData= value;
            }
        }
    }

    /// <summary>
    /// ������ ������� Ȯ���Ѵ� ������Ƽ. true�� ����ְ� false�� �������� ����ִ�.
    /// </summary>
    public bool IsEmpty => slotItemData == null;

    /// <summary>
    /// �� ���Կ� ����ִ� �������� ����  ISEmpty�� 0��
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
    /// ������ 
    /// </summary>
    /// <param name="index">�� ������ �κ��丮���� ���° �������� (�ε���)</param>
    public ItemSlot(uint index)
    {
        slotIndex = index;  //sloatIndex�� �� ���ķ� ���� �������� �ʾƾ��Ѵ�.
    }


    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="data">������ ������ ����</param>
    /// <param name="count">������ ������ ����</param>
    public void AssignSlotItem(ItemData data, uint count =1) //uint count =1 ����Ʈ �Ķ����  
    {
        if(data != null)
        {
            ItemData = data;        //data�� null�� �ƴϸ� �Ķ���ʹ�� ����
            ItemCount = count;
            Debug.Log($"�κ��丮{slotIndex}���Կ� \"{ItemData.itemName}\" �������� {ItemCount}�� ����");
        }
        else
        {
            ClearSlotItem();        //data�� null�̸� �ش� ������ �ʱ�ȭ
        }
    }
    //������ ����
    public void ClearSlotItem()
    {
        ItemCount= 0;
        ItemData = null;
        Debug.Log($"�κ��丮 {slotIndex}������ ���ϴ�.");

    }

    //������ ���� ������Ű��
    /// <summary>
    /// ������ ���� ������Ű�� �Լ�
    /// </summary>
    /// <param name="overCount">�߰��ϴٰ� ��ģ ����</param>
    /// <param name="increseCount">������ų ����</param>
    /// <returns>���� ��������(true�� ����, flase�� ���ƴ�)</returns>
    public bool IncreaseSlotItem(out uint overCount, uint increseCount = 1)
    {
        bool result;
        int over;

        uint newCount = ItemCount + increseCount;
        over =  (int)newCount - (int)ItemData.maxStackCount;

        // ��ġ���� �ȳ�ġ���ĸ� �Ǻ��ϱ�
        if (over > 0)
        {
            // ��ģ ��� ���� ó��
            ItemCount = ItemData.maxStackCount;
            overCount = (uint)over;
            result = false;
            Debug.Log($"�κ��丮 {slotIndex}\"{ItemData.itemName}\" �������� �ִ�ġ���� ����. ���� {ItemCount}.{over}�� ��ħ.");
        }

        else
        {
            // �� ��ģ ��쿡 ���� ó��
            ItemCount = newCount;
            overCount = 0;
            Debug.Log($"�κ��丮 {slotIndex}\"{ItemData.itemName}\" �� ����. ���� {ItemCount}��.");
            result = true;
        }


        return result;
    }



    //������ ���� ���ҽ�Ű��
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
            //�� ������ 0�� ����
            ClearSlotItem();
        }
        else
        {
            ItemCount = (uint)newCount;
            Debug.Log($"�κ��丮 {slotIndex}\"{ItemData.itemName}\"��������{decreaseCount}�� ���� ����{ItemCount}��.");
        }
    }


}
