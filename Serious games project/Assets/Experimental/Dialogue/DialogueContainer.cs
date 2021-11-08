using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueContainer
{
    public DialogueC[] speechbubbles;
    public UnityEvent eventOnceDialogueIsDone;
}
[System.Serializable]
public struct DialogueC
{
    public string nameOfSpeaker;
    [TextArea(6, 6)]
    public string message;
    public float displayLetterSpeed;
    public Sprite speakerSprite;
}
