using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    private static AudioAssets instance;

    [SerializeField]
    private AudioUnits audioSources;

    public static AudioAssets GetInstance() => instance;

    private void Awake()
    {
        instance = this;
    }

    internal AudioClip GetSound(Enums.AudioSounds audioType)
    {
        return audioSources.FindAudioByKey(audioType);
    }

    internal void SetSound(Enums.AudioSounds audioType, AudioClip audioClip)
    {
        audioSources.SetAudioByKey(audioType, audioClip);
    }

    public void SetSounds(GameMode currentGameMode)
    {
        audioSources.Clear();
        foreach (var item in currentGameMode.audioSourse)
        {
            audioSources.SetAudioByKey(item.AudioSoundsKey, item.AudioSource);
        }
    }
}

[Serializable]
public class AudioUnit
{
    [SerializeField]
    private Enums.AudioSounds audioSoundsKey;

    [SerializeField]
    private AudioClip audioSource;

    public AudioClip AudioSource { get => audioSource; set => audioSource = value; }
    public Enums.AudioSounds AudioSoundsKey { get => audioSoundsKey; }
}

[Serializable]
public class AudioUnits
{
    [SerializeField]
    private Dictionary<Enums.AudioSounds, AudioClip> audioSources = new Dictionary<Enums.AudioSounds, AudioClip>();

    internal int Length() => audioSources.Count;

    internal AudioClip FindAudioByKey(Enums.AudioSounds audioType)
    {
        if (!audioSources.ContainsKey(audioType))
            return null;

        return audioSources[audioType];
    }

    internal void SetAudioByKey(Enums.AudioSounds audioType, AudioClip audioClip)
    {
        if (audioSources.ContainsKey(audioType))
        {
            audioSources[audioType] = audioClip;
        }
        else
        {
            audioSources.Add(audioType, audioClip);
        }
    }

    internal void Clear()
    {
        audioSources.Clear();
    }
}