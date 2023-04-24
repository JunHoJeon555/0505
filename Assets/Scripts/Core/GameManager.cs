using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    ItemDataManager iteamDataManager;

    public ItemDataManager ItemData => iteamDataManager;
 
}
