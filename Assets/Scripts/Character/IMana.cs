using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMana
{

    /// <summary>
    /// MP 확인 및 설정용 프로퍼티
    /// </summary>
    float MP { get; set; }

    /// <summary>
    /// 최대 MP를 확인하는 프로퍼티
    /// </summary>
    float MaxMP { get; }

    /// <summary>
    /// MP 변경을 알리기 위한 델리게이트를 설정하고 사용하는 프로퍼티
    /// </summary>
    Action<float> onManaChange { get; set; }

    /// <summary>
    /// 마나를 지속적으로 증가시켜 주는 함수. 초당 totalRegen/duration만큼씩 회복 
    /// </summary>
    /// <param name="totalRegen"></param>
    /// <param name="duration"></param>
    void ManaRegenerate(float totalRegen, float duration);
}
