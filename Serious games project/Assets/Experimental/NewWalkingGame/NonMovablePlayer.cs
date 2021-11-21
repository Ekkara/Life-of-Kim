using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMovablePlayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (!DialogueManager.Instance.isInDialogue)
        {

            //activate interactables
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    StopAllCoroutines();

                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    int layerMask = ~LayerMask.GetMask("Ignore Raycast");
                    Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos, layerMask);

                    if (touchedCollider != null)
                    {
                        Interactable interactable = touchedCollider.GetComponent<Interactable>();


                        if (interactable != null)
                        {
                            interactable.InteractWithItem();
                        }
                    }
                }
            }
        }
    }
}
