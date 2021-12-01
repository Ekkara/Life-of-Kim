using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] Sprite[] playerMovement;
    [SerializeField] float timePerSprite;
    SpriteRenderer sr;
    

    private void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    float currentWalkingTimmer = 0;
    int currentIndex = 0;

    bool oldFlip = true;
    bool walkingToADestination = false;

    void Update() {

        if (walkingToADestination) {
            currentWalkingTimmer += Time.deltaTime;
            if (currentWalkingTimmer >= timePerSprite) {
                currentWalkingTimmer = 0;
                currentIndex++;
                if (currentIndex >= playerMovement.Length) {
                    currentIndex = 0;
                }
                sr.sprite = playerMovement[currentIndex];
            }
            sr.flipX = oldFlip;
        }
        else {
            sr.sprite = playerMovement[0];
        }

        if (!DialogueManager.Instance.isInDialogue) {

            //activate interactables
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    StopAllCoroutines();

                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    int layerMask = ~LayerMask.GetMask("Ignore Raycast");
                    Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos, layerMask);

                    if (touchedCollider != null) {
                        Interactable interactable = touchedCollider.GetComponent<Interactable>();


                        if (interactable != null) {
                            StartCoroutine(WalkToPoint(transform.position.x, touchPos.x, interactable));
                        }
                        else {
                            StartCoroutine(WalkToPoint(transform.position.x, touchPos.x));
                        }
                    }
                    else {
                        StartCoroutine(WalkToPoint(transform.position.x, touchPos.x));
                    }
                }
            }
        }
    }

    IEnumerator WalkToPoint(float fromPos, float toPos, Interactable inter = null) {
        oldFlip = (toPos > transform.position.x) ? true : false;
        walkingToADestination = true;
        float timePassed = 0.0f;
        while (!transform.position.x.Equals(toPos)) {

            transform.position = new Vector3(
                Mathf.Lerp(fromPos, toPos, timePassed / (Mathf.Abs(fromPos - toPos) / movementSpeed)),
                transform.position.y, transform.position.z);

            timePassed += Time.deltaTime;
            yield return null;
        }
        if (inter != null) {
            inter.InteractWithItem();
        }
        walkingToADestination = false;
    }
}
