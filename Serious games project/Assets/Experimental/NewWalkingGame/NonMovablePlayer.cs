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
            //PC USE
            #region pc use

            if (Input.GetMouseButtonDown(0)) {
                StopAllCoroutines();
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                int layerMask = ~LayerMask.GetMask("Ignore Raycast");
                Collider2D touchedCollider = Physics2D.OverlapPoint(mousePos2D, layerMask);
                if (touchedCollider != null) {
                    Interactable interactable = touchedCollider.GetComponent<Interactable>();

                    if (interactable != null) {
                        interactable.InteractWithItem();
                    }
                }
            }
            #endregion
        }
    }
}
