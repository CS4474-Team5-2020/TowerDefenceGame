using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PausableBehaviour
{

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
        if (GamePause.IsPaused())
            return;

        if (!target == null)
            gameObject.SetActive(false);

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
        gameObject.SetActive(false);
        target.TakeDamage(damage);
    }

    public void CheckTarget(GameObject other)
    {
        if (target.gameObject == other)
            Hit(other.GetComponent<EnemyAI>());
    }

    public void Initialize(Vector3 position, EnemyAI target, int damage, float speed)
    {
        transform.position = position;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
    }

}
