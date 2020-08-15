using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficultes/StandartDifficultes", fileName = "New difficult database")]
public class DifficultData : ScriptableObject
{
    [SerializeField, HideInInspector]
    public List<Difficult> difficultList;

    [SerializeField]
    private Difficult currentDifficult;

    private int currentIndex = 0;

    public int GetLenghth()
    {
        return difficultList.Count;
    }
    public void AddDifficulty()
    {
        if (difficultList == null)
            difficultList = new List<Difficult>();

        currentDifficult = new Difficult();
        if (!difficultList.Contains(currentDifficult) && currentDifficult.difficultyTitle != "")
            difficultList.Add(currentDifficult);

        currentIndex = difficultList.Count - 1;
    }

    public void RemoveDifficulty()
    {
        if(currentIndex > 0)
        {
            currentDifficult = difficultList[--currentIndex];
            difficultList.RemoveAt(++currentIndex);
        }
        else
        {
            difficultList.Clear();
            currentDifficult = null;
        }
    }

    public Difficult GetNext()
    {
        if (currentIndex < difficultList.Count - 1)
            currentIndex++;

        currentDifficult = this[currentIndex];
        return currentDifficult;
    }
    public Difficult GetPrev()
    {
        if (currentIndex > 0)
            currentIndex--;

        currentDifficult = this[currentIndex];
        return currentDifficult;
    }

    public void ClearData()
    {
        difficultList.Clear();
        difficultList.Add(new Difficult());
        currentDifficult = difficultList[0];
        currentIndex = 0;
    }
    public Difficult this[int index]
    {
        get
        {
            if (difficultList != null && index >= 0 && index < difficultList.Count)
                return difficultList[index];
            return null;
        }
        set
        {
            if (difficultList == null)
                difficultList = new List<Difficult>();

            if (index >= 0 && index < difficultList.Count && value != null)
                difficultList[index] = value;
            else Debug.LogError("Out of massive or sended data == null");
        }
    }
}

[Serializable]
public class Difficult
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
