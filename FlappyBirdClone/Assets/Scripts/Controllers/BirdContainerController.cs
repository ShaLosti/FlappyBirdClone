using System.Collections.Generic;
using UnityEngine;

public class BirdContainerController : MonoBehaviour
{
    private static BirdContainerController instance;
    public static BirdContainerController GetInstance { get => instance; private set => instance = value; }
    public Dictionary<string, GameObject> birds = new Dictionary<string, GameObject>();
    public string currentBirdTitle = "";

    private void Awake()
    {
        instance = this;
        foreach (var item in Level.GetInstance.gameMods.GameModes)
        {
            if (birds.ContainsKey(item.gameModeTitle))
                return;

            birds.Add(item.gameModeTitle, Instantiate(item.plrPref, transform));
            birds[item.gameModeTitle].GetComponent<IPlrBird>().SetNativeGameMode(item);
            birds[item.gameModeTitle].SetActive(false);
        }
    }

    public void SetNewGameMode(GameMode gameMode)
    {
        if (birds.Count == 0)
            return;
        if (currentBirdTitle == "")
        {
            birds[gameMode.gameModeTitle].SetActive(true);
            currentBirdTitle = gameMode.gameModeTitle;
            return;
        }

        birds[gameMode.gameModeTitle].SetActive(true);
        birds[gameMode.gameModeTitle].GetComponent<IPlrBird>().SetNewGameMode(gameMode, birds[currentBirdTitle]);
        birds[currentBirdTitle].SetActive(false);

        currentBirdTitle = gameMode.gameModeTitle;

        birds[currentBirdTitle].SetActive(true);
    }
}