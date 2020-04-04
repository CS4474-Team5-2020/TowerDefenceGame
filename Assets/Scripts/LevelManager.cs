using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
<<<<<<< Updated upstream
    
=======

>>>>>>> Stashed changes
    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
<<<<<<< Updated upstream
    public Dictionary<Point,Grid> Tiles { get; set; }
=======
    public Dictionary<Point, Grid> Tiles { get; set; }
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
<<<<<<< Updated upstream
       
=======

>>>>>>> Stashed changes

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
    }

    
=======

    }


>>>>>>> Stashed changes

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, Grid>();


        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < 16; y++)
        {
<<<<<<< Updated upstream
            for (int x = 0; x < 11; x++)
=======
            for (int x = 0; x < 15; x++)
>>>>>>> Stashed changes
            {
                PlaceTile(x, y, worldStart);
            }
        }
    }

    private void PlaceTile(int x, int y, Vector3 worldStart)
    {
<<<<<<< Updated upstream
        
=======

>>>>>>> Stashed changes

        Grid newTile = Instantiate(tile).GetComponent<Grid>();
        newTile.setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));

        Tiles.Add(new Point(x, y), newTile);

    }
}
