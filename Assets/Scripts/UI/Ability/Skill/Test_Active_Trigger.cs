using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Active_Trigger : MonoBehaviour
{

    public Vector3 boxCenter; // 박스의 중심 위치
    public Vector3 boxSize; // 박스의 크기
    public Quaternion boxRotation = Quaternion.identity; // 박스의 회전

    // 특정 레이어에 있는 오브젝트만 감지하고 싶을 때 사용
    public LayerMask monsterLayerMask;
    public GameObject hit_particle;
    private GameObject Skill_Hit_Target;
    public DamageNumber DamageText;
    private Animator playerAnimator;
   
    void Start()
    {
        playerAnimator = Managers.Game.GetPlayer().GetComponent<Animator>();
        StartCoroutine(DetectMonstersInBox_and_Attack());
    }

   
    public IEnumerator DetectMonstersInBox_and_Attack()
    {

        Managers.Game.GetPlayer().GetComponent<PlayerController>().State = Define.State.SnowSlash;
        

        Vector3 worldCenter = transform.TransformPoint(boxCenter);

        // 박스 형태로 콜라이더를 검출하고 그 결과를 배열에 저장
        Collider[] hitColliders = Physics.OverlapBox(worldCenter, boxSize / 2, boxRotation, monsterLayerMask);

            // 검출된 콜라이더를 하나씩 처리
            foreach (var hitCollider in hitColliders)
            {                      
               Debug.Log("Monster Detected: " + hitCollider.gameObject.name);
               Skill_Hit_Target = hitCollider.gameObject;
               Hit_Particle_Effect(hitCollider);

               Stat targetStat = Skill_Hit_Target.GetComponent<Stat>();
              
               targetStat.On_Active_Skill_Attacked(Managers.Game.GetPlayer().GetComponent<PlayerStat>(), Print_Damage_Text(targetStat)); //나의 스텟을 인자로 넣어서 상대방의 체력을 깎는다.
           
        }


        
        yield return null;

    }

    private void Hit_Particle_Effect(Collider hitCollider)
    {

        //히트 파티클 이펙트 

        Vector3 particlePosition = hitCollider.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        Quaternion particleRotation = Quaternion.LookRotation(hitCollider.transform.forward);


        GameObject hit_particles = Instantiate(hit_particle, particlePosition, particleRotation);
        hit_particles.SetActive(true);
        Destroy(hit_particles, 1.5f);
    }


    private int Print_Damage_Text(Stat targetStat)
    {
        if (targetStat.Hp >= 0)
        {
            int damage_amount = Managers.Game.GetPlayer().GetComponent<PlayerStat>().ATTACK * ((SkillDataBase.instance.SkillDB[4].num_1)/100);
            DamageNumber damageNumber = DamageText.Spawn(Skill_Hit_Target.transform.position, damage_amount);


            return damage_amount;
        }

        return 0;
    }

        

}
