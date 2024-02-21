using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/ETC(기타)/ETC_ITEM_공통")]
public class Etc_Item : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"사용 할 수 없습니다.");

        return false;
    }
}
