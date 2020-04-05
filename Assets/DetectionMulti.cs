using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DetectionMulti : MonoBehaviour
{

    [SerializeField] private string target;
    public delegate void OnDetect(List<GameObject> targets);
    public OnDetect onDetect;
    public delegate void UnDetect(List<GameObject> targets);
    public UnDetect unDetect;

    List<GameObject> currentCollisions = new List<GameObject>();


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

        currentCollisions.Add(other.gameObject);

        onDetect?.Invoke(currentCollisions);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != target)
            return;

        currentCollisions.Remove(other.gameObject);

        unDetect?.Invoke(currentCollisions);

    }


}
