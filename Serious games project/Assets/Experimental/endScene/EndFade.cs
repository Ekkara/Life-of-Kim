using System.Collections;
using UnityEngine;

public class EndFade : MonoBehaviour
{
    [SerializeField] SpriteRenderer fade;
    [SerializeField] FadingendObject[] FO;
    [SerializeField] float timeItTakesToRemoveAScreen;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(FadingObject());
    }

    int currentIndex = 0;
    IEnumerator FadingObject() {
        while (true) {
            yield return new WaitWhile(() => DialogueManager.Instance.isInDialogue);
            break;
        }
        if (FO[currentIndex].dialogue != null) {
            FO[currentIndex].dialogue.TriggerDialogue();
            while (DialogueManager.Instance.isInDialogue) {
                yield return null;
            }
        }

        float timeRunning = 0;
        while (true) {
            timeRunning += Time.deltaTime;
            fade.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, timeRunning / (timeItTakesToRemoveAScreen / 2)));
            if (fade.color.a.Equals(1)) {
                break;
            }
            yield return null;
        }
        Destroy(FO[currentIndex].GO);
        timeRunning = 0;
        while (true) {
            timeRunning += Time.deltaTime;
            fade.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timeRunning / (timeItTakesToRemoveAScreen / 2)));
            if (fade.color.a.Equals(0)) {
                break;
            }
            yield return null;
        }
       
        while (true) {
            yield return new WaitForSeconds(FO[currentIndex].waitOnThisFrame);
            break;
        }

        currentIndex++;
        if (currentIndex < FO.Length) {
            StartCoroutine(FadingObject());
        }
    }
}
[System.Serializable]
public struct FadingendObject
{
    public float waitOnThisFrame;
    public GameObject GO;
    public BoltDialogueTrigger dialogue;
}
