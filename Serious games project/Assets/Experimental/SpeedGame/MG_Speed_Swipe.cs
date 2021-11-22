using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Speed_Swipe : MG_Speed_BuildingBlock
{
    public enum Direction
    {
        right,
        left
    }
    public Direction direction;
    public GameObject prefab;
    public int scoreChangerCorrect, scoreChangerWrong;
}
