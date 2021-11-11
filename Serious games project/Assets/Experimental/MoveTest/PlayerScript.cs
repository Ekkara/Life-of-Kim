using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
   // [SerializeField] Joystick movement;
    [SerializeField] ButtonScript leftButton, rightButton;
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

    int movingDirection;
    bool oldFlip = true;
    void Update() {
        movingDirection = 0;
        if (leftButton.IsPressed) {
            movingDirection -= 1;
            if (!rightButton.IsPressed) {
                oldFlip = false;
            }
        }
        if (rightButton.IsPressed) {
            movingDirection += 1;
            if (!leftButton.IsPressed) {
                oldFlip = true;
            }
        }


        if (!DialogueManager.Instance.isInDialogue) {
            #region oldwalking system jotstick
            /*if (movement.usingJoystick) {
                currentWalkingTimmer += Time.deltaTime;
                if (currentWalkingTimmer >= timePerSprite) {
                    currentWalkingTimmer = 0;
                    currentIndex++;
                    if (currentIndex >= playerMovement.Length) {
                        currentIndex = 0;
                    }
                    sr.sprite = playerMovement[currentIndex];
                }
                sr.flipX = movement.joyPos.x >= 0 ? true : false;
            }
            else {
                sr.sprite = playerMovement[0];
            }*/
            #endregion
            if (!movingDirection.Equals(0)) {
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
        }

        //activate interactables
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                int layerMask = ~LayerMask.GetMask("Ignore Raycast");
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos, layerMask);

                if (touchedCollider != null) {
                    Debug.Log(touchedCollider.gameObject.name);
                    Interactable interactable = touchedCollider.GetComponent<Interactable>();

                    if (interactable != null) {
                        if (interactable.inRange) {
                            interactable.InteractWithItem();
                        }
                    }
                }
            }
        }
    }
    private void FixedUpdate() {
        if (!DialogueManager.Instance.isInDialogue) {
            // rb.MovePosition(transform.position + (movement.joyPos * movementSpeed));
            rb.MovePosition(transform.position + (Vector3.right * movingDirection * movementSpeed));
        }
    }
}
