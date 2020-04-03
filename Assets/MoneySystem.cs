using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private int moneyBalance;
    [SerializeField] private int moneyCounter = 0;
    public Text moneyText;
    public Text moneyCounterText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        this.moneyCounter += 1;

        //Set UI Text Canvas Objects with money balance and counter values
        this.moneyText.text = '$' + this.moneyBalance.ToString();
        this.moneyCounterText.text = "$+" + this.moneyCounter.ToString();
    }
}
