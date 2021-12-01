using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] Transform leftBoarder, rightBoarder, ObjectToFollow;
    [SerializeField] float s;
    float xMin, xMax;
    private void Start() {
        xMin = leftBoarder.position.x + s;
        xMax = rightBoarder.position.x - s;
    }
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Update() {
        Vector3 targetPosition = Vector3.zero;

        if (ObjectToFollow.position.x < xMin) {
            targetPosition = new Vector3(xMin, transform.position.y, transform.position.z);
        }
        else if(ObjectToFollow.position.x > xMax) {
            targetPosition = new Vector3(xMax, transform.position.y, transform.position.z);
        }
        else {
            targetPosition = new Vector3(ObjectToFollow.position.x, transform.position.y, transform.position.z);
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * s));
    }
}
