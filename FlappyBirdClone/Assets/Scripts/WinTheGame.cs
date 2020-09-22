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

    private void Start()
    {
        StartCoroutine(Coroutins.FadeOut(blank, 1f));
    }
    public void GameWin()
    {
        Bird.GetInstance.autoJump = true;
        StartCoroutine(Coroutins.FadeIn(blank, fadeIn, 1f));
        StartCoroutine(Coroutins.FadeIn(youWinTitle, fadeIn, 1f));
        StartCoroutine(Coroutins.PerfomrmWithDelay(() =>
        {
            billyGif.SetActive(true);
            scoreWindow.SetActive(false);
            backGround.color = new Color32(235, 255, 225, 255);
            StartCoroutine(Coroutins.FadeOut(youWinTitle, fadeOut-5f));
            StartCoroutine(Coroutins.FadeOut(blank, fadeOut));
        }, fadeIn + 1f));
    }
}
