using UnityEngine;



[System.Serializable]
[CreateAssetMenu(menuName = "ItemEffect/ETC(기타)/ETC_ITEM_공통")]
public class Etc_Item : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        Print_Info_Text.Instance.PrintUserText($"사용 할 수 없습니다.");

        return false;
    }
}
