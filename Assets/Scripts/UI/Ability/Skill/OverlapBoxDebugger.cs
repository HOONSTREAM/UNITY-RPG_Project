using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapBoxDebugger : MonoBehaviour
{

    public Vector3 boxCenter; // �˻��� �ڽ��� �߽�
    public Vector3 boxSize; // �˻��� �ڽ��� ũ��
    public Color debugColor = Color.green; // ����� ǥ�� ����

    void OnDrawGizmos()
    {
        // Gizmo ���� ����
        Gizmos.color = debugColor;

        // Transform�� ��ġ, ȸ���� ����Ͽ� �߽� ���� ���
        Vector3 worldCenter = transform.position + boxCenter;

        // �ڽ��� ȸ���� �����ϱ� ���� Gizmos.matrix ���
        Gizmos.matrix = Matrix4x4.TRS(worldCenter, transform.rotation, transform.lossyScale);

        // Wireframe �ڽ� �׸���
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }


}
