using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    [Header("Text Objects")]
    public Canvas canvas;
    public Text baseHealth;
    public Text buildMode;
    public Text weaponChoice;
    public Text towerChoice;
    public Text towerCount;
    public Text maxTower;
    public Text nextSpawn;
    public Text enemyCount;
    public Text maxEnemy;
    
    void Update() {
        enemyCount.text = "Enemy Count: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        towerCount.text = "Tower Count: " + GameObject.FindGameObjectsWithTag("Tower").Length;
        
    }

}
