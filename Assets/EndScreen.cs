using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    public Text enemiesKilledText;
    public Text recordKillsText;
    public Text currentTimeText;
    public Text recordTimeText;

    private void OnEnable()
    {
        enemiesKilledText.text = SessionData.minionsKilled + "";
        recordKillsText.text = SessionData.recordMinions + "";
        currentTimeText.text = SessionData.currentTime + "";
        recordTimeText.text = SessionData.recordTime + "";

    }
}
