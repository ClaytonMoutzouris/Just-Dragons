using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Blank, Floor, Wall, Dirt, Water };


public class Tile
{
    TileType _tileType;
    int tileX;
    int tileY;
    Character Occupant = null;

    public TileType TileType
    {
        get
        {
            return _tileType;
        }

        set
        {
            _tileType = value;
        }
    }


    public int TileX
    {
        get
        {
            return tileX;
        }

        set
        {
            tileX = value;
        }
    }

    public int TileY
    {
        get
        {
            return tileY;
        }

        set
        {
            tileY = value;
        }
    }

    public Tile() { }

    public Tile(int x, int y, TileType t)
    {

        tileX = x;
        tileY = y;
        TileType = t;
    }

    public Vector3 GetWorldPos()
    {
        return new Vector3(TileMapHandler.instance.GetTileOffset() + tileX, TileMapHandler.instance.GetTileOffset() + tileY, 0.0f);
    }
}
