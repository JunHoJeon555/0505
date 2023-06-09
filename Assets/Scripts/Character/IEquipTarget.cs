using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEquipTarget
{
  
    /// <summary>
    /// 특정파트에 아이템이 장비되었는지를 알려주는 인덱서
    /// </summary>
    /// <param name="part">확인할 파추</param>
    /// <returns>장비되어있지 않으면 null</returns>
    ItemSlot this[EquipType part] { get; }

    /// <summary>
    /// 아이템을 장비하는 함수
    /// </summary>
    /// <param name="part">장비할 파츠</param>
    /// <param name="slot">장비할 아이템이 들어있는 슬롯</param>
    void EquipItem(EquipType part, ItemSlot slot);

    /// <summary>
    /// 아이템을 장비해제할 파츠
    /// </summary>
    /// <param name="part"></param>
    void UnEquipItem(EquipType part);

    /// <summary>
    /// 아이템이 장비될 트랜스폼을 돌려주는 함수
    /// </summary>
    /// <param name="part">장비가 될 함수</param>
    /// <returns></returns>
    Transform GetPartTransform(EquipType part);

}
