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


    private MoneyManager money;
    private int value = 2;   //Equivalent to how much money player gets when killing this enemy - could we make this depend on different enemy types?
    private bool isBalanceIncreased = false;    //Balanced increased?

    public delegate void OnDeath(GameObject gameObject);
    public OnDeath onDeath;
    public float agentSpeed = 3.5f;

    //will move the slider on the healthbar
    public HealthBar healthBar;
    

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

        this.GetComponent<NavMeshAgent>().speed = agentSpeed;

        healthBar.SetMaxHealth(health);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            healthBar.SetHealth(0);
            Die();
        }else healthBar.SetHealth(health);
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

    public void ApplyStunEffect(GameObject particalSystemObj, float stunTime)
    {
        EmitMinionDebuff(particalSystemObj); 

        StartCoroutine("StunEffect", stunTime);
    }

    IEnumerator StunEffect(float stunTime)
    {
        float duration = stunTime;
        float totalTime = 0;
        while (totalTime <= duration)
        {
            if (this != null)
                this.GetComponent<NavMeshAgent>().speed = 0f;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* choose how to quantize this */
                                          /* convert integer to string and assign to text */
            yield return null;
        }
        this.GetComponent<NavMeshAgent>().speed = agentSpeed;
    }

    public void EmitMinionDebuff(GameObject particalSystem)
    {
        var instantiatedObj = (GameObject)Instantiate(particalSystem, 
                                                        transform.position + new Vector3(0,0.8f,0), 
                                                        transform.rotation);
        Destroy(instantiatedObj, 1f); //create particle effect and remove it.
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
