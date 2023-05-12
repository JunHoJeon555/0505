using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 따라다니는 미니맵 카메라
/// </summary>
public class FollowCanera : MonoBehaviour
{
    public float followSpeed = 3.0f;
    Player player;

    private void Start()
    {
        player = GameManager.Inst.Player;
    }

    private void LateUpdate()
    {
        if (player.IsAlive)
        {
            Vector3 targetPos = player.transform.position;
            targetPos.y = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
        }
    }

   
}