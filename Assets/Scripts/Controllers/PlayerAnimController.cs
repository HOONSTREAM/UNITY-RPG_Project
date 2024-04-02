using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerAnimController : MonoBehaviour
{
    public Animator animator; // 애니메이터 컴포넌트

    private Item Equip_Weapon; // 장착중이었던 무기가 어떤 종류인지 파악하기 위한 변수 



    #region One_Hand_Weapon
    public AnimationClip oneHand_Attack; 
    public AnimationClip oneHand_Idle; 
    public AnimationClip oneHand_Run;
    public AnimationClip oneHand_Die;
    #endregion

    #region No_Weapon
    public AnimationClip No_Weapon_Attack;
    public AnimationClip No_Weapon_Idle;
    public AnimationClip No_Weapon_Run;
    public AnimationClip No_Weapon_Die;
    #endregion

    #region Two_Hand
    public AnimationClip Two_Hand_Attack;
    public AnimationClip Two_Hand_Idle;
    public AnimationClip Two_Hand_Run;
    public AnimationClip Two_Hand_Die;
    #endregion

    public Item Get_request_Change_Weapon_EquipType(Item item)
    {
        return Equip_Weapon = item;
    }



    private AnimatorOverrideController overrideController;
    void Start()
    {
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

    }

    public void Change_oneHand_weapon_animClip()
    {
        overrideController["Attack01_MagicWand"] = oneHand_Attack;
        overrideController["Idle_noWeapon"] = oneHand_Idle;
        overrideController["NormalSprint_noWeapon"] = oneHand_Run;
        overrideController["Die_noWeapon"] = oneHand_Die;

        gameObject.GetComponent<PlayerWeaponController>().Change_Weapon_Prefabs();
    }

    public void Change_TwoHand_weapon_animClip()
    {
        overrideController["Attack01_MagicWand"] = Two_Hand_Attack;
        overrideController["Idle_noWeapon"] = Two_Hand_Idle;
        overrideController["NormalSprint_noWeapon"] = Two_Hand_Run;
        overrideController["Die_noWeapon"] = Two_Hand_Die;

        gameObject.GetComponent<PlayerWeaponController>().Change_Weapon_Prefabs();
    }


    public void Change_No_Weapon_animClip()
    {
        if (Equip_Weapon == null) return;

        
        gameObject.GetComponent<PlayerWeaponController>().Change_No_Weapon(); // 무기프리펩 비활성화
       
         overrideController["Attack01_MagicWand"] = No_Weapon_Attack;
         overrideController["Idle_noWeapon"] = No_Weapon_Idle;
         overrideController["NormalSprint_noWeapon"] = No_Weapon_Run;
         overrideController["Die_noWeapon"] = No_Weapon_Die;

       
    }
}
