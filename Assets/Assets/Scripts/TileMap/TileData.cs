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
    TileMapData map;
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

    public TileMapData Map
    {
        get
        {
            return map;
        }

        set
        {
            map = value;
        }
    }

    public Tile() {

        

    }

    public Tile(TileMapData map, int x, int y, TileType t, Exit e = null)
    {
        this.map = map;
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

    public Tile[] GetNeighbours(Tile target, bool diagOkay = true)
    {
        Tile[] ns;

        if (diagOkay == false)
        {
            ns = new Tile[4];   // Tile order: N E S W
        }
        else
        {
            ns = new Tile[8];   // Tile order : N E S W NE SE SW NW
        }

        Tile n;

        n = map.GetTile(target.TileX, target.TileY + 1);
        ns[0] = n;  // Could be null, but that's okay.
        n = map.GetTile(target.TileX + 1, target.TileY);
        ns[1] = n;  // Could be null, but that's okay.
        n = map.GetTile(target.TileX, target.TileY - 1);
        ns[2] = n;  // Could be null, but that's okay.
        n = map.GetTile(target.TileX - 1, target.TileY);
        ns[3] = n;  // Could be null, but that's okay.

        if (diagOkay == true)
        {
            n = map.GetTile(target.TileX + 1, target.TileY + 1);
            ns[4] = n;  // Could be null, but that's okay.
            n = map.GetTile(target.TileX + 1, target.TileY - 1);
            ns[5] = n;  // Could be null, but that's okay.
            n = map.GetTile(target.TileX - 1, target.TileY - 1);
            ns[6] = n;  // Could be null, but that's okay.
            n = map.GetTile(target.TileX - 1, target.TileY + 1);
            ns[7] = n;  // Could be null, but that's okay.
        }


        return ns;
    }

    public int GetMovementCost()
    {
        int movementCost = 0;


        switch (TileType)
        {
            case TileType.Blank:
                movementCost = 0;
                break;
            case TileType.Floor:
                movementCost = 1;
                break;

            case TileType.Wall:
                movementCost = 0;
                break;

            case TileType.Exit:
                movementCost = 1;
                break;


        }

        if (Occupant != null)
        {
            movementCost = 0;
        }
        return movementCost;
    }

    public Vector3 GetWorldPos()
    {
        return new Vector3(tileX, tileY, 0.0f) + TileMapManager.Instance.GetTileOffset();
    }
}
