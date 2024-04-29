using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager

{
    AudioSource[] _audiosource = new AudioSource[(int)Define.Sound.Maxcount]; // BGM,EFFECT 용도 하나씩
    Dictionary<string,AudioClip> _audioClips = new Dictionary<string,AudioClip>();   
    //MP3 Player -> AudioSource 컴포넌트
    //MP3 음원 -> AudioClip 
    // 귀  -> Audio Listener

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundnames = System.Enum.GetNames(typeof(Define.Sound));

            for(int i = 0; i < soundnames.Length-1; i++)
            {
              GameObject go =   new GameObject { name = soundnames[i] }; //재생기 붙이는 과정
               _audiosource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform; //root의 자식이 되는 것.
            }

            _audiosource[(int)Define.Sound.Bgm].loop = true; //bgm은 루프
            _audiosource[(int)Define.Sound.Ambiance].loop = true; //Ambiance도 루프
            _audiosource[(int)Define.Sound.Ambiance].volume = 0.5f; //환경음은 볼륨 절반
        }
    }


  
    public void Clear()
    {
        foreach(AudioSource audiosource in _audiosource)
        {
            audiosource.clip = null;
            audiosource.Stop();           
        }
        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioclip = GetorAddAudioClip(path, type);

        Play(audioclip, type, pitch);
    }

    public void Play(AudioClip audioclip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) //오디오클립을 받는 버전
    {

        if (audioclip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            
            AudioSource audiosource = _audiosource[(int)Define.Sound.Bgm];
            if (audiosource.isPlaying)
                audiosource.Stop();

            audiosource.pitch = pitch;
            audiosource.clip = audioclip;
            audiosource.Play();


        }

        else if(type == Define.Sound.Effect)
        {
            AudioSource audiosource = _audiosource[(int)Define.Sound.Effect];
            audiosource.pitch = pitch;
            audiosource.PlayOneShot(audioclip); //던지는개념 (중간에 중단할 수 없음) 

        }

        else if(type == Define.Sound.Ambiance)
        {
            AudioSource audiosource = _audiosource[(int)Define.Sound.Ambiance];
            if (audiosource.isPlaying)
                audiosource.Stop();

            audiosource.pitch = pitch;
            audiosource.clip = audioclip;
            audiosource.Play();
        }
    }

    AudioClip GetorAddAudioClip(string path, Define.Sound type = Define.Sound.Effect) //경로를 입력받아서 있으면 얻고, 없으면 추가 
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }

        AudioClip audioclip = null;

        if (type == Define.Sound.Bgm)
        {
            audioclip = Managers.Resources.Load<AudioClip>(path);

        }

        else if (type == Define.Sound.Ambiance)
        {
            audioclip = Managers.Resources.Load<AudioClip>(path);
        }

        else //Effect (이펙트)
        {
            
            if (_audioClips.TryGetValue(path, out audioclip) == false)
            {
                audioclip = Managers.Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioclip);

            }
           
            
        }

        if (audioclip == null)
        {
            Debug.Log($"Audio clip Missing !{path} ");

        }


        return audioclip;

    }



}
