using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MG_Food_spawner : MonoBehaviour
{
    
    [SerializeField] FoodItem[] foodItem;
    [SerializeField] Transform holder;
    [SerializeField] GameObject end;
    [SerializeField] float spawnMinRate, spawnMaxRate, xOffsetSpawner, yMaxPos, yMinPos, gameDuration;
    float currentCounter, counterGoal;
   [HideInInspector] public List<GameObject> spawnedFood = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateNewGoal();
    }
    void GenerateNewGoal()
    {
        counterGoal = Random.Range(spawnMinRate, spawnMaxRate);
    }

    string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        //float fraction = time * 1000;
        //fraction = (fraction % 1000);
        string timeText = System.String.Format("{0:00}:{1:00}", minutes, seconds + 0.8f);
        return timeText;
    }
    [SerializeField] UnityEvent UEWhenDone;
    [SerializeField] Text timerText;
    bool stopOnce = false;
    void Done()
    {
        UEWhenDone.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameDuration <= 0)
        {
            if (spawnedFood.Count <= 0)
            {
                if (!stopOnce)
                {
                    stopOnce = true;
                    Invoke("Done", 0.4f);
                    /*for (int i = spawnedFood.Count - 1; i >= 0; i--)
                    {
                        RemoveFood(spawnedFood[i]);
                    }*/
                }
            }
            timerText.text = "00:00";
            return;
        }
        gameDuration -= Time.deltaTime;
        timerText.text = FormatTime(gameDuration);
        

        currentCounter += Time.deltaTime;
        if (currentCounter >= counterGoal)
        {
            currentCounter = 0;
            GenerateNewGoal();

            List<int> shuffleList = new List<int>();
            for (int i = 0; i < foodItem.Length; i++)
            {
                for (int j = 0; j < foodItem[i].spawnRelativeValue; j++)
                {
                    shuffleList.Add(i);
                }
            }
            if (shuffleList.Count > 0)
            {
                int objectIndex = Random.Range(0, shuffleList.Count);
                GameObject GO = Instantiate(foodItem[shuffleList[objectIndex]].GO, new Vector3(xOffsetSpawner, Random.Range(yMinPos, yMaxPos), 0), Quaternion.identity);
                GO.transform.parent = holder;

                GO.GetComponent<MG_Food_Food>().generateFunc(
                    Random.Range(foodItem[shuffleList[objectIndex]].minFallSpeed, foodItem[shuffleList[objectIndex]].maxFallSpeed),
                    foodItem[shuffleList[objectIndex]].velocityChangePos,
                    foodItem[shuffleList[objectIndex]].foodType,
                    end,
                    this);
                spawnedFood.Add(GO);
            }
        }

        /* foreach (Touch touch in Input.touches)
         {
             if (touch.phase == TouchPhase.Began)
             {
                 Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                 int layerMask = ~LayerMask.GetMask("Ignore Raycast");
                 Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos, layerMask);

                 if (touchedCollider != null)
                 {
                     MG_Food_Food food = touchedCollider.GetComponent<MG_Food_Food>();

                     if (food != null)
                     {

                     }
                 }
             }
         }*/

    }

    int score;
    [SerializeField] Text scoreText;
    public void AteFood(int scoreChange)
    {
        score += scoreChange;
        scoreText.text = score.ToString();
    }
    public void RemoveFood(GameObject GO)
    {
        spawnedFood.Remove(GO);
        Destroy(GO);
    }
}
public enum FoodType
{
    Garbage,
    Healthy
}
[System.Serializable]
public struct FoodItem
{
    public GameObject GO;
    public int spawnRelativeValue;
    public float minFallSpeed, maxFallSpeed, velocityChangePos;
    public FoodType foodType;
}

