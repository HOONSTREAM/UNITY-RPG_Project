using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public Animator animator; // 애니메이터 컴포넌트

    #region One_Hand_Weapon
    public AnimationClip oneHand_ATTACK; 
    public AnimationClip oneHand_Idle; 
    public AnimationClip oneHand_Run;
    public AnimationClip oneHand_Die;
    #endregion


    private AnimatorOverrideController overrideController;
    void Start()
    {
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

    }

    public void Change_oneHand_weapon_animClip()
    {
        overrideController["Combo03_SwordShield"] = oneHand_ATTACK;
        overrideController["Idle_noWeapon"] = oneHand_Idle;
        overrideController["NormalSprint_noWeapon"] = oneHand_Run;
        overrideController["Die_noWeapon"] = oneHand_Die;

    }
}
