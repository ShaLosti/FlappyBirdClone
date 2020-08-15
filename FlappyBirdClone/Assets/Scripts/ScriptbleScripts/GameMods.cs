using UnityEngine;

[CreateAssetMenu(menuName = "GameMods/GameModesHandler", fileName = "New game modes")]
public class GameMods : ScriptableObject
{
    [SerializeField]
    private GameMode[] gameModes;

    public GameMode[] GameModes { get => gameModes; }
}
