using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어의 방어구 장착 시, 플레이어 애니메이션 변경을 담당하는 인터페이스입니다.
/// </summary>
public class PlayerDefenseController : MonoBehaviour
{
    public interface Defense_Gear_Controller_Interface
    {
        public Item Get_request_Change_Defense_EquipType(Item item);
        public void Change_Defense_Gear_Prefabs();
        public void Change_No_Defense_Gear();
    }

}
