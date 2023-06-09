using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IHealth
{ 
    /// <summary>
    /// 살았는지 죽었는지 확인하는 프로퍼티
    /// </summary>
    bool IsAlive { get; }
    
    /// <summary>
    /// HP 확인 및 설정용 프로퍼티
    /// </summary>
    float HP { get; set; }

    /// <summary>
    /// 최대 HP를 확인하는 프로퍼티
    /// </summary>
    float MaxHP { get;  } 

    /// <summary>
    /// HP 변경을 알리기 위한 델리게이트를 설정하고 사용하는 프로퍼티
    /// </summary>
    Action<float> onHealthChange { get; set; }

    /// <summary>
    /// 사망처리용 함수
    /// </summary>
    void Die();

    /// <summary>
    /// 사망을 알리기 위한 델리게이트를 설정하고 사용하는 프로퍼티
    /// </summary>
    Action onDie { get; set; }
    //인터페이스에는 프로퍼티도 넣을 수 있다.

    /// <summary>
    /// 회복을 지속적으로 증가시켜 주는 함수. 초당 totalRegen/duration만큼씩 회복 
    /// </summary>
    /// <param name="totalRegen"></param>
    /// <param name="duration"></param>
    void HealthRegenerate(float totalRegen, float duration);
}
