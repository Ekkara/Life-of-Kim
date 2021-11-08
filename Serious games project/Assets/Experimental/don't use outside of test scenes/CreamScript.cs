using UnityEngine;

public class CreamScript : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Transform offset, creamHolder;
    [SerializeField] float fallingSpeed, spawnSpeed, shortDeletePos, longDeletePos, visionRange, patrolSpeed;
    [SerializeField] GameObject particle;
    [SerializeField] Transform player, rightPoint, leftPoint;
    [SerializeField] bool reverseAttack, disableMovingToPlayer;
    Transform lastGeneratedParticle;
    [SerializeField] LineRenderer LR;

    public bool isThisParticleLatest(CreamParticleScript CPS) {
        return CPS.transform == lastGeneratedParticle;
    }
    // Start is called before the first frame update
    void Start() {

    }

    float timer = 0;
    Vector3 targetPos;
    void Update() {

        bool movingRight = false;
        float rightDistance = Mathf.Abs(leftPoint.position.x - player.position.x);
        float leftDistance = Mathf.Abs(rightPoint.position.x - player.position.x);
        Debug.Log(leftDistance + "; " + rightDistance);
        if (leftDistance <= rightDistance) {
            targetPos = new Vector3((leftPoint.position.x + player.position.x) / 2, transform.position.y, transform.position.z);

            movingRight = true;
        }
        else {
            targetPos = new Vector3((rightPoint.position.x + player.position.x) / 2, transform.position.y, transform.position.z);
        }
        //  transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * patrolSpeed);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * patrolSpeed) + new Vector3(0.05f * Mathf.Sin(Time.time * 14),0,0);

        timer += Time.deltaTime;
        if (timer >= spawnSpeed) {
            timer = 0;
            GameObject go = Instantiate(particle);
            go.transform.position = offset.position;
            go.transform.SetParent(creamHolder);
            float distance = Mathf.Sqrt((transform.position.x * transform.position.x) + (player.position.x * player.position.x));

            bool playerIsOnRightSide = player.position.x > transform.position.x;

            bool movingTowardsPlayer = (playerIsOnRightSide && movingRight) || (!playerIsOnRightSide && !movingRight);

            float currentDelPos = 0;
            if (disableMovingToPlayer) {
                if (movingTowardsPlayer) {
                    currentDelPos = shortDeletePos;
                }
                else {
                    currentDelPos = (reverseAttack ? distance >= visionRange : distance < visionRange) ? longDeletePos : shortDeletePos;
                }
            }
            else {
                currentDelPos = (reverseAttack ? distance >= visionRange : distance < visionRange) ? longDeletePos : shortDeletePos;
            }

            //            float currentDelPos = (reverseAttack ? distance >= visionRange : distance < visionRange) ? longDeletePos : shortDeletePos;
            go.GetComponent<CreamParticleScript>().GenerateParticle(currentDelPos, fallingSpeed, lastGeneratedParticle, this);
            lastGeneratedParticle = go.transform;
        }
        LR.SetPosition(0, offset.position);
        LR.SetPosition(1, lastGeneratedParticle.position);

    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, shortDeletePos, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + visionRange, transform.position.y, transform.position.z));
    }
}
