using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Floor, Wall, Dirt };


public class Tile {
    TileType _tileType;
    Vector2 _tilePosition;

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

    public Vector2 TilePosition
    {
        get
        {
            return _tilePosition;
        }

        set
        {
            _tilePosition = value;
        }
    }

    public Tile() { }
}
