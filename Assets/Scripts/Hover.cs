using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{

    private SpriteRenderer spriterenderer;
    // Start is called before the first frame update
    void Start()
    {
        this.spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, (float)0.5, transform.position.z);
    }

    public void Activate(Sprite sprite)
    {
        this.spriterenderer.sprite = sprite;
        spriterenderer.enabled = true;
    }

    public void Deactivate()
    {
        spriterenderer.enabled = false;
    }
}
