using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{

    private Transform target;
    public GameObject rotatable;
    public Detection detector;

    // Start is called before the first frame update
    void Start()
    {
        detector.onDetect += SetTarget;
        detector.unDetect += UnsetTarget;

    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
            return;

        Vector3 displacement = target.position - transform.position;
        rotatable.transform.rotation = Quaternion.LookRotation(new Vector3(displacement.x, 0, displacement.z));


    }

    private void SetTarget(GameObject targetObject)
    {
        target = targetObject.transform;
    }

    private void UnsetTarget(GameObject targetObject)
    {
        if (target.gameObject != targetObject)
            return;

        target = null;
    }


 }
