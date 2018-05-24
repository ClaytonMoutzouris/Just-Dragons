using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Blank, Floor, Wall, Exit };
public enum TileMoveCost { Invalid, Walkable };

public class Tile
{
    Tile[] neighbours;
    TileType _tileType;
    int tileX;
    int tileY;
    IEntity occupant = null;
    Exit exit;
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

    public IEntity Occupant
    {
        get
        {
            return occupant;
        }

        set
        {
            //Debug.Log("X: " + TileX + ", Y: " + TileY + " - Occupant: " + value);
            occupant = value;
        }
    }

    public Tile[] Neighbours
    {
        get
        {
            return neighbours;
        }

        set
        {
            neighbours = value;
        }
    }

    public Tile() {

        

    }

    public Tile(int x, int y, TileType t, Exit e = null)
    {
        neighbours = new Tile[9];
        tileX = x;
        tileY = y;
        TileType = t;
        exit = e;
    }


    public bool IsWalkable()
    {
        switch (TileType)
        {
            case TileType.Floor:
                return true;
            case TileType.Wall:
                return false;
            case TileType.Exit:
                return true;

        }

        return false;
    }

    public Exit GetExit()
    {
        return exit;
    }

    public Tile GetNearestNeighbour()
    {
        return null;
    }

    public int GetMovementCost()
    {
        switch (TileType)
        {
            case TileType.Blank:
                return 0;
            case TileType.Floor:
                return 1;
            case TileType.Wall:
                return 0;
            case TileType.Exit:
                return 1;

        }
        return 0;
    }

    public Vector3 GetWorldPos()
    {
        return new Vector3(tileX, tileY, 0.0f) + TileMapManager.Instance.GetTileOffset();
    }
}
