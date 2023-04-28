using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempItemSlotUI : ItemSlotUI_Base
{

    /// <summary>
    /// �ӽ� ������ �����ų� ���� �� ����Ǵ� ��������Ʈ. (true�� ���ȴ�. false�� ������.)
    /// </summary>
    public Action<bool> onTempSlotOpenClose;

    private void Update()
    {
        // Ȱ��ȭ �Ǿ����� �� �� �����Ӹ��� ȣ��
        transform.position = Mouse.current.position.ReadValue();    // ���콺 ��ġ�� ������Ʈ �̵� ��Ű��
    }

    public override void InitializeSlot(uint id, ItemSlot slot)
    {
        onTempSlotOpenClose = null;
        base.InitializeSlot(id, slot);
    }

    /// <summary>
    /// TempSlot�� ���̰� �ϴ� �Լ�
    /// </summary>
    public void Open()
    {
        transform.position = Mouse.current.position.ReadValue();    // �켱 ��ġ�� ���콺 ��ġ�� �ű��
        onTempSlotOpenClose?.Invoke(true);  // ���ȴٰ� �˸���
        gameObject.SetActive(true);     // Ȱ��ȭ ���Ѽ� ���̰� �����
    }

    /// <summary>
    /// TempSlot�� ������ �ʰ� �ϴ� �Լ�
    /// </summary>
    public void Close()
    {
        onTempSlotOpenClose?.Invoke(false); // �����ٰ� �˸���
        gameObject.SetActive(false);    // ��Ȱ��ȭ ���Ѽ� ������ �ʰ� ����� Update�� ����ȵǰ� �ϱ�
    }
}
