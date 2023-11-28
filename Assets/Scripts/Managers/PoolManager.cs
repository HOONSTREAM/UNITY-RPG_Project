using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager 
{

     #region Pool
    class Pool // Ǯ �Ŵ��������� �������� Ǯ�� �����ִ�. Ǯ�Ŵ����� ���յǾ� ������ ã�� �Ϳ� �ð��� �����ɸ��� ����, ��ųʸ��� ����
    {
        public GameObject Original { get; private set; } // ���� ������
        public Transform Root { get; set; } // Ǯ �̸� ���� 


        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root"; //�ڽ� ��Ʈ ������ ����� �ִ� ����.

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name; //Clone �� �ٴ°��� ����


            return go.GetAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;
            poolable.transform.parent = Root; //�ش� Ǯ �̸��� �ڽ��̾�� ��.
            poolable.gameObject.SetActive(false); //��Ȱ��ȭ ������
            poolable.IsUsing = false;

            _poolStack.Push(poolable); //Pool Stack ���ٰ� �ִ´�.
        }


        public Poolable Pop(Transform parent) //�����°� �ƴ� Ȱ��ȭ ���·� Ǯ �ۿ��� ���� �ȿ��� ���� ���� �θ�, ���� �θ� ���ڷ� �޴´�.
        {
            Poolable poolable;

            if(_poolStack.Count>0)
                poolable = _poolStack.Pop();

            else
            {
                poolable = Create();
            }

            poolable.gameObject.SetActive(true);  // ���ӿ�����Ʈ Ȱ��ȭ

            if (parent == null) //DontDestroyOnLoad ���� �뵵 
            {
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            }
            poolable.transform.parent = parent; //�Ķ����(��� �ý����̳� �Լ��� Ư���� ������ ��Ÿ���� ����)�� ���� parent�� �θ�� ���� 
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    Transform _root; //Transform���� ����־ �ǰ� GameObject�� ����־ ��. (GameObject�� Transform ������Ʈ�� ������ ��� �ֱ� ������.) "@Pool_Root"
    Dictionary<string,Pool> _pool = new Dictionary<string,Pool>(); // Pool Ŭ������ �����ϰ� �ִ� ��ųʸ� 
    public void Init()
    {
        if (_root == null)
            _root = new GameObject { name = "@Pool_root" }.transform; //Ǯ���Ŵ����� ������ ���� �� ���ķ� ���������� �����ϰ� ��.
        Object.DontDestroyOnLoad(_root);
    }
   

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }
    public void Push(Poolable poolable) //��Ȱ��ȭ �� ���·� ���� 
    {
        string name = poolable.gameObject.name; 
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
            
           

        _pool[name].Push(poolable);

    }

    public Poolable Pop(GameObject original, Transform parent = null) // Ȱ��ȭ 
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);
        return _pool[original.name].Pop(parent);
    }

    public GameObject Getoriginal (string name)
    {
        if (_pool.ContainsKey(name) == false)
        {
            return null;
        }

     return _pool[name].Original;

    }


    public void Clear()
    {
        foreach(Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);

        }

        _pool.Clear();
    }
}
