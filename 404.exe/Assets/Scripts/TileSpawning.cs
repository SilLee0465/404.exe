using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawning : MonoBehaviour
{
    //use this to increase the variants of the prefab
    public List<GameObject> easyTiles = new List<GameObject>();
    public List<GameObject> mediumTiles = new List<GameObject>();
    public List<GameObject> hardTiles = new List<GameObject>();
    public List<GameObject> specialTiles = new List<GameObject>();
    public GameObject defaultTile;

    int totalTilesRan;
    int randomTIle;
    int random;

    GameObject previousTile;
    GameObject currentTile;
    GameObject triggerPoint;
    Transform nextSpawnPoint;

    void Start()
    {
        currentTile = defaultTile;
        nextSpawnPoint = currentTile.transform.GetChild(0);
        triggerPoint = currentTile.transform.GetChild(1).gameObject;
        totalTilesRan = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerPoint) 
        {
            spawnTile();
            totalTilesRan++;
            Debug.Log(totalTilesRan);
            Debug.Log(currentTile);
            //can increase the speed of player here
        }
        if(other.gameObject.name == "DeletePoint")
        {
            destroyTiles();
        }
    }

    void spawnTile()
    {
        previousTile = currentTile;
        random = Random.Range(0, 4);
        {
            if(random <= 2)
            {
                if (totalTilesRan >= 10)
                {
                    //hard tiles
                    randomTIle = Random.Range(0, hardTiles.Count);
                    GameObject Tile = Instantiate(hardTiles[randomTIle], nextSpawnPoint.position, Quaternion.identity);
                    currentTile = Tile;
                    nextSpawnPoint = Tile.transform.GetChild(0);
                    triggerPoint = Tile.transform.GetChild(1).gameObject;
                }
                else if (totalTilesRan >= 5)
                {
                    //hard tiles
                    randomTIle = Random.Range(0, mediumTiles.Count);
                    GameObject Tile = Instantiate(mediumTiles[randomTIle], nextSpawnPoint.position, Quaternion.identity);
                    currentTile = Tile;
                    nextSpawnPoint = Tile.transform.GetChild(0);
                    triggerPoint = Tile.transform.GetChild(1).gameObject;
                }
                else if (totalTilesRan >= 0)
                {
                    //hard tiles
                    randomTIle = Random.Range(0, easyTiles.Count);
                    GameObject Tile = Instantiate(easyTiles[randomTIle], nextSpawnPoint.position, Quaternion.identity);
                    currentTile = Tile;
                    nextSpawnPoint = Tile.transform.GetChild(0);
                    triggerPoint = Tile.transform.GetChild(1).gameObject;
                }
            }
            else if(random >= 3)
            {
                randomTIle = Random.Range(0, specialTiles.Count);
                GameObject Tile = Instantiate(specialTiles[randomTIle], nextSpawnPoint.position, Quaternion.identity);
                currentTile = Tile;
                nextSpawnPoint = Tile.transform.GetChild(0);
                triggerPoint = Tile.transform.GetChild(1).gameObject;
            }
        }
    }

    void destroyTiles()
    {
        Destroy(previousTile);
    }
}
