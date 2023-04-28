using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemSlotUI_Base : MonoBehaviour
{
    /// <summary>
    /// �κ��丮�� �� ��° ���԰� ����Ǿ�����. ���° ��������.
    /// </summary>
    uint id;
    public uint ID => id;

    /// <summary>
    /// �� UI�� ����� ItemSlot
    /// </summary>
    ItemSlot itemSlot;
    public ItemSlot ItemSlot => itemSlot;

    /// <summary>
    /// ������ ������ ǥ�ÿ� �̹���
    /// </summary>
    Image itemImage;

    /// <summary>
    /// ������ ���� ǥ�ÿ� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI itemCount;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        itemImage= child.GetComponent<Image>();

        child = transform.GetChild(1);
        itemCount = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// ���� �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="id">���� �ε���. ����ID�� ���ҵ� ��</param>
    /// <param name="slot">�� UI�� ������ ItemSlot</param>
    public virtual void InitializeSlot(uint id, ItemSlot slot)
    {
        Debug.Log($"{id} ���� �ʱ�ȭ");
        this.id = id;       //�� ����
        itemSlot = slot;
        itemSlot.onSlotItemChange = Refresh;    //���Կ� ����ִ� �������� ����Ǿ��� �� ���� �� �Լ����

        Refresh();          //���̴� ��� �ʱ�ȭ
    }

    
    /// <summary>
    /// ������ ���̴� ����� �����ϴ� �Լ�
    /// itemSlot�� ����ִ� �������� ����� ������ ����.
    /// </summary>
    private void Refresh()
    {
        if(itemSlot.IsEmpty)
        {
            //���Կ� �������� ������� ���� ��
            itemImage.sprite = null;        //�̹��� ����
            itemImage.color = Color.clear;  //color���� ���İ� ������ ����.
            itemCount.text = string.Empty;  //���� ����
        
        }   
        else
        {
            //���Կ� �������� ������� ��
            itemImage.sprite = itemSlot.ItemData.itemIcon;      //�̹��� �����ϱ�
            itemImage.color = Color.white;                      //�������ϰ� �����
            itemCount.text = ItemSlot.ItemCount.ToString();      //���� ���ڷ� �ֱ�

        }
    }
}
