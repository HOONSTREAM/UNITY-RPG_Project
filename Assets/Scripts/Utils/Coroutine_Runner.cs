using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 이 클래스는, MonoBehavior 클래스를 상속받지 않아도 코루틴을 실행할 수 있게 해주는 클래스 입니다.
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
