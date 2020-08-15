using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameMods/SimpleGameMode", fileName = "New simple game mode")]
public class SimpleGameMod : GameMode
{
    public override void BirdGoUp(GameObject _obj)
    {
        var rotationVector = _obj.transform.rotation.eulerAngles;
        rotationVector.z = 20;
        _obj.transform.rotation = Quaternion.Euler(rotationVector);
    }
    public override void BirdGoDown(GameObject _obj)
    {
        var rotationVector = _obj.transform.rotation.eulerAngles;
        rotationVector.z = -20;
        _obj.transform.rotation = Quaternion.Euler(rotationVector);
    }
}

[Serializable]
public abstract class GameMode : ScriptableObject
{
    public string gameModeTitle;
    public Sprite backGround;
    public Sprite plrSprite;
    public AudioSource music;
    public Enums.GlobalGameMods globalGameMode;
    public AudioSource[] jumpShots;
    public AudioSource[] buttonEnterShots;

    public abstract void BirdGoUp(GameObject _obj);
    public abstract void BirdGoDown(GameObject _obj);
}
