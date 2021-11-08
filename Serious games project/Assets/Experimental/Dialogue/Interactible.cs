using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool inRange = false;
    public virtual void InteractWithItem() {
    }
}
