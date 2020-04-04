using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTower : MonoBehaviour
{
    public DetectionMulti detector;
    public GameObject particles;

    public float timePerShot;

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
        Debug.Log("Freezing " + targetObjects.Count + " Enemies.");
        while (targetObjects.Count > 0)
        {
            //Freeze Animation
            particles.GetComponent<ParticleSystem>().Play();

            foreach (var target in targetObjects) {
                EnemyAI enemy = target.GetComponent<EnemyAI>();
                if(enemy.isActiveAndEnabled)
                    enemy.ApplyFreezeEffect();
            }
            
            yield return new WaitForSeconds(timePerShot);
        }

    }

    private Transform GetNextTarget()
    {
        return null;
    }
}
