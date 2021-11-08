using UnityEngine;

public class JoystickScript : MonoBehaviour
{
    public bool usingJoystick {
        get;
        internal set;
    }
    public Vector2 joyPos {
        get {
            return new Vector2(
                (transform.position.x - startPos.x) / movingRange,
                (transform.position.y - startPos.y) / movingRange);
        }
    }
    [SerializeField] RectTransform movingField;
    [SerializeField] float movingAreaScalar;
    [SerializeField] float movingRange = 0.5f;
    int touchIndex;
    Vector2 startPos;
    private void OnValidate() {
        movingRange = movingField.lossyScale.x * movingAreaScalar;
    }
    // Start is called before the first frame update
    void Start() {
        usingJoystick = false;
        startPos = transform.position;
    }

    public void BeginDrag() {
        foreach (Touch tempTouch in Input.touches) {
            if (tempTouch.phase == TouchPhase.Began) {
                touchIndex = tempTouch.fingerId;
                usingJoystick = true;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (usingJoystick) {
            Touch touch = MobileInputManager.Instance.GetTouchFromFinggerID(touchIndex);
            if (touch.phase.Equals(TouchPhase.Moved)) {
                if (Vector2.Distance(touch.position, startPos) <= movingRange) {
                    transform.position = touch.position;
                }
                else {
                    Vector2 dir = touch.position - startPos;
                    transform.position = startPos + (dir.normalized * movingRange);
                }
            }
            else if (touch.phase.Equals(TouchPhase.Ended)) {
                transform.position = startPos;
                usingJoystick = false;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, movingRange);
    }
}
