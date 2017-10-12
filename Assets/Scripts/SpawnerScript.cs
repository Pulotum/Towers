using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

    public bool spawn;
    public bool permanent;
    public int maxSpawn;

    public float current;
    public float timer;

    [Header("Enemy Info")]
    public GameObject[] enemies;

    PointCollectionScript points;
    UIScript ui;

    [Header("DEBUG")]
    public Vector3 spun;
    public Vector3 area;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public Vector3 vec;

    // Use this for initialization
    void Start () {
        points = FindObjectOfType<PointCollectionScript>();
        ui = FindObjectOfType<UIScript>();
	}
	
	// Update is called once per frame
	void Update () {

        ui.nextSpawn.text = "Next Spawn: " + (timer - current);
        ui.maxEnemy.text = "Max Enemy: " + maxSpawn;

        current += Time.deltaTime;
        if(current >= timer) {
            current = 0;
            Spawn(enemies[Random.Range(0, enemies.Length)]);
        }

        if (spawn) {
            spawn = permanent;
            Spawn(enemies[Random.Range(0, enemies.Length)]);
        }
	}


    public void Spawn(GameObject enemy)
    {

        if(GameObject.FindGameObjectsWithTag("Enemy").Length < maxSpawn) {

            spun = points.start[Random.Range(0, points.start.Length)].transform.position;
            area = points.size;

            minX = spun.x + area.x / 2;
            maxX = spun.x - area.x / 2;
            minZ = spun.z + area.z / 2;
            maxZ = spun.z - area.z / 2;

            vec = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));

            GameObject ob = (GameObject)Instantiate(enemy, vec, Quaternion.identity, GameObject.Find("Enemies").transform);
            //ob.transform.position = ;

        }
    }
}
