using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public Dictionary<string, GameObject> particleSystems = new Dictionary<string, GameObject>();
    public string currentGameMode = "";

    private void Awake()
    {
        foreach (var item in Level.GetInstance.gameMods.GameModes)
        {
            if (particleSystems.ContainsKey(item.gameModeTitle) || item.particleSystem == null)
                continue;

            particleSystems.Add(item.gameModeTitle, Instantiate(item.particleSystem.gameObject, transform));
            particleSystems[item.gameModeTitle].SetActive(false);
        }
    }

    public void SetNewGameMode(GameMode gameMode)
    {
        if (particleSystems.Count == 0)
            return;
        if (currentGameMode == "")
        {
            if(particleSystems.ContainsKey(gameMode.gameModeTitle))
                particleSystems[gameMode.gameModeTitle].SetActive(true);
            currentGameMode = gameMode.gameModeTitle;
            return;
        }
        if (particleSystems.ContainsKey(currentGameMode))
            particleSystems[currentGameMode].SetActive(false);
        currentGameMode = gameMode.gameModeTitle;
        if (particleSystems.ContainsKey(currentGameMode))
            particleSystems[currentGameMode].SetActive(true);
    }

    public void GameFinished()
    {

    }
}
