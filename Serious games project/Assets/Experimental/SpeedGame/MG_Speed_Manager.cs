using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class MG_Speed_Manager : MonoBehaviour
{
    [SerializeField] MG_SwiperScript swipe;
    [SerializeField] MG_Selecter select;
    //pick, foodPlate;
    [SerializeField] GameObject elementHolder;
    MG_Speed_BuildingBlock[] elements;
    int currentElement = 0;
    bool elementDone = false;
    int score = 0;
    public void ChangeDoneState(bool state, int scoreChanger)
    {
        score += scoreChanger;
        elementDone = state;
    }
    // Start is called before the first frame update
    void Start()
    {
        elements = new MG_Speed_BuildingBlock[elementHolder.transform.childCount];
        for (int i = 0; i < elementHolder.transform.childCount; i++)
        {
            elements[i] = elementHolder.transform.GetChild(i).gameObject.GetComponent<MG_Speed_BuildingBlock>();
        }
        StartCoroutine(ElementFunc());
    }

    // Update is called once per frame
    void Update()
    {

    }


    [SerializeField] FlowMacro onGameEndWin,onGameEndFail;
    [SerializeField] int scoreToWin = 3;
    IEnumerator ElementFunc()
    {
        yield return new WaitForEndOfFrameUnit();
        if (elements[currentElement] is MG_Speed_Swipe)
        {
            swipe.InitiateSwipe(elements[currentElement] as MG_Speed_Swipe);
        }
        else if (elements[currentElement] is MG_Speed_Select)
        {
            select.InitiateClicker(elements[currentElement] as MG_Speed_Select);
        }

        yield return new WaitUntil(() => elementDone);
        currentElement++;
        if (currentElement < elements.Length)
        {
            elementDone = false;
            StartCoroutine(ElementFunc());
        }
        else
        {
            if (scoreToWin <= score)
            {
                DialogueManager.Instance.StartDialogue(onGameEndWin);
            }
            else
            {
                DialogueManager.Instance.StartDialogue(onGameEndFail);
            }
        }
    }
}
public abstract class MG_Speed_BuildingBlock : MonoBehaviour
{
}
