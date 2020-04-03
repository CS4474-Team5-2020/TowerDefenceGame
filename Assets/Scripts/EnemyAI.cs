using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    public GameObject finishZone;

    private MoneySystem money;
    [SerializeField] private int health;
    [SerializeField] private int value;   //How much money player gets when killing this enemy

    // Start is called before the first frame update
    void Start()
    {
        try {
            this.money = GameObject.FindObjectOfType<MoneySystem>();
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
        money.setMoneyBalance(value);
        gameObject.SetActive(false);
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
