using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private int moneyBalance = 0;
    private int moneyValue = 0;  //Amount of money gained per kill
    public Text moneyBalanceText;
    public Text moneyValueText;


    // Start is called before the first frame update
    void Start()
    {
        //Load in the saved money balance just before the current game session begins
        this.moneyBalance = PlayerPrefs.GetInt("moneyBalance");
        this.moneyBalanceText.text = this.moneyBalance.ToString();
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

    public int getMoneyValue() {
        return this.moneyValue;
    }

    public void setMoneyBalance(int value) {
        //Set money balance and counter values
        this.moneyBalance += value;
        this.moneyValue = value;

        //Set UI Text Canvas Objects with money balance and counter values
        this.moneyBalanceText.text = this.moneyBalance.ToString();
        this.moneyValueText.text = this.moneyValue.ToString();

        //Save the current balance into Playerprefs so it can be saved for later game sessions
        this.saveMoneyBalance();
    }
}
