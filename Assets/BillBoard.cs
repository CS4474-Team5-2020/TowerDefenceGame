using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

	public Transform cam;
    void LateUpdate()
    {
    	cam = GameObject.Find("Main Camera").GetComponent<Camera>().transform;
    	transform.LookAt(transform.position + cam.forward);
    }
}
