using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : PausableBehaviour
{
    public GameObject finishZone;
    public int health = 50;  //Might want to add this back to inspector
    public string minionType;

    private MoneyManager money;
    private int value = 2;   //Equivalent to how much money player gets when killing this enemy - could we make this depend on different enemy types?
    private bool isBalanceIncreased = false;    //Balanced increased?

    public delegate void OnDeath(GameObject gameObject);
    public OnDeath onDeath;
    public float agentSpeed = 3.5f;

    //will move the slider on the healthbar
    public HealthBar healthBar;
    
    private HealthManager playerHealth;
    private bool isHealthDecreased = false;  //Health was already decreased? 
    private Vector3 destination;
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

        healthBar.SetMaxHealth(health);

    }

    // Update is called once per frame
    void Update()
    {
        //If enemy made it across to the finish zone, then decrease the player health
        if (this.IsEnemyAcross()){
            this.playerHealth.DecreasePlayerHealth(this.health, this.endZone);
            gameObject.SetActive(false);
            onDeath?.Invoke(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0) { health = 0; }
        if (health == 0)
        {
            healthBar.SetHealth(0);
            Die();
        }else healthBar.SetHealth(health);
    }

    private void Die()
    {
        if (minionType == "Group" && gameObject.activeSelf)
        {
            StartCoroutine(SpawnGrouplings());
        }

        StopCoroutine("FreezeEffect");
        gameObject.SetActive(false);

        //If game object is not active and if balance hasn't been increased already, then increase money balance
        if (!gameObject.activeSelf && !this.isBalanceIncreased) {
             this.isBalanceIncreased = true;
             money.IncreaseMoneyBalance(value);   //When enemy dies, increase money balance
        }
        onDeath?.Invoke(gameObject);
    }

    //Spawn 3 smaller Group minions when one dies
    private IEnumerator SpawnGrouplings()
    {
        InheritedBehaviour parentBehaviour = new InheritedBehaviour(this.transform.position + new Vector3(0.4f, 0, 0), this.destination, this.attackOrientation, this.endZone);
        GameManager.Instance.SpawnEnemy("Groupling Enemy", "",parentBehaviour);
        parentBehaviour.SpawnPoint = this.transform.position + new Vector3(0.4f, 0.4f, 0);
        GameManager.Instance.SpawnEnemy("Groupling Enemy", "", parentBehaviour);
        parentBehaviour.SpawnPoint = this.transform.position + new Vector3(0.4f, 0.4f, 0.4f);
        GameManager.Instance.SpawnEnemy("Groupling Enemy", "", parentBehaviour);
        yield return null;

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

        if (this.IsAlive() && (closeness < 0.2f && closeness > -0.2f) && !this.isHealthDecreased) {
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
        this.destination = destination;
    }

    private Vector3 GetDestination() {
        return this.GetComponent<NavMeshAgent>().destination;
    }

    protected override void OnResume()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }

    protected override void OnPause()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}

public class InheritedBehaviour
{
    public InheritedBehaviour(Vector3 spawn, Vector3 destination, string orientation, string end)
    {
        SpawnPoint = spawn;
        Destination = destination;
        Orientation = orientation;
        EndZone = end;
    }
    public Vector3 SpawnPoint { get; set; }
    public Vector3 Destination { get; set; }
    public string Orientation { get; set; }
    public string EndZone { get; set; }

}
