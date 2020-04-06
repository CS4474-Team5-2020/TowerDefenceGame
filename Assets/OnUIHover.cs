using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnUIHover : MonoBehaviour
{
    public GameObject balance;

    public void MouseOver() {
        balance.SetActive(true);
    }

    public void MouseOut() {
        balance.SetActive(false);
    }
}
