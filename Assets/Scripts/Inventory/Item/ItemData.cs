using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName ="New Item Data" , menue = "Scriptable" )]
public class ItemData
{
    public uint id;                        //������ ID
    public string itemName = "������";     //�������� �̸�
    public GameObject mdelPrefab;          //������ ���� ������
    public Sprite itemIcon;                 //�κ��丮���� ���� ������
    public uint price = 0;                  //�������� ����
    public uint maxStaclCount = 1;          //������ ���� ��ĭ�� ����� ��� �� �� �ִ���
    public StringInfo itemDescriptin = "����";      //������ ��� ����

}
