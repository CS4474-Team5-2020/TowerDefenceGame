using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GamePause : MonoBehaviour
{

    public delegate void OnPause();
    public static OnPause onPause;
    public delegate void OnResume();
    public static OnResume onResume;

    private static bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }


    public void OnButtonClick()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            onPause?.Invoke();
        }
        else
        {
            onResume?.Invoke();
        }
    }


    public static bool IsPaused()
    {
        return gamePaused;
    }
}
