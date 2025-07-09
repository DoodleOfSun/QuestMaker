using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;using System.Linq;


public class SelectRandomMap : MonoBehaviour
{
    public GameObject groundBase;
    public GameObject seaBase;

    public List<Tilemap> groundTiles;
    public List<Tilemap> seaTiles;

    public Vector2 spawnSpotPositioning = new Vector2(0.125f, 0.125f);

    private List<Vector2> spawnSpotList = new List<Vector2>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundBase.SetActive(false);
        seaBase.SetActive(false);
        SelectRandomTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectRandomTiles()
    {
        int randomBaseValue = Random.Range(0, 2);
        if (randomBaseValue == 0)
        {
            groundBase.gameObject.SetActive(true);

            int randomTileValue = Random.Range(0, groundTiles.Count);
            groundTiles[randomTileValue].gameObject.SetActive(true);

            for (int x = groundTiles[randomTileValue].cellBounds.xMin; x < groundTiles[randomTileValue].cellBounds.xMax; x++)
            {
                for (int y = groundTiles[randomTileValue].cellBounds.yMin; y < groundTiles[randomTileValue].cellBounds.yMax; y++)
                {
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    if (groundTiles[randomTileValue].HasTile(cellPos))
                    {
                        TileBase tile = groundTiles[randomTileValue].GetTile(cellPos);
                        if (tile.name.Contains("12") || tile.name.Contains("13") || tile.name.Contains("14") || tile.name.Contains("15")
                             || tile.name.Contains("27") || tile.name.Contains("28") || tile.name.Contains("29") || tile.name.Contains("30")
                              || tile.name.Contains("42") || tile.name.Contains("43") || tile.name.Contains("44") || tile.name.Contains("45"))
                        {
                            Vector2 spawnSpots = new Vector2(groundTiles[randomTileValue].CellToWorld(cellPos).x + spawnSpotPositioning.x,
                                                             groundTiles[randomTileValue].CellToWorld(cellPos).y + spawnSpotPositioning.y);
                            spawnSpotList.Add(spawnSpots);
                        }
                    }
                }
            }


        }


        else if (randomBaseValue == 1)
        {
            seaBase.gameObject.SetActive(true);

            int randomTileValue = Random.Range(0, seaTiles.Count);
            seaTiles[randomTileValue].gameObject.SetActive(true);
            
            for (int x = seaTiles[randomTileValue].cellBounds.xMin; x < seaTiles[randomTileValue].cellBounds.xMax; x++)
            {
                for (int y = seaTiles[randomTileValue].cellBounds.yMin; y < seaTiles[randomTileValue].cellBounds.yMax; y++)
                {
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    if (seaTiles[randomTileValue].HasTile(cellPos))
                    {
                        TileBase tile = seaTiles[randomTileValue].GetTile(cellPos);
                        if (tile.name.Contains("89") || tile.name.Contains("90") || tile.name.Contains("91"))
                        {
                            Vector2 spawnSpots = new Vector2(seaTiles[randomTileValue].CellToWorld(cellPos).x + spawnSpotPositioning.x,
                                                            seaTiles[randomTileValue].CellToWorld(cellPos).y + spawnSpotPositioning.y);
                            spawnSpotList.Add(spawnSpots);
                        }
                    }
                }
            }
        }

        SpawnMonsters(randomBaseValue);
    }

    private void SpawnMonsters(int baseValue)
    {
        List<Vector2> spawnSpots = new List<Vector2>();
        bool isDuplicate = false;  

        if (baseValue == 0)
        {
            Debug.Log("0발동");
            if (GameSceneManager.instance.gameLevel == 1)
            {
                int i = 0;
                while (i < 8)
                {
                    Vector2 randomSpot = spawnSpotList[Random.Range(0, 8)];

                    for (int j = 0; j < spawnSpots.Count; j++)
                    {
                        if (randomSpot == spawnSpots[j])
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (isDuplicate)
                    {
                        isDuplicate = false;
                    }
                    else if (!isDuplicate)
                    {
                        spawnSpots.Add(randomSpot);
                        i++;
                    }
                }
            }
        }


        else if (baseValue == 1)
        {
            if (GameSceneManager.instance.gameLevel == 1)
            {
                int i = 0;
                while (i < 8)
                {
                    Vector2 randomSpot = spawnSpotList[Random.Range(0, 8)];

                    for (int j = 0; j < spawnSpots.Count; j++)
                    {
                        if (randomSpot == spawnSpots[j])
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (isDuplicate)
                    {
                        isDuplicate = false;
                    }
                    else if (!isDuplicate)
                    {
                        spawnSpots.Add(randomSpot);
                        i++;
                    }
                }
            }
        }

        Debug.Log("결과 : ");

        if (baseValue == 0)
        {
            if (GameSceneManager.instance.gameLevel == 1)
            {
                for (int i = 0; i < spawnSpots.Count; i++)
                {
                    Debug.Log(spawnSpots[i]);

                    GameObject monster = Instantiate(GameSceneManager.instance.slimeForMaps, spawnSpots[i], Quaternion.identity);
                }
            }
        }


        if (baseValue == 1)
        {
            if (GameSceneManager.instance.gameLevel == 1)
            {
                for (int i = 0; i < spawnSpots.Count; i++)
                {
                    Debug.Log(spawnSpots[i]);

                    GameObject monster = Instantiate(GameSceneManager.instance.slimeForMaps, spawnSpots[i], Quaternion.identity);
                }
            }
        }

    }
}
