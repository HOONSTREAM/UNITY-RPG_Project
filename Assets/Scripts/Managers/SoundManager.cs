using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager

{
    AudioSource[] _audiosource = new AudioSource[(int)Define.Sound.Maxcount]; // BGM,EFFECT �뵵 �ϳ���
    Dictionary<string,AudioClip> _audioClips = new Dictionary<string,AudioClip>();   
    //MP3 Player -> AudioSource ������Ʈ
    //MP3 ���� -> AudioClip 
    // ��  -> Audio Listener

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
              GameObject go =   new GameObject { name = soundnames[i] }; //����� ���̴� ����
               _audiosource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform; //root�� �ڽ��� �Ǵ� ��.
            }

            _audiosource[(int)Define.Sound.Bgm].loop = true; //bgm�� ����
            _audiosource[(int)Define.Sound.Ambiance].loop = true; //Ambiance�� ����
            _audiosource[(int)Define.Sound.Ambiance].volume = 0.5f; //ȯ������ ���� ����
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

    public void Play(AudioClip audioclip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) //�����Ŭ���� �޴� ����
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
            audiosource.PlayOneShot(audioclip); //�����°��� (�߰��� �ߴ��� �� ����) 

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

    AudioClip GetorAddAudioClip(string path, Define.Sound type = Define.Sound.Effect) //��θ� �Է¹޾Ƽ� ������ ���, ������ �߰� 
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

        else //Effect (����Ʈ)
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
