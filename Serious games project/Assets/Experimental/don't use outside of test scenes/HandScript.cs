using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HandScript : MonoBehaviour
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




      void TouchBegan() {
        if (col == MobileInputManager.Instance.touchedColliders) {
            isMoving = true;
        }
    }
      void TouchMove() {
        if (isMoving) {
            transform.position = 
                new Vector3(
                    MobileInputManager.Instance.firstTouchPos.x, 
                    transform.position.y, 
                    transform.position.z);
        }
    }
      void TouchEnd() {
        isMoving = false;
    }
}
