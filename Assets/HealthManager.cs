using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int maxHealth = 200;
    private int playerHealth;
    private int playerHealthLost;

    public Text playerHealthText;
    public Text healthLostTextLeft;
    public Text healthLostTextRight;
    public Text healthLostTextTop;
    public Text healthLostTextBottom;
    public Image heartFilled; 

    private float oldFill, currentFill;  

    private EaseTweenManager tween;

    // Start is called before the first frame update
    void Start()
    {
        //Get instance associated with EaseTweenManager gameObject
        try {
            this.tween = GameObject.FindObjectOfType<EaseTweenManager>();
        }
        catch(Exception e) {
            Debug.LogException(e);
        } 

        this.playerHealth = this.maxHealth;
        this.oldFill = this.maxHealth;
        this.playerHealthText.text = this.playerHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Change the fill of lives heart indicator
    private void SetHeartFill() {
        this.currentFill = (float)this.playerHealth/(float)this.maxHealth;
        this.heartFilled.fillAmount = Mathf.Lerp(this.oldFill, this.currentFill, 10f);
        this.oldFill = this.currentFill;
    }

    //Decrease player health by current enemy health - maybe want this to decrease by enemy value instead? 
    //Was thinking it would be more fair to decrease by enemy health so that it would better reflect a player's effort to kill an enemy
    public void DecreasePlayerHealth(int enemyHealth, string zone){
        this.playerHealth -= enemyHealth;
        this.playerHealthLost = enemyHealth;

        if (zone != "") {
            if (zone == "top") {
                this.healthLostTextTop.text = "-" + this.playerHealthLost.ToString();
                this.tween.TweenHealthLost(zone);
            }
            if (zone == "bottom" ){
                this.healthLostTextBottom.text = "-" + this.playerHealthLost.ToString();
                this.tween.TweenHealthLost(zone);
            }
            if (zone == "left" ){
                this.healthLostTextLeft.text = "-" + this.playerHealthLost.ToString();
                this.tween.TweenHealthLost(zone);
            } 
            if (zone == "right" ){
                this.healthLostTextRight.text = "-" + this.playerHealthLost.ToString();
                this.tween.TweenHealthLost(zone);
            }
        }
    
        this.SetHeartFill();
        this.playerHealthText.text = this.playerHealth.ToString();
    }
}
