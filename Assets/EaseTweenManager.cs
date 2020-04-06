﻿using System;
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

    private Vector3 originalPositionCoin, originalScaleCoin;

    // Start is called before the first frame update
    void Start()
    {
        //Set original position of coin
        this.originalPositionCoin = this.coin.transform.position;
        
        //Get original scale of coin
        this.originalScaleCoin = coin.GetComponent<RectTransform>().localScale;
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
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alpha(coin.GetComponent<RectTransform>(), 1f, 0f));
        seq.append(LeanTween.scale(coin.GetComponent<RectTransform>(), coin.GetComponent<RectTransform>().localScale * 1.15f, 0.15f));
        seq.append(LeanTween.scale(coin.GetComponent<RectTransform>(), coin.GetComponent<RectTransform>().localScale / 1.05f, 0.25f));
        seq.append(LeanTween.alpha(coin.GetComponent<RectTransform>(), 0f, 0f));
        
        StartCoroutine(SetOriginalCoinPos());
    }

    IEnumerator SetOriginalCoinPos() {
        coin.GetComponent<RectTransform>().localScale = this.originalScaleCoin;
        this.coin.transform.position = this.originalPositionCoin;
        yield return null;
    }
}
