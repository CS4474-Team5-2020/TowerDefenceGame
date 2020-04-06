using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnUIHover : MonoBehaviour
{
    public GameObject balanceTooltip;
    public GameObject highScoreTooltip;

    public void MouseOverBalance() {
        balanceTooltip.SetActive(true);
    }

    public void MouseOutBalance() {
        balanceTooltip.SetActive(false);
    }

    public void MouseOverHighScore() {
        if (highScoreTooltip != null) {
            highScoreTooltip.SetActive(true);
        }
    }

    public void MouseOutHighScore() {
        if (highScoreTooltip != null) {
            highScoreTooltip.SetActive(false);
        }
    }
}
