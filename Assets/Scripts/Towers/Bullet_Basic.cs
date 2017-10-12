using UnityEngine;
using System.Collections;

public class Bullet_Basic : MonoBehaviour {
    
    public float speed;
    public float damage;
    public GameObject target;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(transform.position == target.transform.position) {
            if (target.tag == "Enemy") {
                target.GetComponent<EnemySet>().DoDamage(damage);
            }
            else if (target.tag == "End") {
                target.GetComponent<EndPointScript>().DoDamage(damage);
            }

            Destroy(gameObject);
            return;
        }
    }
}
