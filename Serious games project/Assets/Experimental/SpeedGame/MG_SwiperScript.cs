using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_SwiperScript : MonoBehaviour
{
    [SerializeField] float distanceToAnswer = 1;
    [SerializeField] MG_Speed_Manager manager;
    [SerializeField] GameObject[] relevantObjects;
    [SerializeField] Slider timeSlider;
    [SerializeField] float timePerSlide = 1;
    [SerializeField] float extraTimeFirstTime;
    bool wasFirst = true;

    private void Start()
    {
        wasFirst = true;
        for (int i = 0; i < relevantObjects.Length; i++)
        {
            relevantObjects[i].gameObject.SetActive(false);
        }
    }
    public void InitiateSwipe(MG_Speed_Swipe swipeHolder)
    {        
        StartCoroutine(Run(swipeHolder));
    }


    Touch touch;
    bool isSwiping;
    int touchIndex;
    IEnumerator Run(MG_Speed_Swipe swipeHolder)
    {
        isSwiping = false;
        bool activated = true;
        bool travelRight = swipeHolder.direction == MG_Speed_Swipe.Direction.right;
        GameObject objectToSwipe = Instantiate(swipeHolder.prefab);
        Vector3 startSwipePos = Vector3.zero;

        for(int i = 0; i < relevantObjects.Length; i++)
        {
            relevantObjects[i].gameObject.SetActive(true);
        }
        timeSlider.value = 1;

        float timeRunning = 0;
        float timeToRun = wasFirst ? (extraTimeFirstTime + timePerSlide) : timePerSlide;
        wasFirst = false;
        while (activated)
        {
            if (!isSwiping)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Debug.Log("found touch");
                        this.touch = touch;
                        isSwiping = true;
                        startSwipePos = Camera.main.ScreenToWorldPoint(touch.position);
                        touchIndex = touch.fingerId;
                        break;
                    }
                }
            }
            else
            {
                touch = MobileInputManager.Instance.GetTouchFromFinggerID(touchIndex);
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    objectToSwipe.transform.position = new Vector3(
                        touchPos.x - startSwipePos.x,
                        objectToSwipe.transform.position.y,
                        objectToSwipe.transform.position.z);
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    isSwiping = false;
                }
            }


           if (Mathf.Abs(objectToSwipe.transform.position.x) >= distanceToAnswer)
            {
                activated = false;
            }
            timeRunning += Time.deltaTime;
            timeSlider.value = Mathf.Lerp(1, 0, timeRunning / timeToRun);
            if(timeSlider.value.Equals(0))
            {
                activated = false;
            }
            yield return null;
        }

        Destroy(objectToSwipe);
        isSwiping = false;

        for (int i = 0; i < relevantObjects.Length; i++)
        {
            relevantObjects[i].gameObject.SetActive(false);
        }
        if (timeSlider.value.Equals(0))
        {
            manager.ChangeDoneState(true, swipeHolder.scoreChangerWrong);
        }
        else
        {
            bool finishedRight = objectToSwipe.transform.position.x >= 0 ? true : false;
            if (finishedRight == travelRight)
            {
                manager.ChangeDoneState(true, swipeHolder.scoreChangerCorrect);
            }
            else
            {
                manager.ChangeDoneState(true, swipeHolder.scoreChangerWrong);
            }
        }
    }
}
