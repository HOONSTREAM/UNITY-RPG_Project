using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �÷��̾��� �� ���� ��, �÷��̾� �ִϸ��̼� ������ ����ϴ� �������̽��Դϴ�.
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
