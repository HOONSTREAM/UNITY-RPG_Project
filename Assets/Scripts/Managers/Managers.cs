 using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour

{
    
    static Managers s_instance; // 객체 유일성 보장 (스태틱 인스턴스) - 스태틱 객체 생성
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 가져온다. (프로퍼티 사용)

    #region Contents
    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }

    #endregion

    /*엔진과 관련된 매니저 (다른 프로젝트에서도 공용으로 사용가능)*/
    #region Core 
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourcesManager _resource = new ResourcesManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();
   
    public static InputManager Input { get { return Instance._input; } } //여기서 호출되면서 Instance 프로퍼티에서 조건문검사 (게임오브젝트가 있는지)
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
        _input.OnUpdate(); //마우스 입력 이벤트 매 프레임마다 실행
    }

    static void Init() //인스턴스가 null일때 발생하는 crash 방지
    {

        if (s_instance == null) // instance의 Managers 컴포넌트를 긁어오고 싶을때 null 이라면
        {
            GameObject go = GameObject.Find("@Managers"); // Gameobject @Managers 오브젝트를 찾아보고 null이라면

            if (go == null) // 하이라키UI에서 CreateGameobject,후 컴포넌트를 붙이는 작업을 코드로 한다.
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>(); //@Managers 게임 오브젝트에 Managers 컴포넌트를 붙인다.
            }
            DontDestroyOnLoad(go); // 이 게임오브젝트(매니저)는 마음대로 삭제할 수 없다.
            s_instance = go.GetComponent<Managers>(); //변수 s_instance에 Managers의 컴포넌트를 할당한다.

            //Sound.Init(); 사용하면안됨. Instance에 접근하게되고, 무한루프에 빠지게된다.
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

