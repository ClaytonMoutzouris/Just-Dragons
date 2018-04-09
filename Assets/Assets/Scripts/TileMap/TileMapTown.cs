using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapTown : TileMapData {

    List<Entity> objects;

    public TileMapTown(int xSize, int ySize, int ID) : base(xSize, ySize, ID)
    {
        BuildMap();
        AddEntities();

    }

    public override void BuildMap()
    {
        int exit_x, exit_y;
        exit_x = (int)UnityEngine.Random.Range(1, mapSize.x - 2);
        exit_y = (int)mapSize.y -2;


        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {

                if (x == 0 || x == mapSize.x - 1 || y == 0 || y == mapSize.y - 1)
                {
                    tiles[x, y] = new Tile(x, y, TileType.Wall);
                }
                else
                {
                    if (x == exit_x && y == exit_y)
                    {
                        tiles[x, y] = new Tile(x, y, TileType.Exit, new Exit(this, mapID + 1, 25, 25));
                    }
                    else
                    {
                        tiles[x, y] = new Tile(x, y, TileType.Floor);
                    }
                }

            }
        }

        
    }

    public void AddObjects()
    {
        int numObjs = Random.Range(3, 6);
        var treePrefab = Resources.Load<GameObject>("Prefabs/Tree");
        GameObject treeTemp = null;

        for (int i = 0; i < numObjs; i++)
        {

            treeTemp = Object.Instantiate(treePrefab);
            treeTemp.GetComponent<Entity>().SetToTile(TileMapManager.Instance.GetTile(Random.Range(1, 48), Random.Range(1, 48)));
            
            treeTemp.GetComponent<Entity>().Name = "Tree " + i;
            objects.Add(treeTemp.GetComponent<Entity>());
        }

    }

    }
