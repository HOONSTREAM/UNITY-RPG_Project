using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<������ ���� ������ ���ҽ��Ŵ���>

/*================================================================================*/

//���� ���� ������ �� ������ ���� ������Ʈ�� Ŀ���� ������ �����ϱ� ����Ƿ�,
//�ڵ������ �������� ������ �� �ֵ��� �Ѵ�.

/*================================================================================*/


public class ResourcesManager
{
    public T Load<T>(string path) where T : Object //����Ƽ���� �����Ҽ� �ִ� ��� ��ü�� �⺻ Ŭ����
    {
        
        if (typeof(T) == typeof(GameObject))  // �������� 1 . original �� �̹� ��� ������ �ٷ� ���
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index > 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.Getoriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path); //(������ ������Ʈ ����(��üȭ�ƴ�))�Լ��� �����Ѵ�. path : ������� (�Լ�����)
    }

    //Instantiate()�Լ��� ����ϸ� ������ �����ϴ� ���߿� ���ӿ�����Ʈ�� ������ �� �ִ�.
    public GameObject Instantiate(string path, Transform parent = null) //Instantiate �Լ�(Gameobject ���� (�޸𸮿��� ���) �� �����ϴ��Լ�) ����
    {

        GameObject original = Load<GameObject>($"PreFabs/{path}"); //Resources.Load �Լ��� ������ �Լ��� �����´�.
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2. ���� Ǯ���� ������Ʈ�� �ִ��� �˻��ϰ�, Ǯ���̸� Ǯ���� �����´�.
        if (original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent); // �޸𸮿� ��ϵ� ������Ʈ�� Scene���ٰ� ��üȭ (���)
        
        go.name = original.name; 

        return go; //Object.�� �Ⱥ��̸� ���ҽ��Ŵ��� Ŭ���� ���� �����Լ� Instatiate�� ���ȣ�� �ع����� ����. ���� Instantiate�� ȣ���ϴ°��̴�.
    }

    public void Destroy(GameObject go)
    {

        if (go == null)
            return;

        // �������� 3. ���� Ǯ���� �ʿ��� ������Ʈ��� -> Ǯ�� �Ŵ������� ��Ź�Ѵ�. (�ٷ� Destroy�ϴ°��� �ƴ�)
        Poolable poolable = go.GetComponent<Poolable>(); // �ش� ���ӿ�����Ʈ�� Poolable ������Ʈ�� �����ִ��� �˻�
        if (poolable != null)
        {
            Managers.Pool.Push(poolable); //��Ȱ��ȭ�� ��� 
            return;
        }

        // Ǯ������� �ƴ϶�� �ٷ� Destroy
        Object.Destroy(go);
    }
}
