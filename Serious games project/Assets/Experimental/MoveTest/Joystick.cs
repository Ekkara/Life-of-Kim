using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] JoystickScript JS;
    public Vector3 joyPos {
        get {
            return JS.joyPos;
        }
    }
    public bool usingJoystick {
        get { return JS.usingJoystick; }
    }
}
