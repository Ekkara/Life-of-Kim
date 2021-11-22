using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.SceneManagement;

public class BoltDialogueTriggerEnterScene : Interactable
{
    [SerializeField] FlowMacro dialogue;
    bool preventForce = false;
    
    private void Start()
    {
        preventForce = true;
        InteractWithItem();
    }
    public override void InteractWithItem()
    {
        if (preventForce)
        {
            if (!DialogueManager.Instance.FirstTimeEnteredScene(SceneManager.GetActiveScene().name))
            {
                DialogueManager.Instance.StartDialogue(dialogue);
            }
            preventForce = false;
        }
    }
    public void TriggerDialogue()
    {
        InteractWithItem();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithItem();
        }
    }
}

