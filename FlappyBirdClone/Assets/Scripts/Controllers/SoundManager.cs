using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static Queue<GameObject> audioSoundsGameObjects = new Queue<GameObject>();
    private static Queue<Coroutine> plaingSoundRightNow = new Queue<Coroutine>();
    private static List<AudioSource> currentPlaingAudioSounds = new List<AudioSource>();

    public static void Init()
    {
        audioSoundsGameObjects.Clear();
        currentPlaingAudioSounds.Clear();
        Transform soundsContainer = GameObject.Find("SoundPlayersContainer").transform;
        for (int i = 0; i < 50; i++)
        {
            GameObject _gameObj = new GameObject("Sound", typeof(AudioSource));
            _gameObj.transform.SetParent(soundsContainer);
            _gameObj.SetActive(false);
            audioSoundsGameObjects.Enqueue(_gameObj);
        }
    }

    internal static IEnumerator PlaySound(Enums.AudioSounds audioType, Action onMusicStopPlay)
    {
        if (Level.GetInstance.GameFinished)
            yield break;

        GameObject _gameObj = audioSoundsGameObjects.Dequeue();
        _gameObj.SetActive(true);
        AudioSource audioSource = _gameObj.GetComponent<AudioSource>();
        if(AudioAssets.GetInstance().GetSound(audioType) != null)
            audioSource.PlayOneShot(AudioAssets.GetInstance().GetSound(audioType));
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