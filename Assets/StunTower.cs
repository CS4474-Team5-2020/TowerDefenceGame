using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTower : MonoBehaviour
{
    public DetectionMulti detector;
    public GameObject towerParticles;
    public GameObject debuffParticles;

    public float timePerShot;
    public float stunChance = 0.33f;
    public float stunTime = 0.4f;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        detector.onDetect += SetTargets;
        detector.unDetect += UnsetTarget;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetTargets(List<GameObject> targetObjects)
    {
        StartCoroutine(FreezeEnemies(targetObjects));
    }

    private void UnsetTarget(List<GameObject> targetObjects)
    {
        if (targetObjects.Count > 0)
            return;

        StartCoroutine(FreezeEnemies(targetObjects));
    }

    IEnumerator FreezeEnemies(List<GameObject> targetObjects)
    {
        Debug.Log("Stunning " + targetObjects.Count + " Enemies.");
        while (targetObjects.Count > 0)
        {
            //Freeze Animation
            towerParticles.GetComponent<ParticleSystem>().Play();

            foreach (var target in targetObjects) {
                EnemyAI enemy = target.GetComponent<EnemyAI>();
                if(enemy.isActiveAndEnabled)
                    if (Random.Range(0, 100) <= stunChance * 100)
                    {
                        enemy.ApplyStunEffect(debuffParticles, stunTime);
                    }
                enemy.TakeDamage(damage);   
            }
            
            yield return new WaitForSeconds(timePerShot);
        }

    }
}
