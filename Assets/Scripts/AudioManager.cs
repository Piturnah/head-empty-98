using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager _instance;

    private void Awake()
    {
        _instance = this;
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public static void Play(string name)
    {
        if (!Controller.levelIsOver && !IsPlaying(name))
        {
            Array.Find(_instance.sounds, sound => sound.name == name).source.Play();
        }
    }

    public static bool IsPlaying(string name)
    {
        return Array.Find(_instance.sounds, sound => sound.name == name).source.isPlaying;
    }

    public static void Stop(string name)
    {
        Array.Find(_instance.sounds, sound => sound.name == name).source.Stop();
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
