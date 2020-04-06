using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;


public class GridScript : MonoBehaviour
{

    public Point GridPosition { get; set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {





    }

    public void setup(Point gridPos, Vector3 worldPos)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;

    }
    //!EventSystem.current.IsPointerOverGameObject() &&
    private void OnMouseOver()
    {
        if ( GameManager.Instance.ClickedBtn != null)
        {
           if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }

        }
    }

    private void PlaceTower()
    {

        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Z;

        Hover.Instance.Deactivate();

        GameManager.Instance.BuyTower();


    }
}
