using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<프리펩 생성 목적의 리소스매니저>

/*================================================================================*/

//툴로 직접 연결할 수 있지만 게임 프로젝트가 커지면 일일이 연결하기 힘드므로,
//코드상으로 프리펩을 생성할 수 있도록 한다.

/*================================================================================*/


public class ResourcesManager
{
    public T Load<T>(string path) where T : Object //유니티에서 참조할수 있는 모든 개체의 기본 클래스
    {
        
        if (typeof(T) == typeof(GameObject))  // 개선사항 1 . original 을 이미 들고 있으면 바로 사용
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index > 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.Getoriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path); //(프리펩 오브젝트 생성(실체화아님))함수를 리턴한다. path : 폴더경로 (함수랩핑)
    }

    //Instantiate()함수를 사용하면 게임을 실행하는 도중에 게임오브젝트를 생성할 수 있다.
    public GameObject Instantiate(string path, Transform parent = null) //Instantiate 함수(Gameobject 생성 (메모리에만 등록) 을 리턴하는함수) 랩핑
    {

        GameObject original = Load<GameObject>($"PreFabs/{path}"); //Resources.Load 함수를 랩핑한 함수를 가져온다.
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2. 만약 풀링된 오브젝트가 있는지 검사하고, 풀링이면 풀에서 꺼내온다.
        if (original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent); // 메모리에 등록된 오브젝트를 Scene에다가 실체화 (등록)
        
        go.name = original.name; 

        return go; //Object.을 안붙이면 리소스매니저 클래스 안의 랩핑함수 Instatiate를 재귀호출 해버리기 때문. 랩핑 Instantiate를 호출하는것이다.
    }

    public void Destroy(GameObject go)
    {

        if (go == null)
            return;

        // 개선사항 3. 만약 풀링이 필요한 오브젝트라면 -> 풀링 매니저에게 위탁한다. (바로 Destroy하는것이 아님)
        Poolable poolable = go.GetComponent<Poolable>(); // 해당 게임오브젝트가 Poolable 컴포넌트를 갖고있는지 검사
        if (poolable != null)
        {
            Managers.Pool.Push(poolable); //비활성화로 대기 
            return;
        }

        // 풀링대상이 아니라면 바로 Destroy
        Object.Destroy(go);
    }
}
