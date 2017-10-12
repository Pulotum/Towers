using UnityEngine;
using System.Collections;

public class PointCollectionScript : MonoBehaviour {

    public float error;
    public Vector3 size;
    public GameObject[] start;
    public GameObject end;

    //random code
    //start[Random.Range(0, start.Length)]

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach(GameObject ob in start) {
            Gizmos.DrawLine(ob.transform.position, end.transform.position);
            Gizmos.DrawWireCube(ob.transform.position, size);
        }
        Gizmos.DrawWireSphere(end.transform.position, error);
    }

    //random from array
    
	
}
