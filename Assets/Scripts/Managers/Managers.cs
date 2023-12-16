 using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour

{
    
    static Managers s_instance; // ��ü ���ϼ� ���� (����ƽ �ν��Ͻ�) - ����ƽ ��ü ����
    static Managers Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� �����´�. (������Ƽ ���)

    #region Contents
    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }

    #endregion

    /*������ ���õ� �Ŵ��� (�ٸ� ������Ʈ������ �������� ��밡��)*/
    #region Core 
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourcesManager _resource = new ResourcesManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();
   
    public static InputManager Input { get { return Instance._input; } } //���⼭ ȣ��Ǹ鼭 Instance ������Ƽ���� ���ǹ��˻� (���ӿ�����Ʈ�� �ִ���)
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourcesManager Resources { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }

    public static SceneManagerEx Scene { get { return Instance._scene; } }

    public static SoundManager Sound { get { return Instance._sound; } }

    public static DataManager Data { get { return Instance._data; } }

    #endregion
    void Start()
    {
        Init(); 
    }

    void Update()
    {
        _input.OnUpdate(); //���콺 �Է� �̺�Ʈ �� �����Ӹ��� ����
    }

    static void Init() //�ν��Ͻ��� null�϶� �߻��ϴ� crash ����
    {

        if (s_instance == null) // instance�� Managers ������Ʈ�� �ܾ���� ������ null �̶��
        {
            GameObject go = GameObject.Find("@Managers"); // Gameobject @Managers ������Ʈ�� ã�ƺ��� null�̶��

            if (go == null) // ���̶�ŰUI���� CreateGameobject,�� ������Ʈ�� ���̴� �۾��� �ڵ�� �Ѵ�.
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>(); //@Managers ���� ������Ʈ�� Managers ������Ʈ�� ���δ�.
            }
            DontDestroyOnLoad(go); // �� ���ӿ�����Ʈ(�Ŵ���)�� ������� ������ �� ����.
            s_instance = go.GetComponent<Managers>(); //���� s_instance�� Managers�� ������Ʈ�� �Ҵ��Ѵ�.

            //Sound.Init(); ����ϸ�ȵ�. Instance�� �����ϰԵǰ�, ���ѷ����� �����Եȴ�.
            s_instance._data.Init();
            s_instance._sound.Init();
            s_instance._pool.Init();
        }
        
        
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        UI.Clear();
        Scene.Clear();
        Pool.Clear();
    }
}

