using UnityEngine;

[CreateAssetMenu(menuName = "GameMods/NoDeathMod", fileName = "New no death mod")]
public class NoDeathMod : GameMode
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