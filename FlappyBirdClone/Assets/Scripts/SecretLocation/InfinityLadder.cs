using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityLadder : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        TryGetComponent<AudioSource>(out audioSource);
        StartCoroutine(Coroutins.FadeVolume(audioSource, 1f, 1f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlrController>() != null)
        {
            if (audioSource.isPlaying)
                collision.GetComponent<PlrController>().ResetPosition();
        }
    }
}
