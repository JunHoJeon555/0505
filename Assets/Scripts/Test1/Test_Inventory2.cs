using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventory2 : Test_Base
{
    public ItemCode code = ItemCode.Ruby;
    public int index = 0;
    public uint from = 0;
    public uint to = 1;
    public bool isAscending = true;
    public ItemSortBy sortBy = ItemSortBy.ID;

    Inventory inventory;

    private void Start()
    {
        inventory = new Inventory(6);
        inventory.AddItem(ItemCode.Ruby);
        inventory.AddItem(ItemCode.Ruby);
        inventory.AddItem(ItemCode.Ruby, 4);
        inventory.AddItem(ItemCode.Emerald, 5);
        inventory.AddItem(ItemCode.Emerald, 5);
        inventory.AddItem(ItemCode.Emerald, 5);
        inventory.AddItem(ItemCode.Sapphire, 2);
        inventory.AddItem(ItemCode.Sapphire, 2);
        //[ ���(2), (��ĭ), �����̾�(2), (��ĭ), ���(1), ���޶���(5) ]
        inventory.PrintInventory();
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        inventory.SlotSorting(sortBy, isAscending);
        inventory.PrintInventory();
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        inventory.MoveItem(from, to);
        inventory.PrintInventory();
    }


}
