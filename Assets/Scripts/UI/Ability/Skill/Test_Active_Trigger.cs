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

    void Start()
    {
        DetectMonstersInBox();
    }

    // �ڽ� ���� �ִ� ���͸� �����ϰ� ��ȯ�ϴ� �Լ�
    public Collider[] DetectMonstersInBox()
    {
        // �ڽ� ���·� �ݶ��̴��� �����ϰ� �� ����� �迭�� ����
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, boxRotation, monsterLayerMask);

        // ����� �ݶ��̴��� �ϳ��� ó��
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Monster Detected: " + hitCollider.gameObject.name);
        }

        return hitColliders;
    }




}
