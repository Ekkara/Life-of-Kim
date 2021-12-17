using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ludiq;
using Bolt;

public class DialogueManager : MonoBehaviour
{
   
    private static DialogueManager _instance;
    public static DialogueManager Instance { get { return _instance; } }
    private void Awake() {
        
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            Debug.LogError("too many dialoguemanagers in the scene!");
        }
        else {
            _instance = this;
        }
    }

    public bool isInDialogue {
        internal set;
        get;
    }
    public bool CanMoveInTree {
        set;
        get;
    }
    [SerializeField] GameObject dialogueObjectHolder;
    [SerializeField] Text _name, mainText;
    FlowMacro GN;
    [SerializeField] AudioSource dialogueAudioSoruce;
    [SerializeField] Image speakingAvatar;

    private void Start() {
        dialogueObjectHolder.SetActive(false);
        isInDialogue = false;
        wasLastNode = false;

        firstOptionText = firstOption.transform.GetChild(0).gameObject.GetComponent<Text>();
        secondOptionText = seconOption.transform.GetChild(0).gameObject.GetComponent<Text>();
        thirdOptionText = thirdOption.transform.GetChild(0).gameObject.GetComponent<Text>();
        forthOptionText = forthOption.transform.GetChild(0).gameObject.GetComponent<Text>();
}
    public void StartDialogue(FlowMacro FM) {
        if (isInDialogue) {
            Debug.LogError("You cant start a new dialogue during a conversation!");
            return;
        }
        isInDialogue = true;
        speakingAvatar.gameObject.SetActive(false);
        dialogueObjectHolder.SetActive(true);
        gameObject.GetComponent<FlowMachine>().nest.macro = FM;
        CustomEvent.Trigger(gameObject, "StartDialogue", 0);
    }
    public void UpdateDialogBox(string name, string message, float letterApearSpeed, Sprite pfp, AudioClip audioClip, bool prioritized = false) {

        speakingAvatar.gameObject.SetActive(pfp != null);
        if(pfp != null) {
            speakingAvatar.sprite = pfp;
        }
       
        //display the dialogue object
        dialogueObjectHolder.SetActive(true);

        //generate the dialogue box
        DialogueC DC = new DialogueC();
        DC.displayLetterSpeed = letterApearSpeed;
        DC.message = message;
        DC.nameOfSpeaker = name;

        mainText.text = "";
        this._name.text = "";

        //start the new dialogue animation
        StartCoroutine(DisplayDialogue(DC));
        if (audioClip != null) {
            dialogueAudioSoruce.PlayOneShot(audioClip);
        }
    }
    public void StartWasLastSearch() {
        wasLastNode = true;
        StartCoroutine(EndDialogueFunc());
    }
    public void EndWasLastSearch() {
        wasLastNode = false;
        StopAllCoroutines();
    }
    public void EndDialogue() {
        isInDialogue = false;
        dialogueObjectHolder.SetActive(false);
        Debug.Log("conversation over");
    }


    bool ContinueButton = false;
    public void PressedContinue() {
        dialogueAudioSoruce.Stop();
        ContinueButton = true;
    }

    public bool wasLastNode {
        set;
        get;
    }
    IEnumerator EndDialogueFunc() {
        yield return new WaitForNextFrameUnit();
        yield return new WaitForEndOfFrameUnit();

        if (wasLastNode) {
            EndDialogue();
        }
    }

    IEnumerator DisplayDialogue(DialogueC DC) {
        _name.text = DC.nameOfSpeaker;
        string displayedMessage = "";
        for (int j = 0; j < DC.message.Length; j++) {
            displayedMessage += DC.message.Substring(j, 1);
            mainText.text = displayedMessage;
            yield return new WaitForSeconds(DC.displayLetterSpeed);

            if (ContinueButton) {
                mainText.text = DC.message;
                ContinueButton = false;
                break;
            }
        }
        yield return new WaitUntil(() => ContinueButton);
        ContinueButton = false;
        CanMoveInTree = true;
    }
    #region branching
    [SerializeField] GameObject firstOption, seconOption, thirdOption, forthOption;
    Text firstOptionText, secondOptionText, thirdOptionText, forthOptionText;
    public int branchPath {
        set;
        get;
    }
    public void SetBranchNumber(int number) {
        branchPath = number;
    }

    public void SetUpBranching(int amount, string firstText, string secondText, string thirdText, string forthText) {
        mainText.text = "";
        _name.text = "";

        firstOptionText.text = firstText;
        secondOptionText.text = secondText;
        thirdOptionText.text = thirdText;
        forthOptionText.text = forthText;

        switch (amount) {
            case 1:
                firstOption.SetActive(true);
                seconOption.SetActive(false);
                thirdOption.SetActive(false);
                forthOption.SetActive(false);
                break;

            case 2:
                firstOption.SetActive(true);
                seconOption.SetActive(true);
                thirdOption.SetActive(false);
                forthOption.SetActive(false);
                break;

            case 3:
                firstOption.SetActive(true);
                seconOption.SetActive(true);
                thirdOption.SetActive(true);
                forthOption.SetActive(false);
                break;

            case 4:
                firstOption.SetActive(true);
                seconOption.SetActive(true);
                thirdOption.SetActive(true);
                forthOption.SetActive(true);
                break;

        }
    }

    public void SetDownBranching() {
        firstOption.SetActive(false);
        seconOption.SetActive(false);
        thirdOption.SetActive(false);
        forthOption.SetActive(false);
        branchPath = 0;
    }
    #endregion

    //fork stuff
    Dictionary<string, List<string>> savedForkTags = new Dictionary<string, List<string>>();

    public bool IsForkedUsed(string Scene, string tag) {
        if (savedForkTags.ContainsKey(Scene)) {
            return savedForkTags[Scene].Contains(tag);
        }
        else {
            return false;
        }
    }
    public void AddUsedFork(string scene, string tag) {
        if (!savedForkTags.ContainsKey(scene)) {
            savedForkTags.Add(scene, new List<string>());
        }
        savedForkTags[scene].Add(tag);
    }

    //Scen enter
    List<string> enteredSceneNames = new List<string>();
    public bool FirstTimeEnteredScene(string sceneName)
    {
        return enteredSceneNames.Contains(sceneName);
    }
    public void AddSceneName(string sceneName)
    {
        if (!enteredSceneNames.Contains(sceneName))
        {
            enteredSceneNames.Add(sceneName);
        }
    }
}
