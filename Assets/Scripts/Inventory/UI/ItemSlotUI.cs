using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotUI : ItemSlotUI_Base, IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    /// <summary>
    /// �巡�� ������ �˸��� ��������Ʈ
    /// </summary>
    public Action<uint> onDragBegin;

    /// <summary>
    /// �巡�� ���Ḧ �˸��� ��������Ʈ
    /// </summary>
    public Action<uint , bool> onDragEnd;

    /// <summary>
    /// ������ Ŭ���Ǿ��� �� ����Ǵ� ��������Ʈ
    /// </summary>
    public Action<uint> onClick;

    /// <summary>
    /// ���Կ� ���콺�� ������ �� ����Ǵ� ��������Ʈ
    /// </summary>
    public Action<uint> onPointerEnter;

    /// <summary>
    /// ���Կ��� ���콺�� ������ �� ����Ǵ� ��������Ʈ
    /// </summary>
    public Action<uint> onPointerExit;


    /// <summary>
    /// ���� ������ ���콺�� ������ �� ����Ǵ� ��������Ʈ
    /// </summary>
    public Action<Vector2> onPointerMove;

    TempItemSlotUI tempSlotUI;

    /// <summary>
    /// �� ����UI�� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="id">�� ����UI�� ID</param>
    /// <param name="slot">�� ����UI�� ������ ItemSlot</param>
    public override void InitializeSlot(uint id, ItemSlot slot)
    {
        // ��������Ʈ�� ���� ���� �����ϱ�
        onDragBegin = null;
        onDragEnd = null;
        onClick = null;
        onPointerEnter = null;
        onPointerExit = null;
        onPointerMove = null;
        

        // �θ� ó���ϴ� ��
        base.InitializeSlot(id, slot);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"�巡�� ���� : {ID}�� ����");
        onDragBegin?.Invoke(ID);    // ��������Ʈ�� �� ���Կ��� �巡�װ� ���۵Ǿ����� �˸�
    }

    public void OnDrag(PointerEventData eventData)
    {
        //OnBeginDrag�� OnEndDrag�� ���� �߰��� ��
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;    // �巡�װ� ���� ������ ���� ������Ʈ ��������
        if (obj != null)
        {
            // ������Ʈ�� ������ 
            ItemSlotUI endSlot = obj.GetComponent<ItemSlotUI>();
            if (endSlot != null)   // �������� Ȯ��
            {
                // �����̸� ��������Ʈ�� �� ���Կ��� �巡�װ� �������� �˸�
                onDragEnd?.Invoke(endSlot.ID, true);
                Debug.Log($"�巡�� ���� : {endSlot.ID}�� ����");
            }
            else
            {
                onDragEnd?.Invoke(ID, false);
                Debug.Log("������ �ƴմϴ�., ���� �������� �ǵ����ϴ�.");
            }
        }
        else
        {
            Debug.Log("������Ʈ�� �����ϴ�.");
        }
    }

    /// <summary>
    /// ������ Ŭ������ �� ����Ǵ� �Լ�.
    /// �ӽ� ���Կ� �ִ� �������� �� ���Կ� �ֱ� ���� �뵵
    /// (������ �и��ϱ� ���� �뵵)
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(ID);    // Ŭ���Ǿ��ٰ� ��ȣ�� ������
    }

    /// <summary>
    /// �� ����â ���� �ݴ� ���� �� ���� 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(ID);
    }

    /// <summary>
    /// ������â ���� �ݴ°��� �� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke(ID);
    }

    /// <summary>
    /// ���� ������ �������� �־��� ���� �κ��丮UI�� �����ϴ� ���� �� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerMove(PointerEventData eventData)
    {
        onPointerMove?.Invoke(eventData.position);
    }
}
