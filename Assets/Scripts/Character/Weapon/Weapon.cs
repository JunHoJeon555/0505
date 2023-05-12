using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject hitEffect;
    
    /// <summary>
    /// 칼날의 역할을 할 콜라이더, 특정 타이밍에만 활성화
    /// </summary>
    CapsuleCollider blade;

    /// <summary>
    /// 
    /// </summary>
    ParticleSystem ps;

    Transform hitPos;

    private void Awake()
    {
        blade = GetComponent<CapsuleCollider>();
        ps = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            Debug.Log($"{other.name}에게 닿았다!");
            Vector3 impactPoint = transform.position + transform.up;    //무기의 임팩트  지점
            Vector3 effectPoint = other.ClosestPoint(impactPoint);                            //무기 임팩트 지점과 가장 가까우 지점(콜라이더의 표면)

            Instantiate(hitEffect, effectPoint, Quaternion.identity);


        }
    }

    /// <summary>
    /// 칼날을 켜고 끄는 함수. 플레이어의 델리게이트로 실행됨
    /// </summary>
    /// <param name="enable">true면 Enable false면 disable</param>
    public void ColliderEnable(bool enable)
    {
        blade.enabled = enable;
    }

    /// <summary>
    /// 컬 이팩트 켜고 끄는 함수, 플레이어의 델리게이트로 활성화
    /// </summary>
    /// <param name="play"></param>
    public void EffectPlay(bool play)
    {
        if (play)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }
}

