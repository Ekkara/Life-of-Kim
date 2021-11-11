using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool IsPressed {
        get;
        internal set;
    }

    // Start is called before the first frame update
    void Start() {
        IsPressed = false;
    }
    int touchIndex;

    public void BeginTouch() {
        foreach (Touch tempTouch in Input.touches) {
            if (tempTouch.phase == TouchPhase.Began) {
                touchIndex = tempTouch.fingerId;
                IsPressed = true;
                return;
            }
        }
    }
    

    // Update is called once per frame
    void Update() {
        if (IsPressed) {
            Touch touch = MobileInputManager.Instance.GetTouchFromFinggerID(touchIndex);
            if (touch.phase.Equals(TouchPhase.Moved)) {
                
            }
            else if (touch.phase.Equals(TouchPhase.Ended)) {
                IsPressed = false;
            }
        }
    }
}
