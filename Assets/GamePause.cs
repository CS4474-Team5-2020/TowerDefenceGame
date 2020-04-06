using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{

    public delegate void OnPause();
    public static OnPause onPause;
    public delegate void OnResume();
    public static OnResume onResume;

    public GameObject pausePanel;
    private static bool gamePaused;
    public Image buttonImage;
    public Image buttonColour;
    public Sprite playSprite;
    public Sprite pauseSprite;
    public Color pauseColor;
    public Color playColor;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
        pausePanel.SetActive(false);
    }


    public void OnButtonClick()
    {
        gamePaused = !gamePaused;
        pausePanel.SetActive(gamePaused);
        if (gamePaused)
        {
            buttonImage.sprite = playSprite;
            buttonColour.color = playColor;
            onPause?.Invoke();
        }
        else
        {
            buttonImage.sprite = pauseSprite;
            buttonColour.color = pauseColor;
            onResume?.Invoke();
        }
    }


    public static bool IsPaused()
    {
        return gamePaused;
    }
}
