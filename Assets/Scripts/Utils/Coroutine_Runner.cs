using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �� Ŭ������, MonoBehavior Ŭ������ ��ӹ��� �ʾƵ� �ڷ�ƾ�� ������ �� �ְ� ���ִ� Ŭ���� �Դϴ�.
/// </summary>
public class Coroutine_Runner : MonoBehaviour
{
    private static Coroutine_Runner instance;

    public static Coroutine_Runner Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("CoroutineRunner");
                instance = obj.AddComponent<Coroutine_Runner>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public void StartRoutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
