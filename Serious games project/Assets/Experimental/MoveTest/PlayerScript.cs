using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Joystick movement;
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

    void Update() {
        if (!DialogueManager.Instance.isInDialogue) {
            if (movement.usingJoystick) {
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
            rb.MovePosition(transform.position + (movement.joyPos * movementSpeed));
        }
    }
}
