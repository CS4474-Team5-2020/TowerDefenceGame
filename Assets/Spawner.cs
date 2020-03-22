using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject finishZone;
    public ObjectPool pool;
    public float timePerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject enemyObject = pool.GetGameObject();
            enemyObject.transform.position = transform.position;
            enemyObject.GetComponent<EnemyAI>().SetDestination(finishZone.transform.position);
            yield return new WaitForSeconds(timePerSpawn);
        }
    }
}
