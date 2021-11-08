using UnityEngine;

public class DialogueTriggerFork : Interactable
{
    bool playedDialogue = false;
    [SerializeField] DialogueContainer firstDialogueC, forkedDialogueC;

    public override void InteractWithItem() {
        base.InteractWithItem();

        // base.InteractWithItem();

        if (playedDialogue) {
            //DialogueManager.Instance.StartDialogue(forkedDialogueC);
        }
        else {
           // DialogueManager.Instance.StartDialogue(firstDialogueC);
            playedDialogue = true;

        }
    }
}
