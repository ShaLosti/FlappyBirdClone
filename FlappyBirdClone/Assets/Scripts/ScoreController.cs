using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private static ScoreController instance;
    private TextMeshProUGUI score;
    bool _increaseScore = false;

    public static ScoreController Instance { get => instance; }
    public TextMeshProUGUI Score { get => score; }

    private void Awake()
    {
        TryGetComponent<TextMeshProUGUI>(out score);
    }

    private void Start()
    {
        instance = this;
        Level.Instance.OnPipePassedBird += IncreaseScore;
    }
    private void OnDisable()
    {
        Level.Instance.OnPipePassedBird -= IncreaseScore;
    }

    private void IncreaseScore()
    {
        if(_increaseScore)
            Score.text = (Int32.Parse(Score.text) + 1).ToString();
        _increaseScore = !_increaseScore;
    }
}
