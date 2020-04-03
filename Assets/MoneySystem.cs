using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    private int moneyBalance;
    private int moneyCounter = 0;  //Amount of money gained in current game session
    public Text moneyText;
    public Text moneyCounterText;

    // Start is called before the first frame update
    void Start()
    {
        //Load in the saved money balance just before the current game session begins
        this.moneyBalance = PlayerPrefs.GetInt("moneyBalance");
        this.moneyText.text = '$' + this.moneyBalance.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void saveMoneyBalance() {
        PlayerPrefs.SetInt("moneyBalance", this.moneyBalance);
        PlayerPrefs.Save();
    }

    public int getMoneyBalance() {
        return this.moneyBalance;
    }

    public int getMoneyCounter() {
        return this.moneyCounter;
    }

    public void setMoneyBalance(int value) {
        //Set money balance and counter values
        this.moneyBalance += value;
        this.moneyCounter += value;

        //Set UI Text Canvas Objects with money balance and counter values
        this.moneyText.text = '$' + this.moneyBalance.ToString();
        this.moneyCounterText.text = "$+" + this.moneyCounter.ToString();

        //Save the current balance into Playerprefs so it can be saved for later game sessions
        this.saveMoneyBalance();
    }
}
