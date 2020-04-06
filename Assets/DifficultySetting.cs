using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySetting : MonoBehaviour
{

    private Color easyColor = new Color(150 / 255f, 220 / 255f, 55 / 255f);
    private Color medColor = new Color(255 / 255f, 250 / 255f, 100 / 255f);
    private Color hardColor = new Color(200 / 255f, 50 / 255f, 0 / 255f);
    public Image easyButton;
    public Image medButton;
    public Image hardButton;
    public GameObject medMinions;
    public GameObject hardMinions;

    public void Awake()
    {
        SelectDifficulty(1);
    }
    public void SelectDifficulty(int level)
    {
        if (level == 1)
        {
            easyButton.color = easyColor;
            medButton.color = Color.white;
            hardButton.color = Color.white;
            medMinions.SetActive(false);
            hardMinions.SetActive(false);
            SessionData.difficultyMultiplier = 1;
        }
        else if (level == 2)
        {
            easyButton.color = Color.white;
            medButton.color = medColor;
            hardButton.color = Color.white;
            medMinions.SetActive(true);
            hardMinions.SetActive(false);
            SessionData.difficultyMultiplier = 2;
        }
        else if (level == 3)
        {
            easyButton.color = Color.white;
            medButton.color = Color.white;
            hardButton.color = hardColor;
            medMinions.SetActive(true);
            hardMinions.SetActive(true);
            SessionData.difficultyMultiplier = 3;
        }
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
    }

}
