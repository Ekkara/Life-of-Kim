using UnityEngine;

public class FallingNoteScript : MonoBehaviour
{
    public bool isInteractible;
    bool isMoving;
    Collider2D col;
    [SerializeField] float fallingSpeed;

    // Start is called before the first frame update
    void Start() {
        isMoving = false;
        isInteractible = false;
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected void Update() {
        transform.position += Vector3.right * fallingSpeed * Time.deltaTime;
        UpdateMoveState();
    }
    void UpdateMoveState() {
        if (!isInteractible)
            return;
        if (Input.touchCount > 0) {
            if (MobileInputManager.Instance.firstTouch.phase.Equals(
                TouchPhase.Began)) {
                TouchBegan();
            }
            if (MobileInputManager.Instance.firstTouch.phase.Equals(
                TouchPhase.Moved)) {
                TouchMove();
            }
            if (MobileInputManager.Instance.firstTouch.phase.Equals(
                TouchPhase.Ended)) {
                TouchEnd();
            }
        }
    }

    protected void TouchBegan() {
        if (col == MobileInputManager.Instance.touchedColliders) {
            Destroy(gameObject);
            isMoving = true;
        }
    }
    protected void TouchMove() {
        if (isMoving) {
            transform.position = MobileInputManager.Instance.firstTouchPos;
        }
    }
    protected void TouchEnd() {
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        NoteActivatorScript FN = collision.gameObject.GetComponent<NoteActivatorScript>();
        if (FN != null) {
            isInteractible = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        NoteActivatorScript FN = collision.gameObject.GetComponent<NoteActivatorScript>();
        if (FN != null) {
            isInteractible = false;
        }
    }
}
