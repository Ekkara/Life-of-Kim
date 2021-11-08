using UnityEngine;

public class EnableInteractables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        if (interactable != null) {
            interactable.inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        if (interactable != null) {
            interactable.inRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        if (interactable != null) {
            interactable.inRange = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        if (interactable != null) {
            interactable.inRange = false;
        }
    }
}
