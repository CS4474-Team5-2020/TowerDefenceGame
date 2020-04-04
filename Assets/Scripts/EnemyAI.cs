using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    public GameObject finishZone;
    [SerializeField] private int health;
    public delegate void OnDeath(GameObject gameObject);
    public OnDeath onDeath;
    public float agentSpeed = 3.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<NavMeshAgent>().speed = agentSpeed;
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
        StopCoroutine("FreezeEffect");
        gameObject.SetActive(false);
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
    public void SetDestination(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }
}
