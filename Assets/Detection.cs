using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Detection : MonoBehaviour
{

    [SerializeField] private string target;
    public delegate void OnDetect(GameObject target);
    public OnDetect onDetect;
    public delegate void UnDetect(GameObject target);
    public UnDetect unDetect;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != target)
            return;

        onDetect?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != target)
            return;

        unDetect?.Invoke(other.gameObject);

    }


}
