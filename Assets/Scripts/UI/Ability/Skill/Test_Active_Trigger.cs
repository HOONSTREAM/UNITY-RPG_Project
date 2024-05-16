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

    private Animator playerAnimator;
    private bool canUseSkill = true;
    void Start()
    {
        playerAnimator = Managers.Game.GetPlayer().GetComponent<Animator>();
        StartCoroutine(DetectMonstersInBox_and_Attack());
    }

    // TODO : 애니메이션 재생 완료까지 스킬사용 불가하도록 조치, 데미지 부여, 파티클 이펙트 메서드로 빼내기
    public IEnumerator DetectMonstersInBox_and_Attack()
    {
        if (canUseSkill)
        {
            canUseSkill = false;

            Managers.Game.GetPlayer().GetComponent<PlayerController>().State = Define.State.Skill;

        Vector3 worldCenter = transform.TransformPoint(boxCenter);

        // 박스 형태로 콜라이더를 검출하고 그 결과를 배열에 저장
        Collider[] hitColliders = Physics.OverlapBox(worldCenter, boxSize / 2, boxRotation, monsterLayerMask);

            // 검출된 콜라이더를 하나씩 처리
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log("Monster Detected: " + hitCollider.gameObject.name);


                //히트 파티클 이펙트 

                Vector3 particlePosition = hitCollider.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                Quaternion particleRotation = Quaternion.LookRotation(hitCollider.transform.forward);


                GameObject hit_particles = Instantiate(hit_particle, particlePosition, particleRotation);
                hit_particles.SetActive(true);
                Destroy(hit_particles, 1.5f);

            }


            AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

            // 애니메이션 재생 완료까지 대기
            yield return new WaitForSeconds(3.0f);

            // 스킬 사용 가능 상태로 변경
            canUseSkill = true;

        }


    }




}
