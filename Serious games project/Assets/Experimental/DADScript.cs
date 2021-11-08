using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DADScript : MonoBehaviour
{
    [SerializeField] bool isInteractible = true;
    [SerializeField] bool isMoving;
    Collider2D col;

    // Start is called before the first frame update
    void Start() {
        isMoving = false;
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
   protected virtual void Update() {
        UpdateMoveState();
    }
    protected void UpdateMoveState() {
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

    protected virtual void TouchBegan() {      
        if (col == MobileInputManager.Instance.touchedColliders) {
            isMoving = true;
        }
    }
    protected virtual void TouchMove() {
        if (isMoving) {
            transform.position = MobileInputManager.Instance.firstTouchPos;
        }
    }
    protected virtual void TouchEnd() {
        isMoving = false;
    }
}
