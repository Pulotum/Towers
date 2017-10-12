using UnityEngine;
using System.Collections;

public class EndPointScript : MonoBehaviour {

    public float health;
    UIScript ui;
	
    void Start() {
        ui = FindObjectOfType<UIScript>();
    }

	// Update is called once per frame
	void Update () {
        ui.baseHealth.text = "Base Health: " + health;
        if (health <= 0) {
            Time.timeScale = 0f;
        }
	}

    public void DoDamage(float dam) {
        health -= dam;
        Debug.Log("Health: " + health);
    }
}
