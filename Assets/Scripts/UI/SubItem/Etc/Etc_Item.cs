using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/ETC(��Ÿ)/ETC_ITEM_����")]
public class Etc_Item : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"��� �� �� �����ϴ�.");

        return false;
    }
}
