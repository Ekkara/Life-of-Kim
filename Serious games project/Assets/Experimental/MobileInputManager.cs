using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    private static MobileInputManager _instance;
    public static MobileInputManager Instance { get { return _instance; } }

    public Touch GetTouchFromFinggerID(int id) {
        foreach(Touch touch in Input.touches) {
            if (touch.fingerId.Equals(id)) {
                return touch;
            }
        }
        Debug.LogWarning("no touches maching the id were found!");
        return Input.GetTouch(0);
    }

    public Touch firstTouch {
        get;
        internal set;
    }
    public Vector2 firstTouchPos {
        get;
        internal set;
    }
    public Collider2D touchedColliders {
        get;
        internal set;
    }
    private void Update() {
        if (Input.touchCount > 0) {
            firstTouch = Input.GetTouch(0);
            firstTouchPos = Camera.main.ScreenToWorldPoint(firstTouch.position);
            touchedColliders = Physics2D.OverlapPoint(firstTouchPos);
        }
        else {
            touchedColliders = null;
        }

        Debug.ClearDeveloperConsole();
        if (touchedColliders != null) {
        }
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            // Destroy(gameObject);
            Destroy(GetComponent<MobileInputManager>());
            Debug.LogError("too many inputmanagers in the scene!");
        }
        else {
            _instance = this;
        }
    }
}
