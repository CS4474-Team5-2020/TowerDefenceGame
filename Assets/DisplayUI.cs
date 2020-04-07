using System;
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
    public TowerBtn towerbtn;

    private MoneyManager money;

    // Start is called before the first frame update
    void Start()
    {
        try {
            this.money = GameObject.FindObjectOfType<MoneyManager>();
            // Debug.Log(this.money.GetMoneyBalance().ToString());
        }
        catch(Exception e){
            Debug.Log("Error: " + e.ToString());
        }

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
            fill.color = new Color(255f, 255f, 255f, 0.6f);
        }
        else
        {
            myText.color = Color.clear;
            fill.color = Color.clear;
        }

        //If don't have enough money to purchase tower, grey out tower button
        var tempColor = this.gameObject.GetComponent<Button>().colors;
        if (this.money.GetMoneyBalance() < towerbtn.Price) {
            tempColor.normalColor = Color.grey;
            tempColor.pressedColor = Color.grey;
            tempColor.highlightedColor = Color.grey;
            tempColor.selectedColor = Color.grey;
        }
        else {
            tempColor.normalColor = Color.white; 
            tempColor.pressedColor = new Color(0.78f, 0.78f, 0.78f, 1f);
            tempColor.highlightedColor = new Color(0.88f, 0.88f, 0.88f, 1f);
            tempColor.selectedColor = Color.white;
        }
        this.gameObject.GetComponent<Button>().colors = tempColor;
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
