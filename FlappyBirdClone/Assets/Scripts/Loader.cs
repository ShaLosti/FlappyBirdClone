using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenu,
        Scene1,
        CurrentScene,
    }

    private static Scene targetScene;

    public static void Load(Scene scene)
    {
        targetScene = scene;
        LoadTargetScene();
    }

    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }

    public static void LoadAdditiveScene(string _name)
    {
        if (_name == "CurrentScene")
            _name = SceneManager.GetActiveScene().ToString();
        if (!SceneManager.GetSceneByName(_name).isLoaded)
            SceneManager.LoadSceneAsync(_name, LoadSceneMode.Additive);
    }

    public static void UnloadScene(string _name = "CurrentScene")
    {
        _name = _name == "CurrentScene" ? SceneManager.GetActiveScene().name : _name;
        if (SceneManager.GetSceneByName(_name).isLoaded)
            SceneManager.UnloadSceneAsync(_name);
    }
}