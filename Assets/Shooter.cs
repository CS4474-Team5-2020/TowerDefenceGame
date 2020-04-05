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
    public int shotDamage;
    public float shotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //For now comment these out to see player's health indicator decrease
        // detector.onDetect += SetTarget;
        // detector.unDetect += UnsetTarget;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTarget(GameObject targetObject)
    {
        if (target == null)
            StartCoroutine(Shoot(targetObject.transform));
    }

    private void UnsetTarget(GameObject targetObject)
    {
        if (target.gameObject != targetObject)
            return;

        target = null;
    }

    IEnumerator Shoot(Transform newTarget)
    {
        StartCoroutine(Aim());
        target = newTarget;
        EnemyAI enemy = target.GetComponent<EnemyAI>();
        while (target != null)
        {
            if (!enemy.IsAlive())
            {
                target = GetNextTarget();
                if (target == null)
                    break;
            }
            GameObject bullet = bulletPool.GetGameObject();
            bullet.GetComponent<Bullet>().Initialize(transform.position, target.GetComponent<EnemyAI>(), shotDamage, shotSpeed);
            yield return new WaitForSeconds(timePerShot);
        }

    }

    IEnumerator Aim()
    {
        while (target != null)
        {
            Vector3 displacement = target.position - transform.position;
            rotatable.transform.rotation = Quaternion.LookRotation(new Vector3(displacement.x, 0, displacement.z));
            yield return null;
        }
    }
    

    private Transform GetNextTarget()
    {
        return null;
    }
}
