using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Item_Equip : Test_Base
{
    Player player;
    private void Start()
    {
        player = GameManager.Inst.Player;
        
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        ItemFactory.MakeItem(ItemCode.RoundShield);
        ItemFactory.MakeItem(ItemCode.IronSword);
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        player.Inventory.AddItem(ItemCode.RoundShield);
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        player.Inventory.AddItem(ItemCode.KiteShield);
    }

    protected override void Test4(InputAction.CallbackContext _)
    {
        player.Inventory.AddItem(ItemCode.KiteShield);
    }
}
