using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMana
{

    /// <summary>
    /// MP Ȯ�� �� ������ ������Ƽ
    /// </summary>
    float MP { get; set; }

    /// <summary>
    /// �ִ� MP�� Ȯ���ϴ� ������Ƽ
    /// </summary>
    float MaxMP { get; }

    /// <summary>
    /// MP ������ �˸��� ���� ��������Ʈ�� �����ϰ� ����ϴ� ������Ƽ
    /// </summary>
    Action<float> onManaChange { get; set; }

    /// <summary>
    /// ������ ���������� �������� �ִ� �Լ�. �ʴ� totalRegen/duration��ŭ�� ȸ�� 
    /// </summary>
    /// <param name="totalRegen"></param>
    /// <param name="duration"></param>
    void ManaRegenerate(float totalRegen, float duration);
}
