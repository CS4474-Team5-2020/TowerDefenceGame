using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    public GameObject finishZone;
    [SerializeField] private int health;
    
    private HealthManager playerHealth;
    private bool isHealthDecreased = false;     //Health was already decreased? 

    // Start is called before the first frame update
    void Start()
    {
        //Get instance of HealthManager gameObject
        try {
            this.playerHealth = GameObject.FindObjectOfType<HealthManager>();
        }
        catch(Exception e) {
            Debug.LogException(e);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        //If enemy made it across to the finish zone, then decrease the player health
        if (this.IsEnemyAcross()){
            this.playerHealth.DecreasePlayerHealth(this.health);
        }
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
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    //Check if enemy made it to the destination
    private bool IsEnemyAcross() {
        //If enemy comes within certain distance of finish zone, it got across
        float closeness = gameObject.transform.position.z - getDestination().z; 
        if (this.IsAlive() && closeness < 0.5f && !this.isHealthDecreased) {
            this.isHealthDecreased = true;
            return true;
        }
        return false;
    }

    public void SetDestination(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }

    private Vector3 getDestination() {
        return GetComponent<NavMeshAgent>().destination;
    }
}
