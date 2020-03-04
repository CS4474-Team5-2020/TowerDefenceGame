using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void GoToMapSelect()
    {
        Debug.Log("Opening Map Selection.");
        //TODO
    }

    public void Quit()
    {
        Debug.Log("Quiting Game.");
        Application.Quit();
    }
}
