using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapTown : TileMapData {

    List<IEntity> objects;

    public TileMapTown(int xSize, int ySize, int ID) : base(xSize, ySize, ID)
    {
        BuildMap();
        AddEntities();
        SetTileNeighbours();

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
                    tiles[x, y] = new Tile(this, x, y, TileType.Wall);
                }
                else
                {
                    if (x == exit_x && y == exit_y)
                    {
                        tiles[x, y] = new Tile(this, x, y, TileType.Exit, new Exit(this, mapID + 1, 25, 25));
                    }
                    else
                    {
                        tiles[x, y] = new Tile(this, x, y, TileType.Floor);
                    }
                }

            }
        }

        
    }



    }
