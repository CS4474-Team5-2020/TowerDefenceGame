using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //TODO: There is probably some better way to inherit or what not for this class.... rather then coppying bullet
    public GameObject body;
    public GameObject particles;
    public float rangeSize;

    private EnemyAI target;
    private int damage;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Mainly for the rocket but, this rotate the bullet as well...
        Vector3 displacement = target.transform.position - transform.position;
        target.transform.rotation = Quaternion.LookRotation(new Vector3(displacement.x, 0, displacement.z));

        Vector3 distanceRemaining = target.transform.position - transform.position;
        float distanceTraveled = speed * Time.deltaTime;
        if (distanceRemaining.magnitude > distanceTraveled)
        {
            transform.position += distanceRemaining.normalized * distanceTraveled;
        }
        else
        {
            Hit(target);
        }
    }


    void Hit(EnemyAI target)
    {
        //play animation
        var instantiatedObj = (GameObject)Instantiate(particles, transform.position, transform.rotation);
        Destroy(instantiatedObj, 1f); //create particle effect and remove it.

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < rangeSize)
            {
                Debug.Log(enemies.Length);
                enemy.GetComponent<EnemyAI>().TakeDamage(damage);
            }
        }

        gameObject.SetActive(false);
    }

    public void CheckTarget(GameObject other)
    {
        if (target.gameObject == other)
            Hit(other.GetComponent<EnemyAI>());
    }

    public void Initialize(Vector3 position, EnemyAI target, int damage, float speed)
    {
        //Mainly for the rocket but, this rotate the bullet as well...
        Vector3 displacement = position - target.transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(displacement.x, 0, displacement.z));

        transform.position = position;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
    }
}
