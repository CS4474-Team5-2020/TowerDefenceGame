using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{

    private Transform target;
    public GameObject rotatable;
    public Detection detector;

    private Queue<Transform> enemies = new Queue<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        detector.onDetect += SetTarget;
        detector.unDetect += UnsetTarget;

    }

    // Update is called once per frame
    void Update()
    {

        if (target == null && enemies.Count > 0){
            target = enemies.Dequeue();
        }else if(target != null && !target.GetComponent<EnemyAI>().IsAlive()){
            if(enemies.Count > 0)
                target = enemies.Dequeue();
        }else if(target == null) return;

        Vector3 displacement = target.position - transform.position;
        rotatable.transform.rotation = Quaternion.LookRotation(new Vector3(displacement.x, 0, displacement.z));


    }

    private void SetTarget(GameObject targetObject)
    {
        enemies.Enqueue(targetObject.transform);
    }

    private void UnsetTarget(GameObject targetObject)
    {
        target = null;
    }


 }
