using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private static ScoreController instance;
    private TextMeshProUGUI score;
    private int pipePassedCount;
    private bool _increaseScore = false;

    public static ScoreController GetInstance { get => instance; }
    public TextMeshProUGUI Score { get => score; }
    public int PipePassedCount { get => pipePassedCount; }

    private void Awake()
    {
        TryGetComponent<TextMeshProUGUI>(out score);
    }

    private void Start()
    {
        instance = this;
    }
    public void IncreaseScore()
    {
        if (_increaseScore)
        {
            pipePassedCount++;
            Score.text = pipePassedCount.ToString();
        }
        _increaseScore = !_increaseScore;
    }
}