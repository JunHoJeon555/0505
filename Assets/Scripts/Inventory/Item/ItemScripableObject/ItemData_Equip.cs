using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_Equip : ItemData, IEquipable
{
    [Header("장비아이템 데이터")]
    /// <summary>
    /// 아이템을 장비했을 때 보일 오브젝트의 프리팹
    /// </summary>
    public GameObject equipPrefab;

    /// <summary>
    /// 장착될 위치를 알려주는 프로퍼티
    /// </summary>
    public virtual EquipType EquipPart => EquipType.Weapon;  //인터페이스 구현해야하기 때문에 만든것

    /// <summary>
    /// 아이템 장비하는 함수
    /// </summary>
    /// <param name="target">아이템을 장비할 대상</param>
    /// <param name="slot">아이템이 들어있는 슬롯</param>
    public void EquipItem(GameObject target, ItemSlot slot)
    {
        if (target != null)
        {
            IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
            if (equipTarget != null)
            {
                slot.IsEquipped = true;
                equipTarget.EquipItem(EquipPart, slot);
                Debug.Log($"{slot.Index}번째 슬롯 아이템 장착");
            }
        }
    }

    /// <summary>
    /// 아이템 장비 해제하는 함수
    /// </summary>
    /// <param name="target">아이템을 장비할 대상</param>
    /// <param name="slot">아이템이 들어있는 슬롯</param>
    public void UnEquipItem(GameObject target, ItemSlot slot)
    {
        if (target != null)
        {
            IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
            if (equipTarget != null)
            {
                slot.IsEquipped = false;
                equipTarget.UnEquipItem(EquipPart);
                Debug.Log($"{slot.Index}번째 슬롯 아이템 해제");
            }
        }
    }

    /// <summary>
    /// 상황에 따라 아이템을 장비하고 해제하는 함수
    /// </summary>
    /// <param name="target">아이템을 장비할 대상</param>
    /// <param name="slot">아이템이 들어있는 슬롯</param>
    public void AutoEquipUnequip(GameObject target, ItemSlot slot)
    {
        if (target != null)
        {
            IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
            if (equipTarget != null)            //대상이 아이템을 장비할 수 있는지 확인
            {
                ItemSlot oldsSlot = equipTarget[EquipPart];
                if(oldsSlot == null)            //이미 장비된것이 있는지 확인
                {
                    
                    //장비된것이 없으면 
                    EquipItem(target, slot);    //해당 파츠 장착하기
                }
                else
                {
                    //장비된것이 있으면
                    UnEquipItem(target, oldsSlot);  //옛날 슬롯에 있던 아이템제
                    if (oldsSlot != slot)
                    {
                        EquipItem(target, oldsSlot);  //새 슬롯과 옛슬롯이 다른 종류의 아이템이면 새로 장착
                    }
                  
                }

            }
        }
        //슬롯 클릭
        //1.장비 안되었을 때 >> 장비하기
        //2.장비가 되어있을 떄
        //2.1 다른 아이템이 장비되어있을 때 >>해제하고 장비하기
        //2.2 같은 아이템이 장비되어있을 때 >> 해제하기

    }
}
