using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit {

    TileMapModel map;
    int TargetMapID;
    int destination_X;
    int destination_Y;

    public int Destination_X
    {
        get
        {
            return destination_X;
        }

        set
        {
            destination_X = value;
        }
    }

    public int Destination_Y
    {
        get
        {
            return destination_Y;
        }

        set
        {
            destination_Y = value;
        }
    }

    public TileMapModel Map
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

    public int GetDestinationID()
    {
        return TargetMapID;
    }

    public Exit (TileMapModel m, int ID, int x, int y)
    {
        Map = m;
        TargetMapID = ID;
        Destination_X = x;
        Destination_Y = y;
    }
}
