using UnityEngine;

public interface IPlrBird
{
    void SetNewGameMode(GameMode gameMode, GameObject prevPlrBird);

    void SetNativeGameMode(GameMode gameMode);

    bool AllowMove { get; set; }
}