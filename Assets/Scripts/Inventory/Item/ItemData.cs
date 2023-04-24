using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName ="New Item Data" , menue = "Scriptable" )]
public class ItemData
{
    public uint id;                        //아이템 ID
    public string itemName = "아이템";     //아이템의 이름
    public GameObject mdelPrefab;          //아이템 외형 프리팹
    public Sprite itemIcon;                 //인벤토리에서 보일 아이콘
    public uint price = 0;                  //아이템의 가격
    public uint maxStaclCount = 1;          //아이템 슬롯 한칸에 몇개까지 들어 갈 수 있는지
    public StringInfo itemDescriptin = "설명";      //아이템 상셍 설명

}
