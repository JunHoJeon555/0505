using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEngine;

//ItemData용 커스텀 애디터라는 의, 두벝ㄴ째 퍼러매토거 true면 저삭또 같이 적용받는다
[CustomEditor(typeof(ItemData),true)]
public class ItemDataManager : Editor
{   

}
#endif