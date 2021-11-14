using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCleaner : MonoBehaviour
{
    [SerializeField] float force;
    //[SerializeField] List<GameObject> goInField = new List<GameObject>();
    [SerializeField] MG_Food_spawner spawner;
    Collider2D col;

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("FallingFood"))
        {
            goInField.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (goInField.Contains(collision.gameObject))
        {
            goInField.Remove(collision.gameObject);
        }
    }*/
    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }
    private void Update()
    {        
        for(int i = 0; i < spawner.spawnedFood.Count; i++)
        {
            if (col.OverlapPoint(spawner.spawnedFood[i].transform.position))
            {
                spawner.spawnedFood[i].transform.position += Vector3.up * force * Time.deltaTime;
            }
        }
    }

    IEnumerator DestroyItemShortly()
    {
        yield return null;
    }
}
