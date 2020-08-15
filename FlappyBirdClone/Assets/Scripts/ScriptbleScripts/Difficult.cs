using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Difficultes/StandartDifficult", fileName = "New difficult")]
public class Difficult : ScriptableObject
{
    [Tooltip("Название сложности")]
    public string difficultyTitle = "";
    [Tooltip("Количество пайпов для данной сложности")]
    public int pipeSpawnedCapacity = 0;
    [Tooltip("Высота пайпов")]
    public float gapSize = 0;
    [Tooltip("Скорость пайпов")]
    public int pipeMoveSpeed = 0;
    [Tooltip("Промежуток времени между спавном новых пайпов")]
    public float pipeSpawnTimerMax = 0;
}