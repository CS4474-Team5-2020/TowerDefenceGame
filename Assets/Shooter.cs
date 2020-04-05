using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    private Transform target;
    public ObjectPool bulletPool;
    public Detection detector;
    public GameObject rotatable;

    public float timePerShot;
    public float attackTimer;
    public int shotDamage;
    public float shotSpeed;
    private bool canAttack;

    private Queue<Transform> enemies = new Queue<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        // detector.onDetect += SetTarget;
        // detector.unDetect += UnsetTarget;

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if(!canAttack)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= timePerShot)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if(target == null && enemies.Count > 0){
            target = enemies.Dequeue();
        }else if(target != null && !target.GetComponent<EnemyAI>().IsAlive()){
            if(enemies.Count > 0)
                target = enemies.Dequeue();
        }else if(target != null){
            if(canAttack){
                Shoot();
                canAttack = false;
            }
        }
    }

    private void SetTarget(GameObject targetObject)
    {
        enemies.Enqueue(targetObject.transform);
    }

    private void UnsetTarget(GameObject targetObject)
    {
        target = null;
    }

    private void Shoot()
    {
        //Todo is there some fancy OO way of making this cleaner?
        GameObject projectile = bulletPool.GetGameObject();
        Debug.Log(bulletPool.name);
        if (bulletPool.name == "BulletPool") {
            projectile.GetComponent<Bullet>().Initialize(transform.position, target.GetComponent<EnemyAI>(), shotDamage, shotSpeed);
        } else if (bulletPool.name == "MissilePool") {
            projectile.GetComponent<Missile>().Initialize(transform.position, target.GetComponent<EnemyAI>(), shotDamage, shotSpeed);
        }

    }
}
