using System;
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