using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EaseTweenManager : MonoBehaviour
{
    public GameObject coin;
    public GameObject coinUsed;

    public Text healthLostTxtLeft;
    public Text healthLostTxtRight;
    public Text healthLostTxtTop;
    public Text healthLostTxtBottom;

    private Vector3 originalScaleCoin, originalUsedCoinPos;

    // Start is called before the first frame update
    void Start()
    {       
        //Set original scale of coin
        this.originalScaleCoin = coin.GetComponent<RectTransform>().localScale;
        LeanTween.alpha(coin.GetComponent<RectTransform>(), 0f, 0f);

        //Set original position of used red coin
        this.originalUsedCoinPos = this.coinUsed.transform.position;
        LeanTween.alpha(coinUsed.GetComponent<RectTransform>(), 0f, 0f);
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
        
        StartCoroutine(SetOriginalCoinScale());
    }

    IEnumerator SetOriginalCoinScale() {
        coin.GetComponent<RectTransform>().localScale = this.originalScaleCoin;
        yield return null;
    }

    public void TweenUsedCoin() {
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alpha(coinUsed.GetComponent<RectTransform>(), 1f, 0f));
        LeanTween.moveX(coinUsed.GetComponent<RectTransform>(), 225f, 0.75f);
        LeanTween.alpha(coinUsed.GetComponent<RectTransform>(), 0f, 0.75f);

        StartCoroutine(SetOriginalUsedCoinPos());
    }

    IEnumerator SetOriginalUsedCoinPos() {
        this.coinUsed.transform.position = this.originalUsedCoinPos;
        yield return null;
    }

}
