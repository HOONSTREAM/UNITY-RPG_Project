using UnityEngine;



[System.Serializable]
[CreateAssetMenu(menuName = "ItemEffect/ETC(��Ÿ)/ETC_ITEM_����")]
public class Etc_Item : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        Print_Info_Text.Instance.PrintUserText($"��� �� �� �����ϴ�.");

        return false;
    }
}
