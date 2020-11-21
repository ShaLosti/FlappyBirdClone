using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GameMode : ScriptableObject
{
    public string gameModeTitle;
    public Sprite backGround;
    public GameObject plrPref;
    public Transform pipePref;
    public Transform pipeHeadPref;
    public Transform particleSystem;
    public Enums.GlobalGameMods globalGameMode;
    public Color backGroundColor;
    public Difficult difficult;

    [SerializeField]
    public AudioUnit[] audioSourse;

    public abstract void BirdGoUp(GameObject _obj);

    public abstract void BirdGoDown(GameObject _obj);
}
