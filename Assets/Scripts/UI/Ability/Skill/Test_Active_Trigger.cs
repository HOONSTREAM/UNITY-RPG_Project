using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Active_Trigger : MonoBehaviour
{

    public Vector3 boxCenter; // �ڽ��� �߽� ��ġ
    public Vector3 boxSize; // �ڽ��� ũ��
    public Quaternion boxRotation = Quaternion.identity; // �ڽ��� ȸ��

    // Ư�� ���̾ �ִ� ������Ʈ�� �����ϰ� ���� �� ���
    public LayerMask monsterLayerMask;
    public GameObject hit_particle;

    private Animator playerAnimator;
    private bool canUseSkill = true;
    void Start()
    {
        playerAnimator = Managers.Game.GetPlayer().GetComponent<Animator>();
        StartCoroutine(DetectMonstersInBox_and_Attack());
    }

    // TODO : �ִϸ��̼� ��� �Ϸ���� ��ų��� �Ұ��ϵ��� ��ġ, ������ �ο�, ��ƼŬ ����Ʈ �޼���� ������
    public IEnumerator DetectMonstersInBox_and_Attack()
    {
        if (canUseSkill)
        {
            canUseSkill = false;

            Managers.Game.GetPlayer().GetComponent<PlayerController>().State = Define.State.Skill;

        Vector3 worldCenter = transform.TransformPoint(boxCenter);

        // �ڽ� ���·� �ݶ��̴��� �����ϰ� �� ����� �迭�� ����
        Collider[] hitColliders = Physics.OverlapBox(worldCenter, boxSize / 2, boxRotation, monsterLayerMask);

            // ����� �ݶ��̴��� �ϳ��� ó��
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log("Monster Detected: " + hitCollider.gameObject.name);


                //��Ʈ ��ƼŬ ����Ʈ 

                Vector3 particlePosition = hitCollider.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                Quaternion particleRotation = Quaternion.LookRotation(hitCollider.transform.forward);


                GameObject hit_particles = Instantiate(hit_particle, particlePosition, particleRotation);
                hit_particles.SetActive(true);
                Destroy(hit_particles, 1.5f);

            }


            AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

            // �ִϸ��̼� ��� �Ϸ���� ���
            yield return new WaitForSeconds(3.0f);

            // ��ų ��� ���� ���·� ����
            canUseSkill = true;

        }


    }




}
