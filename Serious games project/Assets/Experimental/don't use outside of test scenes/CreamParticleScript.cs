using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamParticleScript : MonoBehaviour
{

    float yDestroyPos, fallSpeed;
    Transform previousParticle;
    LineRenderer LR;
    CreamScript cs;

    public void GenerateParticle(float yDestroyPos, float fallSpeed, Transform previousParticle, CreamScript CS) {
        this.yDestroyPos = yDestroyPos;
        this.fallSpeed = fallSpeed;
        this.previousParticle = previousParticle;
        LR = gameObject.GetComponent<LineRenderer>();
        cs = CS;
    }

    void Update()
    {
        transform.position = new Vector3(
               transform.position.x,
               transform.position.y - (fallSpeed * Time.deltaTime),
               transform.position.z);

        if (transform.position.y <= yDestroyPos) {
            Destroy(gameObject);
            return;
        }
        LR.enabled = (previousParticle != null) ? true : false;
        if (LR.enabled) {
            LR.SetPosition(0, transform.position);
            LR.SetPosition(1, previousParticle.position);
        }
    }
}
