using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField] DialogueContainer dialogueC;

    public override void InteractWithItem() {
          //  DialogueManager.Instance.StartDialogue(dialogueC);
    }
}
