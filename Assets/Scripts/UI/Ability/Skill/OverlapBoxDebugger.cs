using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapBoxDebugger : MonoBehaviour
{

    public Vector3 boxCenter; // 검사할 박스의 중심
    public Vector3 boxSize; // 검사할 박스의 크기
    public Color debugColor = Color.green; // 디버그 표시 색상

    void OnDrawGizmos()
    {
        // Gizmo 색상 설정
        Gizmos.color = debugColor;

        // Transform의 위치, 회전을 고려하여 중심 지점 계산
        Vector3 worldCenter = transform.position + boxCenter;

        // 박스의 회전을 적용하기 위해 Gizmos.matrix 사용
        Gizmos.matrix = Matrix4x4.TRS(worldCenter, transform.rotation, transform.lossyScale);

        // Wireframe 박스 그리기
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }


}
