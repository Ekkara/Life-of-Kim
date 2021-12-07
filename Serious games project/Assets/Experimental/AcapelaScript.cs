using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcapelaScript : MonoBehaviour
{
    SpriteRenderer SR;
    [SerializeField] float time1, time2;
    // Start is called before the first frame update
    void Start()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Func());
    }
    IEnumerator Func() {

        float timeRunning = 0;
        while (true) {
            timeRunning += Time.deltaTime;
            SR.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timeRunning / (time1 / 2)));
            if (SR.color.a.Equals(1)) {
                break;
            }
            yield return null;
        }
        timeRunning = 0;
        while (true) {
            timeRunning += Time.deltaTime;
            SR.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, timeRunning / (time2 / 2)));
            if (SR.color.a.Equals(0)) {
                break;
            }
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }
}
