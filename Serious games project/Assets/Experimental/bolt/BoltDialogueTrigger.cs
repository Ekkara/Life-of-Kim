using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;

public class BoltDialogueTrigger : Interactable
{
    [SerializeField] FlowMacro dialogue;
    [SerializeField] bool forceActivation = false;
    public override void InteractWithItem() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    public void TriggerDialogue() {
        InteractWithItem();
    }
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space) || forceActivation) {
            InteractWithItem();
            forceActivation = false;
        }
    }
}
