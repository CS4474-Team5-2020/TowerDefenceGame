using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
    
    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    public Dictionary<Point,Grid> Tiles { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, Grid>();


        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                PlaceTile(x, y, worldStart);
            }
        }
    }

    private void PlaceTile(int x, int y, Vector3 worldStart)
    {
        

        Grid newTile = Instantiate(tile).GetComponent<Grid>();
        newTile.setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));

        Tiles.Add(new Point(x, y), newTile);

    }
}
