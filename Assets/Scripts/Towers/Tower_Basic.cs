using UnityEngine;
using System.Collections;

public class Tower_Basic : MonoBehaviour {

    public enum AttackType {
        closest, furthest, weakest, strongest, random
    }

    float current;
    public bool shoot;
    public AttackType attacking;

    [Header("Tower Data")]
    public float speed;
    public float range;
    public float rotSpeed;
    public float damage;

    [Header("Objects")]
    public Transform rotor;
    public GameObject bullet;

    //ROTATION INFO
    public GameObject ob_close = null;
    public float dis = Mathf.Infinity;
    public float ob_dis = Mathf.Infinity;
    public float ob_hel = Mathf.Infinity;

    // Use this for initialization
    void Start() {
        current = speed;
    }

    // Update is called once per frame
    void Update() {

        if (shoot) {
            shoot = false;
            Shoot();
        }

        if(attacking == AttackType.closest) {
            FindEnemyClosest();
        }
        else if(attacking == AttackType.furthest) {
            FindEnemyFurthest();
        }
        else if(attacking == AttackType.strongest) {
            FindEnemyStrongest();
        }
        else if (attacking == AttackType.weakest) {
            FindEnemyWeakest();
        }
        else if (attacking == AttackType.random) {
            FindEnemyRandom();
        }
        else {
            FindEnemyClosest();
        }

        if (ob_close != null) {

            //rotate
            Vector3 newDir = Vector3.RotateTowards(rotor.position, ob_close.transform.position, rotSpeed * Time.deltaTime, 0f);
            rotor.rotation *= Quaternion.Euler(90, 0, 0);
            rotor.rotation = Quaternion.LookRotation(newDir);
            

            current += Time.deltaTime;
            if (current >= speed) {
                current = 0;
                Shoot();
            }
        }

    }

    void Shoot() {
        GameObject bul = (GameObject)Instantiate(bullet);
        bul.transform.position = rotor.position;
        bul.GetComponent<Bullet_Basic>().damage = damage;
        bul.GetComponent<Bullet_Basic>().target = ob_close;
    }

    void OnMouseDown() {
        //Debug.Log("Clicked");
    }

    void FindEnemyClosest() {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Enemy");
        ob_close = null;
        ob_dis = Mathf.Infinity;
        foreach (GameObject ob in obs) {
            dis = Vector3.Distance(transform.position, ob.transform.position);
            if (dis <= range) {
                if (dis < ob_dis) {
                    ob_close = ob;
                    ob_dis = dis;
                }
            }
        }
    }
    void FindEnemyFurthest() {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Enemy");
        ob_close = null;
        ob_dis = 0;
        foreach (GameObject ob in obs) {
            dis = Vector3.Distance(transform.position, ob.transform.position);
            if (dis <= range) {
                if (dis > ob_dis) {
                    ob_close = ob;
                    ob_dis = dis;
                }
            }
        }
    }
    void FindEnemyStrongest() {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Enemy");
        ob_close = null;
        ob_hel = 0;
        foreach (GameObject ob in obs) {
            dis = Vector3.Distance(transform.position, ob.transform.position);
            if (dis <= range) {
                if (ob.GetComponent<EnemySet>().health > ob_hel) {
                    ob_close = ob;
                    ob_hel = ob.GetComponent<EnemySet>().health;
                }
            }
        }
    }
    void FindEnemyWeakest() {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Enemy");
        ob_close = null;
        ob_hel = Mathf.Infinity;
        foreach (GameObject ob in obs) {
            dis = Vector3.Distance(transform.position, ob.transform.position);
            if (dis <= range) {
                if (ob.GetComponent<EnemySet>().health < ob_hel) {
                    ob_close = ob;
                    ob_hel = ob.GetComponent<EnemySet>().health;
                }
            }
        }
    }
    void FindEnemyRandom() {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Enemy");
        ob_close = null;
        foreach (GameObject ob in obs) {
            dis = Vector3.Distance(transform.position, ob.transform.position);
            if (dis <= range) {
                if(ob_close == null) {
                    ob_close = ob;
                }
                else {
                    if((Random.Range(0, 2) == 1)) {
                        ob_close = ob;
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnDrawGizmos() {
        if (ob_close != null) {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(rotor.position, ob_close.transform.position);
        }
    }
}
