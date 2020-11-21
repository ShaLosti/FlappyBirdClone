using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTheGame : MonoBehaviour
{
    [SerializeField]
    private float fadeIn = 0f;
    [SerializeField]
    private float fadeOut = 0f;
    //private bool gameIsOver = false;
    [SerializeField]
    CanvasGroup blank;
    [SerializeField]
    GameObject billyGif;
    [SerializeField]
    SpriteRenderer backGround;
    [SerializeField]
    GameObject scoreWindow;
    [SerializeField]
    CanvasGroup youWinTitle;
    [SerializeField]
    AudioSource finalMusic;
    [SerializeField]
    AudioSource prettyGood;

    AudioClip finalClip;
    private void Start()
    {
        finalClip = finalMusic.clip;
        StartCoroutine(Coroutins.FadeOut(blank, 1f));
    }
    public void GameWin()
    {
        Bird.GetInstance.autoJump = true;

        if (Bird.GetInstance.GetComponent<ObjDie>() != null)
            Bird.GetInstance.GetComponent<ObjDie>().enabled = false;

        prettyGood.Play();
        StartCoroutine(Coroutins.FadeIn(blank, fadeIn, 1f));
        StartCoroutine(Coroutins.FadeIn(youWinTitle, fadeIn, 1f));
        PlayerPrefs.DeleteAll();
        Utils.PerformWithDelay(this, fadeIn + 1f,
            () =>
            {
                billyGif.SetActive(true);
                scoreWindow.SetActive(false);
                backGround.color = new Color32(235, 255, 225, 255);
                StartCoroutine(Coroutins.FadeOut(youWinTitle, fadeOut - 5f));
                StartCoroutine(Coroutins.FadeOut(blank, fadeOut));
            });
        StartCoroutine(EndBlank());
    }

    IEnumerator EndBlank()
    {
        yield return new WaitUntil(() => finalMusic.time >= finalClip.length - 18f);
        StartCoroutine(Coroutins.FadeIn(blank, 16f, 1f));
    }
}
