using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{

    public Map[] maps;
    private int selectedMap;
    public Button startButton;
    public Text buttonText;

    public float switchTimePerMap;

    public Image backgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        SelectMap(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectMap(int mapID)
    {
        if (selectedMap == mapID)
            return;

        selectedMap = mapID;
        if (maps[mapID].canSelect)
        {
            startButton.interactable = true;
            buttonText.text = "Start Game";
        }
        else
        {
            startButton.interactable = false;
            buttonText.text = "Map unavailable";
        }

        backgroundImage.sprite = maps[selectedMap].mapImage.sprite;
        backgroundImage.color = maps[selectedMap].mapImage.color;
    }


}
