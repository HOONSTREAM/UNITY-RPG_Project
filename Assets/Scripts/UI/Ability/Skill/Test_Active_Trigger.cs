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

    void Start()
    {
        DetectMonstersInBox();
    }

    // 박스 내에 있는 몬스터를 검출하고 반환하는 함수
    public Collider[] DetectMonstersInBox()
    {
        // 박스 형태로 콜라이더를 검출하고 그 결과를 배열에 저장
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, boxRotation, monsterLayerMask);

        // 검출된 콜라이더를 하나씩 처리
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Monster Detected: " + hitCollider.gameObject.name);
        }

        return hitColliders;
    }




}
