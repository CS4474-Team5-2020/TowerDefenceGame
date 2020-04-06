using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableBehaviour : MonoBehaviour
{
    protected bool paused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GamePause.onPause += OnPause;
        GamePause.onResume += OnResume;

        paused = GamePause.IsPaused();
        if (paused)
            OnPause();
        else OnResume();
    }
    private void OnDisable()
    {
        GamePause.onPause -= OnPause;
        GamePause.onResume -= OnResume;
    }

    protected virtual void OnResume()
    {
        paused = false;
    }
    protected virtual void OnPause()
    {
        paused = true;
    }
}
