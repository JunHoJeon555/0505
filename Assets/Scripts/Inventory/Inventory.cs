using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // �ʿ� ��� -----------------------------------------------------------------------------------

    /// <summary>
    /// �⺻ �κ��丮 ũ��
    /// </summary>
    public const int Default_Inventory_Size = 6;

    /// <summary>
    /// �ӽ� ������ �ε���
    /// </summary>
    public const uint TempSlotIndex = 99999999;

    // ���� ---------------------------------------------------------------------------------------

    /// <summary>
    /// �� �κ��丮�� ����ִ� ������ �迭
    /// </summary>
    ItemSlot[] slots;

    /// <summary>
    /// �κ��丮 ���Կ� �����ϱ� ���� �ε���
    /// </summary>
    /// <param name="index">������ ������ �ε���</param>
    /// <returns>������ ����</returns>
    public ItemSlot this[uint index] => slots[index];

    /// <summary>
    /// �κ��丮 ������ ����
    /// </summary>
    public int SlotCount => slots.Length;

    /// <summary>
    /// �ӽ� ����(�巡�׳� �и��� �� ���)
    /// </summary>
    ItemSlot tempSlot;
    public ItemSlot TempSlot => tempSlot;

    /// <summary>
    /// ���� �޴����� ������ �ִ� ������ ������ �޴���(��� �������� �����͸� ������ �ִ�.(������))
    /// </summary>
    ItemDataManager dataManager;

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="size">���� ���� �κ��丮�� ũ��</param>
    public Inventory(uint size = Default_Inventory_Size)
    {
        Debug.Log($"{size}ĭ¥�� �κ��丮 ����");
        slots = new ItemSlot[size];             // ���Կ� �迭 �����
        for (uint i = 0; i < size; i++)
        {
            slots[i] = new ItemSlot(i);         // ���� �ϳ��� ����
        }
        tempSlot = new ItemSlot(TempSlotIndex); // �ӽ� ���� �����

        dataManager = GameManager.Inst.ItemData;// ������ �޴��� ĳ���س���
    }

    /// <summary>
    /// �������� 1�� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="data">�߰��� �������� ������</param>
    /// <returns>��������(true�� �߰�, false �߰�����)</returns>
    public bool AddItem(ItemData data)
    {
        bool result = false;

        // ���� ������ �������� �ִ���
        ItemSlot sameDataSlot = FindSameItem(data);
        if (sameDataSlot != null)
        {
            // ���� ������ �������� ������ ���� �õ�
            result = sameDataSlot.IncreaseSlotItem(out uint _); // ��ġ�� ������ �ǹ̾���. ����� ���
        }
        else
        {
            // ���� ������ �������� ������ �󽽷� ã��
            ItemSlot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                // �󽽷� ã�Ҵ�.
                emptySlot.AssignSlotItem(data);
                result = true;
            }
            else
            {
                // ����ִ� ������ ����.
                Debug.Log("���� : �κ��丮�� ���� á���ϴ�.");
            }
        }
        return result;
    }

    /// <summary>
    /// �������� 1�� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="code">�߰��� �������� enum</param>
    /// <returns>��������(true�� �߰�, false �߰�����)</returns>
    public bool AddItem(ItemCode code)
    {
        return AddItem(dataManager[code]);
    }

    /// <summary>
    /// �������� �κ��丮�� Ư�� ���Կ� 1�� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="data">�߰��� ������ ������</param>
    /// <param name="index">�������� �߰��� �ε���</param>
    /// <returns>true�� ����, false�� ����</returns>
    public bool AddItem(ItemData data, uint index)
    {
        bool result = false;

        if (IsValidIndex(index)) // ������ �ε������� Ȯ��
        {
            ItemSlot slot = slots[index];  // index�� �ش��ϴ� ���� ã�ƿ���

            if (slot.IsEmpty)
            {
                // ������ ��������� �׳� �߰�
                slot.AssignSlotItem(data);
            }
            else
            {
                // ������ ������� �ʴ�.
                if (slot.ItemData == data)
                {
                    result = slot.IncreaseSlotItem(out uint _); // ���� �������̸� ���� �õ�
                }
                else
                {
                    // �ٸ� �������̸� �׳� ����
                    Debug.Log($"���� : �κ��丮 {index}�� ���Կ� �ٸ� �������� ����ֽ��ϴ�.");
                }
            }
        }
        else
        {
            Debug.Log($"���� : {index}���� �߸��� �ε����Դϴ�.");
        }

        return result;
    }

    /// <summary>
    /// �������� �κ��丮�� Ư�� ���Կ� 1�� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="code">�߰��� ������ enum�ڵ�</param>
    /// <param name="index">�������� �߰��� �ε���</param>
    /// <returns>true�� ����, false�� ����</returns>
    public bool AddItem(ItemCode code, uint index)
    {
        return AddItem(dataManager[code], index);
    }

    /// <summary>
    /// �κ��丮 Ư�� ���Կ��� ���� ������ŭ ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="slotIndex">������ ���� �ε���</param>
    /// <param name="decreaseCount">���ҽ�ų ����</param>
    public void RemoveItem(uint slotIndex, uint decreaseCount = 1)
    {
        if (IsValidIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(decreaseCount);
        }
        else
        {
            Debug.Log($"���� : {slotIndex}�� �߸��� �ε����Դϴ�.");
        }
    }


    /// <summary>
    /// Ư�� ���Կ��� �������� ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="slotIndex">������ ���� �ε���</param>
    public void ClearSlot(uint slotIndex)
    {
        if (IsValidIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.ClearSlotItem();
        }
        else
        {
            Debug.Log($"���� : {slotIndex}�� �߸��� �ε����Դϴ�.");
        }
    }

    /// <summary>
    /// �κ��丮�� ���� ���� �Լ�
    /// </summary>
    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlotItem();
        }
    }

    /// <summary>
    /// �������� �̵� ��Ű�� �Լ�
    /// </summary>
    /// <param name="from">���� ������ �ε���</param>
    /// <param name="to">���� ������ �ε���</param>
    public void MoveItem(uint from, uint to)
    {
        // from�� to�� ���� ���� ��ŵ
        // from�� to ��� valid�ؾ� �Ѵ�.
        if ((from != to) && IsValidIndex(from) && IsValidIndex(to))
        {
            // temp������ �����ؼ� ���׿����ڷ� ���� ���ϱ�.
            ItemSlot fromSlot = (from == TempSlotIndex) ? TempSlot : slots[from];
            if (!fromSlot.IsEmpty) // from�� �� ���� ó�� ����(���������� ������)
            {
                ItemSlot toSlot = (to == TempSlotIndex) ? TempSlot : slots[to];

                if (fromSlot.ItemData == toSlot.ItemData)
                {
                    // from�� to�� ���� �������� ���. ������ ��ġ��
                    toSlot.IncreaseSlotItem(out uint overCount, fromSlot.ItemCount);
                    fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);
                    Debug.Log($"�κ��丮�� {from}���Կ��� {to}�������� ������ ��ġ�� ����");
                }
                else
                {
                    // from�� to�� �ٸ� �������� ���. ���� �����ϱ�
                    ItemData tempData = fromSlot.ItemData;
                    uint tempCount = fromSlot.ItemCount;
                    fromSlot.AssignSlotItem(toSlot.ItemData, toSlot.ItemCount);
                    toSlot.AssignSlotItem(tempData, tempCount);
                    Debug.Log($"�κ��丮�� {from}���԰� {to}������ ������ ��ü ����");
                }
            }
        }
    }

    //������ ����
    public void SlotSorting(ItemSortBy sortBy, bool isAscending = true)
    {
        // ������ ����Ʈ �����
        List<ItemSlot> sortSlots = new List<ItemSlot>(SlotCount);
        foreach (var slot in slots)
        {
            sortSlots.Add(slot);
        }

        // �Ķ���Ϳ��� ������ ���ؿ� ���� ����
        switch (sortBy)
        {
            case ItemSortBy.Name:
                sortSlots.Sort((x, y) =>
                {
                    if (x.ItemData == null)
                    {
                        return 1;       //x�� null�̸� x�� ũ��
                    }
                    if (y.ItemData == null)
                    {
                        return -1;      //y�� null�̸� y�� ũ��
                    }
                    if (isAscending)
                    {
                        return x.ItemData.itemName.CompareTo(y.ItemData.itemName);  //�������� ��������
                    }
                    else 
                    {
                        return y.ItemData.itemName.CompareTo(x.ItemData.itemName);  //������������ ����
                    }
                });
                break;
            case ItemSortBy.Price:
                sortSlots.Sort((x, y) =>
                {
                    if (x.ItemData == null)
                    {
                        return 1;
                    }
                    if (y.ItemData == null)
                    {
                        return -1;
                    }
                    if (isAscending)
                    {
                        return x.ItemData.itemName.CompareTo(y.ItemData.price);
                    }
                    else
                    {
                        return y.ItemData.itemName.CompareTo(x.ItemData.price);
                    }
                });
                break;
            case ItemSortBy.ID:
            default:
                sortSlots.Sort((x, y) =>
                {
                    if (x.ItemData == null)
                    {
                        return 1;
                    }
                    if (y.ItemData == null)
                    {
                        return -1;
                    }
                    if (isAscending)
                    {
                        return x.ItemData.itemName.CompareTo(y.ItemData.id);
                    }
                    else
                    {
                        return y.ItemData.itemName.CompareTo(x.ItemData.id);
                    }
                });
                break;
        }


        // ���ĵ� ����� ���� ���� ���� �����ϱ�
        List<(ItemData, uint)> sortedData = new List<(ItemData, uint)>(SlotCount);
        foreach (var slot in sortSlots)
        {
            //������ �����Ϳ� ������ ������ ���ļ����� ���缭 ����Ʈ�� �����ϱ�
            sortedData.Add((slot.ItemData, slot.ItemCount));        
        }

        int index = 0;
        foreach (var data in sortedData)
        {
            //������ ���� �����Ϳ� ���� ���Կ� ������ ���� 
            slots[index].AssignSlotItem(data.Item1, data.Item2);
            index++;
        }
    }

    // �ܼ� Ȯ�� �� Ž���� �Լ��� -------------------------------------------------------------------

    private bool IsValidIndex(uint index) => (index < SlotCount) || (index == TempSlotIndex);

    /// <summary>
    /// ����ִ� ������ ã���ִ� �Լ�
    /// </summary>
    /// <returns>null�̸� ����ִ� �Լ��� ����. null�� �ƴϸ� ã�Ҵ�.</returns>
    private ItemSlot FindEmptySlot()
    {
        ItemSlot result = null;
        foreach (ItemSlot slot in slots) // �׳� �� ��������
        {
            if (slot.IsEmpty)
            {
                result = slot;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// �κ��丮�� ���� ������ �������� �ִ��� ã���ִ� �Լ�(�ִ밹���� ������ ����)
    /// </summary>
    /// <param name="data">ã�� ������</param>
    /// <returns>���� ������ �������� ����ִ� ����(null�� �ƴϸ� ã�Ҵ�. null�̸� ����.)</returns>
    private ItemSlot FindSameItem(ItemData data)
    {
        ItemSlot findSlot = null;
        foreach (ItemSlot slot in slots)    // ���� ã��
        {
            // ���� ������ ������ �����Ͱ� ���Կ� ��뷮�� �־�� �Ѵ�.
            if (slot.ItemData == data && slot.ItemCount < slot.ItemData.maxStackCount)
            {
                findSlot = slot;
                break;
            }
        }
        return findSlot;
    }

    /// <summary>
    /// �׽�Ʈ Ȯ�ο� �Լ�
    /// </summary>
    public void PrintInventory()
    {
        // ��� ���� : [ ���(1), �����̾�(1), ���޶���(2), (��ĭ), (��ĭ), (��ĭ) ]
        string printText = "[ ";

        for (int i = 0; i < SlotCount - 1; i++)
        {
            if (!slots[i].IsEmpty)
            {
                printText += $"{slots[i].ItemData.itemName}({slots[i].ItemCount})";
            }
            else
            {
                printText += "(��ĭ)";
            }
            printText += ", ";
        }

        ItemSlot last = slots[SlotCount - 1];
        if (!last.IsEmpty)
        {
            printText += $"{last.ItemData.itemName}({last.ItemCount})";
        }
        else
        {
            printText += "(��ĭ)";
        }
        printText += " ]";

        Debug.Log(printText);
    }
}
