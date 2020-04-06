using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayUI : MonoBehaviour
{
    public string myString;
    public Text myText;
    public GameObject text;
    public float fadeTime;
    public Image fill;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        myText = text.GetComponent<Text>();
        myText.color = Color.clear;
        fill.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMouseOverUIWithIgnores())
        {
            myText.text = myString;
            myText.color = Color.black;
            fill.color = Color.white;
        }
        else
        {
            myText.color = Color.clear;
            fill.color = Color.clear;
        }

    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsMouseOverUIWithIgnores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for(int i = 0; i < raycastResultList.Count; i++)
        {
            if(raycastResultList[i].gameObject.name != gameObject.name)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultList.Count > 0;
    }
}
