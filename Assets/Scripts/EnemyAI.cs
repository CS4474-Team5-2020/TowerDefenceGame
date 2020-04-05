using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    public GameObject finishZone;
    private int health = 50;  //Might want to add this back to inspector

    private MoneyManager money;
    private int value = 2;   //Equivalent to how much money player gets when killing this enemy - could we make this depend on different enemy types?
    private bool isBalanceIncreased = false;    //Balanced increased?

    public delegate void OnDeath(GameObject gameObject);
    public OnDeath onDeath;
    public float agentSpeed = 3.5f;
    
    private HealthManager playerHealth;
    private bool isHealthDecreased = false;  //Health was already decreased? 

    private string attackOrientation;
    private string endZone;

    // Start is called before the first frame update
    void Start()
    {
        //Get instance associated with MoneyManager gameObject
        try {
            this.money = GameObject.FindObjectOfType<MoneyManager>();
            this.playerHealth = GameObject.FindObjectOfType<HealthManager>();
        }
        catch(Exception e) {
            Debug.LogException(e);
        } 

        this.GetComponent<NavMeshAgent>().speed = agentSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //If enemy made it across to the finish zone, then decrease the player health
        if (this.IsEnemyAcross()){
            this.playerHealth.DecreasePlayerHealth(this.health, this.endZone);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        StopCoroutine("FreezeEffect");
        gameObject.SetActive(false);

        //If game object is not active and if balance hasn't been increased already, then increase money balance
        if (!gameObject.activeSelf && !this.isBalanceIncreased) {
             this.isBalanceIncreased = true;
             money.SetMoneyBalance(value);   //When enemy dies, increase money balance
        }
        onDeath?.Invoke(gameObject);
    }

    public void ApplyFreezeEffect() {
        StartCoroutine("FreezeEffect",0.5f);
    }

    IEnumerator FreezeEffect(float freezeTime)
    {
        float duration = freezeTime;
        float totalTime = 0;
        while (totalTime <= duration)
        {
            if(this != null)
                this.GetComponent<NavMeshAgent>().speed = 1f;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* choose how to quantize this */
                                          /* convert integer to string and assign to text */
            yield return null;
        }
        this.GetComponent<NavMeshAgent>().speed = agentSpeed;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    //Check if enemy made it to the destination
    private bool IsEnemyAcross() {
        //If enemy comes within certain distance of finish zone, it got across
        float closeness;

        if (this.attackOrientation == "horizontal") {
            closeness = this.transform.position.x - this.GetDestination().x;
        }
        else if (this.attackOrientation == "vertical") {
             closeness = this.transform.position.z - this.GetDestination().z;
        }
        else {
            closeness = -100f; 
        }

        if (this.IsAlive() && (closeness < 0.5f && closeness > -0.5f) && !this.isHealthDecreased) {
            this.isHealthDecreased = true;
            return true;
        }
        return false;
    }

    public void SetAttackOrientation(string orientation) {
        this.attackOrientation = orientation;
    }

    public void SetEndZone(string endZone) {
        this.endZone = endZone;
    }

    public void SetDestination(Vector3 destination) {
        this.GetComponent<NavMeshAgent>().destination = destination;
    }

    private Vector3 GetDestination() {
        return this.GetComponent<NavMeshAgent>().destination;
    }
}
