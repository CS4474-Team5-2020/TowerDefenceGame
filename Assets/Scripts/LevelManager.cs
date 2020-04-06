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
    public Dictionary<Point, GridScript> Tiles { get; set; }

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
        Tiles = new Dictionary<Point, GridScript>();

        Vector3 worldStart = new Vector3((float)-5.76, (float)0.5, (float)5.75);
        for (int z = 0; z < 9; z++)
        {
            for (int x = 0; x < 9; x++)
            {
                PlaceTile(x,(float)0.5, z, worldStart);
            }
        }
    }

    private void PlaceTile(int x, float y, int z, Vector3 worldStart)
    {
        GridScript newTile = Instantiate(tile).GetComponent<GridScript>();
        newTile.setup(new Point(x, y, z), new Vector3(worldStart.x + (TileSize * x), (float)0.5, worldStart.z - (TileSize * z)));

        Tiles.Add(new Point(x, y, z), newTile);
        

    }
}