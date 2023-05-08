using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data - Food", order = 3)]

public class ItemData_Food : ItemData, IConsumable
{
    [Header("���� ������ ������")]

    /// <summary>
    /// ��ü ȸ����
    /// </summary>
    public float healthAmount;

    /// <summary>
    /// ��ü ȸ���ð�
    /// </summary>
    public float duration;

    public void Consume(GameObject target)
    {
        IHealth health = target.GetComponent<IHealth>();
        if (health != null)
        {
            health.HealthRegenerate(healthAmount, duration);
        }
    }
}
