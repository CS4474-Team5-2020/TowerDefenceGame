using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private int moneyBalance = 0;
    private int moneyValue = 0;  //Amount of money gained per kill
    private int moneyHighScore = 0; 
    private int moneyCollected = 0;

    public Text moneyBalanceText;
    public Text moneyValueText;

    public Text moneyHighScoreLabel;
    public Text moneyHighScoreText;
    public Text moneyCollectedText;

    private EaseTweenManager tween;

    //Tooltips
    public GameObject tooltipBalance;
    public GameObject tooltipHighScore;

    // Start is called before the first frame update
    void Start()
    {
        try {
            //Set tooltips to inactive to start
            this.tooltipBalance.SetActive(false);
            if (this.tooltipHighScore != null) {
                this.tooltipHighScore.SetActive(false);
            }

            //If the player is just starting out with no money accumulated, then provide $20 to start off
            if (this.moneyBalance < 20) {
                PlayerPrefs.SetInt("moneyBalance", 20);
                PlayerPrefs.Save();
            }
            //Load in the saved money balance just before the current game session begins
            this.moneyBalance = PlayerPrefs.GetInt("moneyBalance");
            this.moneyBalanceText.text = this.moneyBalance.ToString();

            //Load in saved high score of money collected just before the current game session begins
            this.moneyHighScore = PlayerPrefs.GetInt("moneyHighScore");
            if (this.moneyHighScoreText != null) {
                 this.moneyHighScoreText.text = this.moneyHighScore.ToString();
            }

            //Display amount collected just before current game begins
            if (this.moneyCollectedText != null) {
                this.moneyCollectedText.text = this.moneyCollected.ToString();
            }
        }
        catch(Exception e) {
            Debug.LogException(e);
        }


        //Get instance associated with EaseTweenManager gameObject
        try {
            this.tween = GameObject.FindObjectOfType<EaseTweenManager>();
        }
        catch(Exception e) {
            
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SaveMoneyBalance() {
        PlayerPrefs.SetInt("moneyBalance", this.moneyBalance);
        PlayerPrefs.Save();
    }

    private void SaveNewHighScore() {
        PlayerPrefs.SetInt("moneyHighScore", this.moneyHighScore);
        PlayerPrefs.Save();
    }

    public int GetMoneyBalance() {
        return this.moneyBalance;
    }

    public int GetMoneyValue() {
        return this.moneyValue;
    }

    public int getHighScore() {
        return this.moneyHighScore;
    }

    public void IncreaseMoneyBalance(int value) {
        //Set money balance and counter values
        this.moneyBalance += value;
        this.moneyValue = value;
        this.moneyCollected += value;

        //Apply tween to coin
        this.tween.TweenCoin();

        //Set UI Text Canvas Objects with money balance and counter values
        this.moneyBalanceText.text = this.moneyBalance.ToString();
        this.moneyValueText.text = this.moneyValue.ToString();
        this.moneyCollectedText.text = this.moneyCollected.ToString();

        //Save the current balance into Playerprefs so it can be saved for later game sessions
        this.SaveMoneyBalance();

        //If money collected in current round is greater than high score, then set new high score
        if (this.moneyCollected > this.moneyHighScore) {
            this.moneyHighScore = this.moneyCollected;
            //Set text and colour
            this.moneyHighScoreText.text = this.moneyHighScore.ToString();
            moneyHighScoreLabel.GetComponent<Text>().color = new Color(0.316f, 0.59f, 0.55f);
            moneyHighScoreText.GetComponent<Text>().color = new Color(0.316f, 0.59f, 0.55f);
            //Save high score in prefs
            this.SaveNewHighScore();
        }
    }

    public void DecreaseMoneyBalance(int cost) {
        this.moneyBalance -= cost;
        this.moneyValue = cost;
        this.moneyBalanceText.text = this.moneyBalance.ToString();
        this.moneyValueText.text = this.moneyValue.ToString();
        this.SaveMoneyBalance();
    }
}
