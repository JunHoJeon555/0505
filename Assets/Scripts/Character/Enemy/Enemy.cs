using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //순찰돌기
    //- 정해진 웨이포인트 지점을 반복해서 움직인다.
    //- 웨이포인트 지점에 도착하면 일정 시간 정지한다.
    //추적
    //- 순찰 중에 플레이어 발견하면 추적
    //- 시야범위에서 플레이어가 벗어나면 다시 순찰
    //공격
    //- 플레이어가 공격범위안에 들어오면 공격시작
    // 사망
    //- 플레이어에게 일정 이상 데미지를 입으먄 사망
    //- 사망하면 파티클 이팩트 재생

    protected enum EnemyState
    {
        Wait = 0,   //대기상태
        patrol,     //순찰상태
        Chase,      //추적상태
        Attack,     //공격상태
        Die         //죽은상태
    }

    EnemyState state = EnemyState.Wait;

    public Waypoints waypoints;

    Transform moveTarget;

    NavMeshAgent agent;

    private void Awake()
    {
        waypoints = GetComponentInChildren<Waypoints>();
        waypoints.transform.SetParent(null);
    }

    private void Start()
    {
        moveTarget = waypoints.Current;
    }


}
