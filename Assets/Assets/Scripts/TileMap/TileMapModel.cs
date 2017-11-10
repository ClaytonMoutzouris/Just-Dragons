using System;
using System.Collections.Generic;
using UnityEngine;

// Dispatched when the enemy's position changes
public class TileTypeChangedEventArgs : EventArgs
{
    public Tile tile;
}

// Interface for the model
public interface ITileMapModel
{
    // Dispatched when the position changes
    event EventHandler<TileTypeChangedEventArgs> OnTileTypeChanged;
    void BuildMap();
    void ChangeTile(int x, int y);
    Tile[,] tiles { get; }
    Vector2 mapSize { get; }
    //List<IEnemyController> enemyList { get; }
}

// Implementation of the enemy model interface
public class TileMapModel : ITileMapModel
{
    public Vector2 mapSize { get; set; }
    public event EventHandler<TileTypeChangedEventArgs> OnTileTypeChanged = (sender, e) => { };
    //public List<IEnemyController> enemyList { get; set; }
    public Tile[,] tiles { get; set; }

    public TileMapModel(int xSize, int ySize)
    {
        mapSize = new Vector2(xSize, ySize);
        tiles = new Tile[xSize, ySize];
        BuildMap();
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    
    public void ChangeTile(int x, int y)
    {
        Tile cTile = tiles[x, y];
        if ((int)cTile.TileType < 4)
        {
            cTile.TileType = cTile.TileType + 1;
        }
        else
        {
            cTile.TileType = TileType.Floor;
        }

        // Dispatch the 'on clicked' event
        var eventArgs = new TileTypeChangedEventArgs();
        eventArgs.tile = cTile;
        OnTileTypeChanged(this, eventArgs);
    }

    public void BuildMap()
    {
        for(int y = 0; y< mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                
                if(x == 0 || x == mapSize.x - 1 || y == 0 || y == mapSize.y - 1)
                {
                    tiles[x, y] = new Tile(x, y, TileType.Wall);
                } else
                {
                    tiles[x, y] = new Tile(x, y, TileType.Floor);
                }
                
                //tiles[x, y] = new Tile(x, y, TileType.Floor);

            }
        }

    }


    
    /*
    public Vector3 Position
    {
        get { return position; }
        set
        {
            // Only if the position changes
            if (position != value)
            {
                // Set new position
                position = value;

                // Dispatch the 'position changed' event
                var eventArgs = new EnemyPositionChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }
    */
}
