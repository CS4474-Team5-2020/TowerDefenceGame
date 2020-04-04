using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    public string mapName;
    public Image mapImage;
    public Text mapNameText;
    public bool canSelect;

    // Start is called before the first frame update
    void Start()
    {
        mapNameText.text = mapName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Debug.Log("Test");
    }
}
