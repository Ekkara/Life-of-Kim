using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MG_Food_Food : MonoBehaviour
{
    float velocitySpeedChange;
    FoodType foodType;
    float speed;
    Rigidbody2D rb;
    Vector3 dir;
    Collider2D collider;
    GameObject end;
    MG_Food_spawner spawner;
    public void generateFunc(float speed, float velocitySpeedChange, FoodType foodType, GameObject end, MG_Food_spawner spawner)
    {
        this.speed = speed;
        this.velocitySpeedChange = velocitySpeedChange;
        this.foodType = foodType;
        this.end = end;
        this.spawner = spawner;
        dir = new Vector3(speed, 0, 0);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir;

        collider = gameObject.GetComponent<Collider2D>();
    }


    int touchIndex;

    public bool movingFood = false;
    Vector3 oldPos = new Vector3();



    private void Update()
    {
        if (transform.position.x >= 10)
        {
            spawner.RemoveFood(gameObject);
        }

        if (!movingFood)
        {
            foreach (Touch tempTouch in Input.touches)
            {
                if (tempTouch.phase == TouchPhase.Began)
                {
                    if (collider.OverlapPoint(Camera.main.ScreenToWorldPoint(tempTouch.position)))
                    {
                        touchIndex = tempTouch.fingerId;
                        movingFood = true;
                        rb.velocity = Vector2.zero;
                        oldPos = Camera.main.ScreenToWorldPoint(tempTouch.position);
                        StopAllCoroutines();
                        break;
                    }
                }
            }
        }
        else
        {
            Touch touch = MobileInputManager.Instance.GetTouchFromFinggerID(touchIndex);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase.Equals(TouchPhase.Moved))
            {
                transform.position = mousePos;
            }
            else if (touch.phase.Equals(TouchPhase.Ended))
            {
                //start cor
                Vector2 direc = mousePos - new Vector2(transform.position.x, transform.position.y);
                direc.Normalize();
                direc *= speed;

                rb.velocity = new Vector2(rb.velocity.x, direc.y);
                
                    StartCoroutine(ChangeVelocity(direc));
               
                movingFood = false;
            }
        }
    }

    IEnumerator ChangeVelocity(Vector3 currentDir)
    {
        float elapsedTime = 0;
        while (!rb.velocity.y.Equals(0))
        {
            // s/t=v => t = s/v
            Vector2 vel = rb.velocity;
            vel.x = Mathf.Lerp(vel.x, speed, elapsedTime / (Mathf.Abs(currentDir.x - speed) / velocitySpeedChange));
            vel.y = Mathf.Lerp(vel.y, 0, elapsedTime / (Mathf.Abs(currentDir.y) / velocitySpeedChange));
            rb.velocity = vel;


            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(end))
        {
            switch (foodType)
            {
                case FoodType.Garbage:
                    spawner.AteFood(-4);
                    break;
                case FoodType.Healthy:
                    spawner.AteFood(1);
                    break;
            }
        }
    }
}

