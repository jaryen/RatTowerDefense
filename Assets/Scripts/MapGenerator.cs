using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField] private int mapWidth = 0;
    [SerializeField] private int mapHeight = 0;

    public static List<GameObject> mapTiles = new List<GameObject>();
    public static List<GameObject> pathTiles = new List<GameObject>();

    public static GameObject startTile;
    public static GameObject endTile;

    private bool reachedX = false;
    private bool reachedY = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;

    public Color pathColor;
    public Color startTileColor;
    public Color endTileColor;

    private void Start()
    {
        mapTiles.Clear();
        pathTiles.Clear();
        GenerateMap();
    }

    private List<GameObject> GetTopEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        for (int i = mapWidth * (mapHeight-1); i < mapWidth * mapHeight; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }
        return edgeTiles;
    }

    private List<GameObject> GetBottomEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        for (int i = 0; i < mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }
        return edgeTiles;
    }

    private void MoveLeft()
    {
        // Add the current tile into the path tiles list
        pathTiles.Add(currentTile);
        // Get the index of the current tile
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        // Set current tile equal to the next tile
        currentTile = mapTiles[nextIndex];
    }

    private void MoveRight()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex+1;
        currentTile = mapTiles[nextIndex];
    }

    private void MoveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }

    private void GenerateMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(mapTile);
                mapTiles.Add(newTile);

                newTile.transform.position = new Vector2(x, y);
            }
        }

        // Get top and bottom edge tiles
        List<GameObject> topEdgeTiles = GetTopEdgeTiles();
        List<GameObject> bottomEdgeTiles = GetBottomEdgeTiles();

        // Get a random start and end tile (for path)
        int rand1 = Random.Range(0, mapWidth);
        int rand2 = Random.Range(0, mapWidth);
        startTile = topEdgeTiles[rand1];
        endTile = bottomEdgeTiles[rand2];
     
        // Start path generation
        currentTile = startTile;

        // Move down by a random amount in the 
        // beginning
        int randNum = Random.Range(1, mapHeight/2);
        for (int i = 0; i < randNum; i++)
        {
            MoveDown();
        }
        
        int loopCount = 0;
        while (!reachedX)
        {
            loopCount++;
            if (loopCount > 100)
            {
                Debug.Log("Loop ran too long! Broke out of it!");
                break;
            }
            if (currentTile.transform.position.x > endTile.transform.position.x)
            {
                MoveLeft();
            }
            else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                MoveRight();
            }
            else
            {
                reachedX = true;
            }
        }

        while (!reachedY)
        {
            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
                MoveDown();
            }
            else
            {
                reachedY = true;
            }
        }
        pathTiles.Add(endTile);

        foreach(GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = pathColor;
        }

        // Set start and end tile colors
        startTile.GetComponent<SpriteRenderer>().color = startTileColor;
        endTile.GetComponent<SpriteRenderer>().color = endTileColor;
    }
}
