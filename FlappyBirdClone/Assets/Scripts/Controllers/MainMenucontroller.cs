using UnityEngine;

public class MainMenucontroller : MonoBehaviour
{
    private RectTransform rectTransform;

    private void OnEnable()
    {
        gameObject.TryGetComponent(out rectTransform);
    }

    public void StartPlay()
    {
        Loader.LoadAdditiveScene(Loader.Scene.Scene1.ToString());
        StartCoroutine(Coroutins.MoveRect(rectTransform, new Vector2(0, -2 * Screen.height), 1f));
        Coroutins.eventCompleted += GameStarted;
    }

    private void GameStarted()
    {
        Loader.UnloadScene(Loader.Scene.CurrentScene.ToString());
        Coroutins.eventCompleted -= GameStarted;
    }

    public void ExitApp() => Application.Quit();
}