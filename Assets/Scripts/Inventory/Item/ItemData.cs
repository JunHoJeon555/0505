using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("������ �⺻ ������")]
    public uint id;                     // ������ ID
    public string itemName = "������";  // �������� �̸�
    public GameObject modelPrefab;      // ������ ���� ������
    public Sprite itemIcon;             // �κ��丮���� ���� ������
    public uint price = 0;              // �������� ����
    public uint maxStackCount = 1;      // ������ ���� ��ĭ�� ����� �� �� �ִ���
    public string itemDescription = "����";   // ������ �� ����
}
