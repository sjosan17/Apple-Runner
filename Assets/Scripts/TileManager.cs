using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour {
    // Access the Prefabs to creating a array of top and left tiles to be used later.
    public GameObject[] tilePrefabs;
    // Sets the tile(left/top) so that the game can set the next tile on it.
    public GameObject currentTile;

    // Used for recycle the tiles after the player has gone over them.
    private Stack<GameObject> leftTiles = new Stack<GameObject>();
    public Stack<GameObject> LeftTiles
    {
        get
        {
            return leftTiles;
        }

        set
        {
            leftTiles = value;
        }
    }

    // Used for recycle the tiles after the player has gone over them.
    private Stack<GameObject> topTiles = new Stack<GameObject>();
    public Stack<GameObject> TopTiles
    {
        get
        {
            return topTiles;
        }

        set
        {
            topTiles = value;
        }
    }
    // Tilmanager being uniqe, therefor static.
    private static TileManager instance;
    public static TileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TileManager>();
            }
            return instance;
        }
    }

    // Use this for initialization
    // Creates the starting tiles, so that the player "feels" it is a infinty of tiles.
    void Start () {

        CreateTiles(100);

        for (int i = 0; i < 50; i++)
        {
            SpawnTile();
        }
	}

    public void CreateTiles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            LeftTiles.Push(Instantiate(tilePrefabs[0]));
            TopTiles.Push(Instantiate(tilePrefabs[1]));
            LeftTiles.Peek().name = "LeftTile";
            TopTiles.Peek().name = "TopTile";
            LeftTiles.Peek().SetActive(false);
            TopTiles.Peek().SetActive(false);
        }
    }
    /// <summary>
    /// Takes care for placing the tiles randomly and recyle them.
    /// Also adds pickups for bonus point randomly for red and golden apples.
    /// </summary>
    public void SpawnTile()
    {
        if (LeftTiles.Count == 0 || TopTiles.Count == 0)
        {
            CreateTiles(10);
        }

        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            GameObject tmp = LeftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
            currentTile = tmp;
        }
        else if(randomIndex == 1)
        {
            GameObject tmp = TopTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
            currentTile = tmp;
        }
        // Each tile will have a 50% chance of spawing a red- or golden-apple with their own chance, 1/8 and 1/50 respectily. 
        int spawnPickup = Random.Range(0, 2);

        if (spawnPickup == 0)
        {
            //int + ny if(redapple == 0) + 0, 10
            int redApple = Random.Range(0, 8);
            if (redApple == 0)
            {
                currentTile.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else if(spawnPickup == 1)
        {
            int goldenApple = Random.Range(0, 50);
            if (goldenApple == 0)
            {
                currentTile.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Game");
    }
}
