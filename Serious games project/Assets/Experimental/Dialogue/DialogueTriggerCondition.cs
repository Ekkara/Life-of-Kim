using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerCondition : Interactable
{
    [SerializeField] string conditionName;
    [SerializeField] bool conditionEqual;

    [SerializeField] DialogueContainer conditionMetC;
    [SerializeField] DialogueContainer conditionDidntMetC;
    public override void InteractWithItem() {
        base.InteractWithItem();


    }
}
