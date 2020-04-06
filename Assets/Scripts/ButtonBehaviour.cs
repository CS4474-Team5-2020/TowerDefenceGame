using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{


    //Otherwise you can do it publicly.  
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public void MouseEnter()
    {
        Debug.Log("Enter Cursor");
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void MouseExit()
    {
        Debug.Log("Exit Cursor");
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

}