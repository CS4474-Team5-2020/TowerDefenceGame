using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    public GameObject finishZone;
    [SerializeField] private int health;
    

    // Start is called before the first frame update
    void Start()
    {

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
