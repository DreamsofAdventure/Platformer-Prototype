using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("ForestAmbience");
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + "doesn't exist");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + "doesn't exist");
            return;
        }
        s.source.Stop();
    }
}
