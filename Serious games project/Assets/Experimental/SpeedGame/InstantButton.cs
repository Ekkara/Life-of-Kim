using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantButton : MonoBehaviour
{
    [SerializeField] UnityEvent events;
    Collider2D colider;
    private void Start()
    {
        colider = gameObject.GetComponent<Collider2D>();
    }
    void Update()
    {
        foreach (Touch tempTouch in Input.touches)
        {
            if (tempTouch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(tempTouch.position);
                int layerMask = ~LayerMask.GetMask("Ignore Raycast");
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos, layerMask);
                if (touchedCollider != null)
                {
                    if (touchedCollider.Equals(colider))
                    {
                        events.Invoke();
                    }
                }
                break;
            }
        }
    }
}
