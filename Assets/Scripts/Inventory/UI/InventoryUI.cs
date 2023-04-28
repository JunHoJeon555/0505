using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// ������ �ٽ� ���� �� �ʿ��� ������
    /// </summary>
    public GameObject slotPrefab;

    /// <summary>
    /// �� UI�� ������ �κ��丮
    /// </summary>
    Inventory inven;

    /// <summary>
    /// �� �κ��丮 UI�� �ִ� ��� ���� UI
    /// </summary>
    ItemSlotUI[] slotUIs;
    /// <summary>
    /// ������ �̵��̳� �и��� �� ����� �ӽ� ���� UI
    /// </summary>
    TempItemSlotUI tempSlotUI;

    /// <summary>
    /// �������� �������� �����ִ� â
    /// </summary>
    DetailWindow detail;

    PlayerInputActions inputActions;

    private void Awake()
    {
        Transform slotParent = transform.GetChild(0);
        slotUIs = slotParent.GetComponentsInChildren<ItemSlotUI>();

        tempSlotUI = GetComponentInChildren<TempItemSlotUI>();

        detail = GetComponentInChildren<DetailWindow>();

        inputActions = new PlayerInputActions();

    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    /// <summary>
    /// �κ��丮 UI �ʱ�ȭ
    /// </summary>
    /// <param name="playerInven">�� UI�� ǥ���� �κ��丮</param>
    public void InitializeInventory(Inventory playerInven)
    {
        inven = playerInven;

        Transform slotParent = transform.GetChild(0);
        GridLayoutGroup layout = slotParent.GetComponent<GridLayoutGroup>();

        if (Inventory.Default_Inventory_Size != inven.SlotCount)
        {
            // �⺻ ũ��� �޶����� ��

            // �̸� ������� �ִ� ���� ��� ����
            foreach (var slot in slotUIs)
            {
                Destroy(slot.gameObject);
            }

            // �� ũ�� ����ϱ�
            RectTransform rect = (RectTransform)slotParent;
            float totalArea = rect.rect.height * rect.rect.width;   // �θ� ������ ��ü ����
            float slotArea = totalArea / inven.SlotCount;           // ���� �ϳ��� ���� �� �ִ� ����
            float slotSideLength = Mathf.Floor(Mathf.Sqrt(slotArea)); // ���� �Ѻ��� ���� ���ϱ�
            layout.cellSize = new Vector2(slotSideLength, slotSideLength); // ���� ���̷� �����ϱ�

            // ���� ���� �����
            slotUIs = new ItemSlotUI[inven.SlotCount];
            for (uint i = 0; i < inven.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);   // �����ϰ�
                obj.name = $"{slotPrefab.name}_{i}";                    // �̸� ���̰�
                slotUIs[i] = obj.GetComponent<ItemSlotUI>();            // ������ ����
            }
        }

        // ������ �ʱ�ȭ �۾�
        for (uint i = 0; i < inven.SlotCount; i++)
        {
            slotUIs[i].InitializeSlot(i, inven[i]);
            slotUIs[i].onDragBegin += OnItemMoveBegin;
            slotUIs[i].onDragEnd += OnItemMoveEnd;
            slotUIs[i].onClick += OnSlotClick;
            slotUIs[i].onPointerEnter += OnItemDetailOn;
            slotUIs[i].onPointerExit += OnItemDetailOff;
            slotUIs[i].onPointerMove += OnSlotPointerMove;

        }
        // �ӽ� ���� �ʱ�ȭ
        tempSlotUI.InitializeSlot(Inventory.TempSlotIndex, inven.TempSlot); // �ӽý��Ե� �ʱ�ȭ
        tempSlotUI.onTempSlotOpenClose += OnDetailPause;
        tempSlotUI.Close();     // �����ϸ� ������
   
        //����â �ݾƳ���
        detail.Close();
    }

    

    /// <summary>
    /// ���콺 �巡�װ� ���۵Ǿ��� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="slotID">�巡�� ���� ������ ID</param>
    private void OnItemMoveBegin(uint slotID)
    {
        inven.MoveItem(slotID, tempSlotUI.ID);  // ���� ������ ����� �ӽ� ������ ������ ���� ��ü��Ű��
        tempSlotUI.Open();                      // �ӽ� ���� ���̰� �����
    }

    /// <summary>
    /// ���콺 �巡�װ� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="slotID">�巡�װ� ���� ������ ID</param>
    private void OnItemMoveEnd(uint slotID , bool isSuccess)
    {
        inven.MoveItem(tempSlotUI.ID, slotID);  // �ӽ� ������ ����� �巡�װ� ���� ������ ������ ���� ��ü��Ű��
        if(tempSlotUI.ItemSlot.IsEmpty)         // ��ü ��� �ӽ� ������ ��� �Ǹ�
        {
            tempSlotUI.Close();                 // �ӽ� ���� ��Ȱ��ȭ�ؼ� �Ⱥ��̰� �����
        }
        if(isSuccess)
        {
            detail.Open(inven[slotID].ItemData);// �巡�װ� ���������� �������� ������â �����ֱ�

        }
    }

    /// <summary>
    /// ������ Ŭ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="slotID">Ŭ���� ������ ID</param>
    private void OnSlotClick(uint slotID)
    {
        if(!tempSlotUI.ItemSlot.IsEmpty)
        {
            // Ŭ���Ǿ� ���� ������ �巡�װ� ���� �Ͱ� ���� ó��
            // �ӽý��԰� Ŭ���Ƚ����� ������ ���� ��ü
            OnItemMoveEnd(slotID, true);
        }
    }

    /// <summary>
    /// ���콺 �����Ͱ� ���Կ� ��Ű��� �� �� ������ ������ ������ ���� �����ִ� â ���� �Լ�
    /// </summary>
    /// <param name="slotID"></param>
    private void OnItemDetailOn(uint slotID)
    {
        detail.Open(slotUIs[slotID].ItemSlot.ItemData);
    }

    /// <summary>
    /// ���콺 �����Ͱ� ���Կ��� ������ �� ������ ������â�� �ݴ� �Լ�
    /// </summary>
    /// <param name="slotID"></param>
    private void OnItemDetailOff(uint slotID)
    {
        detail.Close();
    }
    /// <summary>
    /// ���콺�� ���Ծȿ��� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="screenPos">���콺 Ŀ���� ��ũ�� ��ǥ</param>
    private void OnSlotPointerMove(Vector2 screenPos)
    {
        detail.MovePosition(screenPos); // ������ â �̵���Ű��
    }

    /// <summary>
    /// ������â�� �Ͻ����� ���� ������ �����ϴ� �Լ�(�ַ� �ӽý����� ���� �� �Ͻ�������)
    /// </summary>
    /// <param name="isPause">true�� �Ͻ�����, false�� �Ͻ���������</param>
    private void OnDetailPause(bool isPause)
    {
        detail.IsPause = isPause;
    }

    public void TestInventory()
    {

    }
}
