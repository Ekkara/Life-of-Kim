using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;

public class BoltDialogueTrigger : MonoBehaviour
{
    [SerializeField] FlowMacro dialogue;
    // Start is called before the first frame update
    private void Start() {
       
    }
   public void TriggerDialogue() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DialogueManager.Instance.StartDialogue(dialogue);          
        }
    }
}
