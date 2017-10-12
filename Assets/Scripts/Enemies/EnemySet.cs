using UnityEngine;
using System.Collections;

public class EnemySet : MonoBehaviour {

    public float health;
    public int counter;
    public float error;
    public float speed;

    [Header("End Info")]
    public float damage;
    public float endCounter;
    public float endCurrent;
    public GameObject bull;

    [Header("Attacking Info")]
    public bool trackPlayer;
    public float rangeNotice;
    public float rangeAttack;
    public float attackCurrent;
    public float attacTimer;

    Vector3 curPoint;

    PointCollectionScript points;
    NavMeshAgent agent;
    EndPointScript end;
    PlayerScript player;

    // Use this for initialization
    void Start () {
        points = FindObjectOfType<PointCollectionScript>();
        agent = GetComponent<NavMeshAgent>();
        error = points.error;
        end = FindObjectOfType<EndPointScript>();
        player = FindObjectOfType<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
        
        curPoint = transform.localPosition;

        if (Vector3.Distance(curPoint, player.transform.localPosition) <= rangeNotice && trackPlayer) {
            agent.destination = player.transform.localPosition;
            agent.speed = speed * 0.5f;
            if (Vector3.Distance(curPoint, player.transform.localPosition) <= rangeAttack) {
                attackCurrent += Time.deltaTime;
                agent.speed = 0;
                if(attackCurrent >= attacTimer) {
                    attackCurrent = 0;
                    Debug.Log("Attack Player");
                }
            }
        }
        else if (health <= 0) {
            Destroy(gameObject);
            return;
        }
        else if (counter == 1) {
            agent.destination = transform.position;
            agent.speed = speed;

            //do damage to end
            endCurrent += Time.deltaTime;
            if (endCurrent >= endCounter) {

                GameObject bul = (GameObject)Instantiate(bull);
                bul.transform.localPosition = transform.position;
                bul.GetComponent<Bullet_Basic>().damage = damage;
                bul.GetComponent<Bullet_Basic>().target = points.end;

                endCurrent = 0;
            }

        }
        else {
            agent.speed = speed;
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(agent.destination, path);
            if (path.status == NavMeshPathStatus.PathPartial) {
                Debug.Log("Path Destroyed");
            }

            var cur = transform.position;
            var tag = points.end.transform.position;
            if (Vector3.Distance(cur, tag) <= error) {
                counter++;
                agent.destination = gameObject.transform.localPosition;
            }
            else {
                agent.destination = points.end.transform.position;

            }
        }
        
    }

    public void DoDamage(float dam) {
        health -= dam;
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(255, 125, 0);
        Gizmos.DrawWireSphere(transform.position, rangeNotice);
        Gizmos.color = new Color(255, 0, 0, 255);
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }
}
