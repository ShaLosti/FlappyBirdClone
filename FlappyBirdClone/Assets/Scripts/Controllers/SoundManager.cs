using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static Queue<GameObject> audioSoundsGameObjects = new Queue<GameObject>();
    private static Queue<Coroutine> plaingSoundRightNow = new Queue<Coroutine>();
    private static List<AudioSource> currentPlaingAudioSounds = new List<AudioSource>();

    private static AudioSource backMusic;
    public static void Init()
    {
        audioSoundsGameObjects.Clear();
        currentPlaingAudioSounds.Clear();
        Transform soundsContainer = GameObject.Find("SoundPlayersContainer").transform;
        for (int i = 0; i < 40; i++)
        {
            GameObject _gameObj = new GameObject("Sound", typeof(AudioSource), typeof(AudioHighPassFilter));
            AudioHighPassFilter audioHighPassFilter = _gameObj.GetComponent<AudioHighPassFilter>();
            _gameObj.GetComponent<AudioSource>().playOnAwake = false;
            audioHighPassFilter.cutoffFrequency = 2000;
            audioHighPassFilter.highpassResonanceQ = 4;
            audioHighPassFilter.enabled = false;
            _gameObj.transform.SetParent(soundsContainer);
            _gameObj.SetActive(false);
            audioSoundsGameObjects.Enqueue(_gameObj);
        }

        ITakeDmg takeDmg;
        foreach (var item in BirdContainerController.GetInstance.birds.Values)
        {
            item.TryGetComponent<ITakeDmg>(out takeDmg);
            if (takeDmg != null)
                item.GetComponent<ITakeDmg>().OnDie += () => backMusic.GetComponent<AudioHighPassFilter>().enabled = true;
        }

    }
    internal static IEnumerator PlaySound(Enums.AudioSounds audioType, Action onMusicStopPlay)
    {
        if (Level.GetInstance.GameFinished)
            yield break;

        GameObject _gameObj = audioSoundsGameObjects.Dequeue();
        AudioSource audioSource = _gameObj.GetComponent<AudioSource>();
        if (AudioAssets.GetInstance().GetSound(audioType) != null)
        {
            if(audioType == Enums.AudioSounds.BackGroundMusic)
                backMusic = audioSource;

            audioSource.clip = AudioAssets.GetInstance().GetSound(audioType);
            _gameObj.SetActive(true);
            audioSource.Play();
        }
        currentPlaingAudioSounds.Add(audioSource);
        yield return new WaitWhile(() => audioSource.isPlaying);
        currentPlaingAudioSounds.Remove(audioSource);
        audioSoundsGameObjects.Enqueue(audioSource.gameObject);
        audioSource.gameObject.SetActive(false);
        onMusicStopPlay?.Invoke();
    }

    internal static void StopAllSounds()
    {
        foreach (var item in currentPlaingAudioSounds)
        {
            item.Stop();
            audioSoundsGameObjects.Enqueue(item.gameObject);
        }
        currentPlaingAudioSounds.Clear();
        currentPlaingAudioSounds.TrimExcess();
    }
}