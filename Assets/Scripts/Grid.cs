using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using UnityEngine.EventSystems;
>>>>>>> Stashed changes

public class Grid : MonoBehaviour
{

    public Point GridPosition { get; set; }
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        
=======

>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======

>>>>>>> Stashed changes
    }

    public void setup(Point gridPos, Vector3 worldPos)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;

    }

    private void OnMouseOver()
    {
<<<<<<< Updated upstream
        if(Input.GetMouseButtonDown(0))
        {
            PlaceTower();
=======
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
                
            }
>>>>>>> Stashed changes
        }
    }

    private void PlaceTower()
<<<<<<< Updated upstream
    {
        
=======
    {       
            
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        GameManager.Instance.BuyTower();

>>>>>>> Stashed changes
    }
}
