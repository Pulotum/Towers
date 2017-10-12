using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    [Header("Placement Info")]
    public float placementRange;
    public float placementSize;
    public bool canPlace;
    public bool currentPlace;

    [Header("Tower Data")]
    public int maxCount;
    public int currentTower;
    public GameObject[] towers;

    [Header("Weapon/Tools")]
    public int currentWeapon;
    public GameObject hammer;
    public GameObject[] weapons;

    [Header("Required")]
    public GameObject weaponPosition;
    public Terrain terrain;
    public GameObject position;
   
    Camera mainCamera;
    Vector3 pointToLook;
    GameObject circle;
    UIScript ui;

	// Use this for initialization
	void Start () {
        mainCamera = FindObjectOfType<Camera>();
        ui = FindObjectOfType<UIScript>();
	}

    // Update is called once per frame
    void Update() {

        ui.buildMode.text = "Build Mode: " + canPlace;
        ui.towerChoice.text = "Tower Choice: " + currentTower;
        ui.weaponChoice.text = "Weapon Choice: " + currentWeapon;
        ui.maxTower.text = "Max Tower: " + maxCount;

        //can plcae
        if (canPlace == true && canPlace != currentPlace) {
            currentPlace = canPlace;
            foreach (Transform child in weaponPosition.transform) {
                Destroy(child.gameObject);
            }
            GameObject ob = (GameObject)Instantiate(hammer, weaponPosition.transform);
            ob.transform.localPosition = new Vector3(0, 0, 0);
            ob.transform.localRotation = Quaternion.identity;
        }
        //not place
        else if (canPlace == false && canPlace != currentPlace) {
            currentPlace = canPlace;
            foreach (Transform child in weaponPosition.transform) {
                Destroy(child.gameObject);
            }
            GameObject ob = (GameObject)Instantiate(weapons[currentWeapon-1], weaponPosition.transform);
            ob.transform.localPosition = new Vector3(0, 0, 0);
            ob.transform.localRotation = Quaternion.identity;
        }

        //get towers/wapons number
        if (canPlace) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { currentTower = 1; }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { currentTower = 2; }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { currentTower = 3; }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) { currentTower = 4; }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { currentTower = 5; }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) { currentTower = 6; }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) { currentTower = 7; }
            else if (Input.GetKeyDown(KeyCode.Alpha8)) { currentTower = 8; }
            else if (Input.GetKeyDown(KeyCode.Alpha9)) { currentTower = 9; }
            if(currentTower >= towers.Length) {
                currentTower = towers.Length;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { currentWeapon = 1; }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { currentWeapon = 2; }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { currentWeapon = 3; }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) { currentWeapon = 4; }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { currentWeapon = 5; }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) { currentWeapon = 6; }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) { currentWeapon = 7; }
            else if (Input.GetKeyDown(KeyCode.Alpha8)) { currentWeapon = 8; }
            else if (Input.GetKeyDown(KeyCode.Alpha9)) { currentWeapon = 9; }
            if (currentWeapon >= weapons.Length) {
                currentWeapon = weapons.Length;
            }
        }



        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            canPlace = true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            canPlace = false;
        }


        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (terrain.GetComponent<Collider>().Raycast(cameraRay, out rayHit, Mathf.Infinity)) {
            pointToLook = rayHit.point;
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            if (Vector3.Distance(transform.position, pointToLook) <= placementRange) {

                if (canPlace) {
                    if (GameObject.FindGameObjectsWithTag("Pointer").Length < 1) {
                        circle = (GameObject)Instantiate(position);
                    }
                    if (GameObject.FindGameObjectsWithTag("Pointer").Length > 0) {
                        circle.transform.position = pointToLook;
                    }

                    Debug.DrawLine(pointToLook, new Vector3(pointToLook.x, pointToLook.y + 10, pointToLook.z));

                    if (Input.GetMouseButtonDown(0)) {
                        bool col = false;

                        foreach (GameObject ub in GameObject.FindGameObjectsWithTag("Tower")) {
                            if (circle.GetComponent<BoxCollider>().bounds.Intersects(ub.GetComponent<BoxCollider>().bounds)) {
                                col = true;
                            }
                        }

                        if (col == false && GameObject.FindGameObjectsWithTag("Tower").Length < maxCount) {
                            Instantiate(towers[currentTower-1], pointToLook, Quaternion.identity, GameObject.Find("Towers").transform);
                        }
                    }
                    if (Input.GetMouseButtonDown(1)) {
                        GameObject gum = null;

                        foreach (GameObject ub in GameObject.FindGameObjectsWithTag("Tower")) {
                            if (circle.GetComponent<BoxCollider>().bounds.Intersects(ub.GetComponent<BoxCollider>().bounds)) {
                                gum = ub;
                            }
                        }

                        if (gum != null) {
                            Destroy(gum);
                        }
                    }
                }
                else {
                    if (GameObject.FindGameObjectsWithTag("Pointer").Length > 0) {
                        Destroy(circle);
                    }
                }
                
            }
            else {
                if (GameObject.FindGameObjectsWithTag("Pointer").Length > 0) {
                    Destroy(circle);
                }
            }
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, placementRange);

        if (Vector3.Distance(transform.position, pointToLook) <= placementRange) {
            Gizmos.DrawWireCube(pointToLook, new Vector3(placementSize, placementSize, placementSize));
        }

    }
}
