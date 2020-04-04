﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    public GameObject finishZone;
    [SerializeField] private int health;

    private MoneyManager money;
    private int value = 1;   //Equivalent to how much money player gets when killing this enemy - could we make this depend on different enemy types?
    private bool isBalanceIncreased = false;    //Balanced increased?

    // Start is called before the first frame update
    void Start()
    {
        //Get instance associated with MoneyManager gameObject
        try {
            this.money = GameObject.FindObjectOfType<MoneyManager>();
        }
        catch(Exception e) {
            Debug.LogException(e);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);

        //If game object is not active and if balance hasn't been increased already, then increase money balance
        if (!gameObject.activeSelf && !this.isBalanceIncreased) {
             this.isBalanceIncreased = true;
             money.SetMoneyBalance(value);   //When enemy dies, increase money balance
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }
    public void SetDestination(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }
}
