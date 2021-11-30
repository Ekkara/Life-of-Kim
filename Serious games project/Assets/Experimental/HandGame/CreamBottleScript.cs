using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamBottleScript : MonoBehaviour
{
    [SerializeField] float range, changeSpeed, waveLength;
    float startXPos;
    bool headingRight = true;
    bool programRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        startXPos = transform.position.x;
        programRunning = true;
        StartCoroutine(move(transform.position.x, startXPos + range));
    }

    // Update is called once per frame
    void Update() {
    }

    IEnumerator move(float oldPos, float newPos) {
        float timePassed = 0;
        float estimatedTime = (Mathf.Abs(oldPos - newPos) / changeSpeed);
        while (timePassed < estimatedTime) {
            transform.position = new Vector3(
                Mathf.Lerp(oldPos, newPos, timePassed / (Mathf.Abs(oldPos - newPos) / changeSpeed)),
                transform.position.y,
                transform.position.z);
            transform.position += new Vector3(waveLength * Mathf.Sin(Time.time), 0, 0);
            timePassed += Time.deltaTime;
            yield return null;
        }
        headingRight = !headingRight;
        if (headingRight) {
            StartCoroutine(move(transform.position.x, startXPos + range));
        }
        else {
            StartCoroutine(move(transform.position.x, startXPos - range));
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + range, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - range, transform.position.y, transform.position.z));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + range + waveLength, transform.position.y, transform.position.z), new Vector3(transform.position.x + range, transform.position.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x - range - waveLength, transform.position.y, transform.position.z), new Vector3(transform.position.x - range, transform.position.y, transform.position.z));
    }
}
