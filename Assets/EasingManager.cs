using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasingManager : MonoBehaviour
{
    public GameObject coin;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.originalPosition = coin.transform.position;
        LeanTween.alpha(coin.GetComponent<RectTransform>(), 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void easeCoin() {
        LeanTween.alpha(coin.GetComponent<RectTransform>(), 1f, 0f);
        LeanTween.alpha(coin.GetComponent<RectTransform>(), 0f, 0.75f);

        LeanTween.moveY(coin.GetComponent<RectTransform>(), -50f, 0.75f);
        coin.transform.position = this.originalPosition;
    }
}
