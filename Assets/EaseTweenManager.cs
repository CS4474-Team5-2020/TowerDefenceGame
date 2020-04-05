using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EaseTweenManager : MonoBehaviour
{
    public GameObject coin;
    public Text healthLostTxtLeft;
    public Text healthLostTxtRight;
    public Text healthLostTxtTop;
    public Text healthLostTxtBottom;

    private Vector3 originalPositionCoin;

    // Start is called before the first frame update
    void Start()
    {
        //Set original position of coin
        this.originalPositionCoin = this.coin.transform.position;
        LeanTween.alpha(coin.GetComponent<RectTransform>(), 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TweenHealthLost(string zone) {
        if (zone == "top") {
            LeanTween.alphaText(healthLostTxtTop.GetComponent<RectTransform>(), 1f, 0f);
            LeanTween.alphaText(healthLostTxtTop.GetComponent<RectTransform>(), 0f, 1f);
        }
        if (zone == "bottom" ){
            LeanTween.alphaText(healthLostTxtBottom.GetComponent<RectTransform>(), 1f, 0f);
            LeanTween.alphaText(healthLostTxtBottom.GetComponent<RectTransform>(), 0f, 1f);
        }
        if (zone == "left" ){
            LeanTween.alphaText(healthLostTxtLeft.GetComponent<RectTransform>(), 1f, 0f);
            LeanTween.alphaText(healthLostTxtLeft.GetComponent<RectTransform>(), 0f, 1f);
        }
        if (zone == "right" ){
            LeanTween.alphaText(healthLostTxtRight.GetComponent<RectTransform>(), 1f, 0f);
            LeanTween.alphaText(healthLostTxtRight.GetComponent<RectTransform>(), 0f, 1f);
        }
    }

    public void TweenCoin() {
        LeanTween.alpha(coin.GetComponent<RectTransform>(), 1f, 0f);
        LeanTween.alpha(coin.GetComponent<RectTransform>(), 0f, 0.60f);

        LeanTween.moveY(coin.GetComponent<RectTransform>(), -50f, 0.60f);
        this.coin.transform.position = this.originalPositionCoin;
    }
}
