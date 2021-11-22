using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Speed_Select : MG_Speed_BuildingBlock
{
    public SpeedSelect option1, option2, option3, option4;
    public int scoreChangerCorrect, scoreChangerWrong;
}

[System.Serializable]
public struct SpeedSelect
{
    public GameObject prefab;
    public bool isCorrectAnswer;
}