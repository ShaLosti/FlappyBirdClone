using TMPro;
using System;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI maxScore;

    private void Start()
    {
        ITakeDmg takeDmg;
        foreach (var item in BirdContainerController.GetInstance.birds.Values)
        {
            item.TryGetComponent<ITakeDmg>(out takeDmg);
            if (takeDmg != null)
                item.GetComponent<ITakeDmg>().OnDie += Show;
        }

        Hide();
    }

    public void OnMenuBtnClick()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }

    public void OnRetryBtnClick()
    {
        Loader.Load(Loader.Scene.Scene1);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        int maxGameScore = PlayerPrefs.GetInt("MaxGameScore", 0);
        score.text = ScoreController.GetInstance.Score.text;
        if (maxGameScore < Int32.Parse(score.text))
            PlayerPrefs.SetInt("MaxGameScore", Int32.Parse(score.text));

        maxScore.text = PlayerPrefs.GetInt("MaxGameScore").ToString();
        gameObject.SetActive(true);
    }
}