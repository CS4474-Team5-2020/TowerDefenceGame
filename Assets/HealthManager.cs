using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int playerHealth = 90;
    public Text playerHealthText;

    // Start is called before the first frame update
    void Start()
    {
        this.playerHealthText.text = this.playerHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Decrease player health by current enemy health - maybe want this to decrease by enemy value instead? 
    //Was thinking it would be more fair to be by health so that would better reflect a player's effort to kill an enemy; less enemy health means less penalty for player
    public void DecreasePlayerHealth(int enemyHealth){
        playerHealth -= enemyHealth;
        this.playerHealthText.text = this.playerHealth.ToString();
    }
}
