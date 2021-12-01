using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Selecter : MonoBehaviour
{
    [SerializeField] MG_Speed_Manager manager;
    [SerializeField] GameObject[] relevantObjects;
    [SerializeField] Slider timeSlider;
    [SerializeField] float timePerSlide = 2.5f;
    [SerializeField] Transform firstOption, secondOption, thirdOption, forthOption;
    [SerializeField] float extraTimeFirst;
    bool firstTime;

    private void Start()
    {
        firstTime = true;
        for (int i = 0; i < relevantObjects.Length; i++)
        {
            relevantObjects[i].gameObject.SetActive(false);
        }
    }
    public void InitiateClicker(MG_Speed_Select selectHolder)
    {
        StartCoroutine(Run(selectHolder));
    }

    int answer = 0;
    public void SelectOption(int value)
    {
        answer = value;
    }

    IEnumerator Run(MG_Speed_Select selectHolder)
    {
        answer = 0;
        for (int i = 0; i < relevantObjects.Length; i++)
        {
            relevantObjects[i].gameObject.SetActive(true);
        }
        GameObject firstPrefab = Instantiate(selectHolder.option1.prefab);
        firstPrefab.transform.position = firstOption.position;
        firstPrefab.transform.SetParent(firstOption);
        GameObject secondPrefab = Instantiate(selectHolder.option2.prefab);
        secondPrefab.transform.position = secondOption.position;
        secondPrefab.transform.SetParent(secondOption);
        GameObject thirdPrefab = Instantiate(selectHolder.option3.prefab);
        thirdPrefab.transform.position = thirdOption.position;
        thirdPrefab.transform.SetParent(thirdOption);
        GameObject forthPrefab = Instantiate(selectHolder.option4.prefab);
        forthPrefab.transform.position = forthOption.position;
        forthPrefab.transform.SetParent(forthOption);

        timeSlider.value = 1;

        float actualTime = firstTime ? (timePerSlide + extraTimeFirst) : timePerSlide;
         firstTime = false;

        float timeCounter = 0;
        while (answer.Equals(0))
        {           
            timeCounter += Time.deltaTime;
            timeSlider.value = Mathf.Lerp(1, 0, timeCounter / actualTime);
            if (timeSlider.value.Equals(0))
            {
                break;
            }
            yield return null;
        }
        switch (answer)
        {
            case 1:
                if (selectHolder.option1.isCorrectAnswer)
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerCorrect);
                }
                else
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerWrong);
                }
                break;
            case 2:
                if (selectHolder.option2.isCorrectAnswer)
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerCorrect);
                }
                else
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerWrong);
                }
                break;
            case 3:
                if (selectHolder.option3.isCorrectAnswer)
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerCorrect);
                }
                else
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerWrong);
                }
                break;
            case 4:
                if (selectHolder.option4.isCorrectAnswer)
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerCorrect);
                }
                else
                {
                    manager.ChangeDoneState(true, selectHolder.scoreChangerWrong);
                }
                break;
        }
        answer = 0;

        if (timeSlider.value.Equals(0))
        {
            manager.ChangeDoneState(true, selectHolder.scoreChangerCorrect);
        }

        Destroy(firstPrefab);
        Destroy(secondPrefab);
        Destroy(thirdPrefab);
        Destroy(forthPrefab);

        for (int i = 0; i < relevantObjects.Length; i++)
        {
            relevantObjects[i].gameObject.SetActive(false);
        }
    }
}
