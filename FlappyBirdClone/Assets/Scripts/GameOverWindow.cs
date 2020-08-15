using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button menuBtn;
    private void Start()
    {
        Bird.Instance.OnDie += Show;

        retryButton.onClick.AddListener(() => Loader.Load(Loader.Scene.Scene1));
        menuBtn.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenu));

        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        score.text = ScoreController.Instance.Score.text;
        gameObject.SetActive(true);
    }
}
