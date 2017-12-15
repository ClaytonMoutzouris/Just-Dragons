using UnityEngine;
using System;
using System.Collections.Generic;

public class MapChangedEventArgs : EventArgs
{
    public Exit exit;
    public ITileMapModel map;
}

// Implementation of the enemy controller
public class TileMapManager : MonoBehaviour
{
    // Keep references to the model and view
    //This is a list of all the maps in the game, ~TODO~ Make a better way to organize these, a list will do for now
    private List<ITileMapModel> mapModels;
    private ITileMapModel currentMap;
    private ITileMapObject mapView;
    public static TileMapManager Instance { get; private set; }
    public ITileMapModel CurrentMap
    {
        get
        {
            return currentMap;
        }

        set
        {
            currentMap = value;
        }

    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    // Controller depends on interfaces for the model and view
    public void Initialize( ITileMapObject view)
    {


        mapModels = new List<ITileMapModel>();
        // Listen to input from the view
        view.OnClicked += HandleClicked;

        mapView = view;

    }


    public void SetUpTestWorld(List<ITileMapModel> models)
    {
        //draws the initial map
        foreach(ITileMapModel m in models)
        {
            m.OnTileChanged += HandleTileChanged;
            mapModels.Add(m);
        }
        CurrentMap = models[0];
        SyncCurrentMap();

    }

    public Tile GetTile(int x , int y)
    {
        if (x < 0 || x >= CurrentMap.mapSize.x || y < 0 || y >= CurrentMap.mapSize.y)
            return null;

        return CurrentMap.tiles[x, y];
    }

    public Tile[] GetNeighbours(Tile target)
    {
        Tile[] tiles = new Tile[9];

        //Top right neighbour
        tiles[0] = GetTile(target.TileX - 1, target.TileY + 1);
        //Top neighbour
        tiles[1] = GetTile(target.TileX, target.TileY + 1);
        //Top Left neighbour
        tiles[2] = GetTile(target.TileX + 1, target.TileY + 1);
        //right neighbour
        tiles[3] = GetTile(target.TileX - 1, target.TileY);
        //Tile in question
        tiles[4] = target;
        //Top Left neighbour
        tiles[5] = GetTile(target.TileX + 1, target.TileY);
        //Bottom right neighbour
        tiles[6] = GetTile(target.TileX - 1, target.TileY - 1);
        //Bottom neighbour
        tiles[7] = GetTile(target.TileX, target.TileY - 1);
        //Bottom Left neighbour
        tiles[8] = GetTile(target.TileX + 1, target.TileY - 1);



        return tiles;

    }

    public Tile GetNearestNeighbour(Tile start, Tile target)
    {
        Tile[] neighbours = GetNeighbours(target);
        Tile nearestNeighbour = null;
        if(start.TileX < target.TileX )
        {
            if (start.TileY < target.TileY)
            {
                nearestNeighbour = neighbours[6];
            }
            else if (start.TileY == target.TileY)
            {
                nearestNeighbour = neighbours[3];

            }
            else if (start.TileY > target.TileY)
            {
                nearestNeighbour = neighbours[0];

            }

        } else if (start.TileX == target.TileX)
        {
            if (start.TileY < target.TileY)
            {
                nearestNeighbour = neighbours[7];
            }
            else if (start.TileY > target.TileY)
            {
                nearestNeighbour = neighbours[1];

            }

        } else if (start.TileX > target.TileX)
        {
            if (start.TileY < target.TileY)
            {
                nearestNeighbour = neighbours[8];
            }
            else if (start.TileY == target.TileY)
            {
                nearestNeighbour = neighbours[5];
            }
            else if (start.TileY > target.TileY)
            {
                nearestNeighbour = neighbours[2];

            }
        }


        return nearestNeighbour;
    }

    public List<Tile> GetTilesInRange(Tile tile, int range)
    {
        List<Tile> tilesInRange = new List<Tile>();
        Tile temp;
        for(int i = tile.TileX-range; i<tile.TileX + range; i++)
        {
            for (int j = tile.TileY - range; j < tile.TileY + range; j++)
            {
                //Starting in the bottom left
                temp = GetTile(i, j);
                if(temp != null)
                tilesInRange.Add(temp);

            }
        }

        return tilesInRange;

    }

    public void ChangeMap(Exit e)
    {
        if(e.GetDestinationID() > mapModels.Count - 1)
        {
            CurrentMap = mapModels[0];
        } else
        {
            CurrentMap = mapModels[e.GetDestinationID()];

        }

        Debug.Log("Changed to Map: " + currentMap.mapID);
        SyncCurrentMap();
        DelegatesAndEvents.TileMapChanged(e, currentMap);
        //GameManager.instance.characters[0].SetTile(currentMap.tiles[e.Destination_X, e.Destination_Y], true);

    }

    public Vector3 GetTileOffset()
    {
        return mapView.TileOffset();
    }


    private void SyncCurrentMap()
    {
        mapView.DrawMap(CurrentMap.tiles, (int)CurrentMap.mapSize.x, (int)CurrentMap.mapSize.y);
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, OnMapClickedEventArgs e)
    {
        /*
        Entity temp = GetTile(e.x, e.y).Occupant;

        if (temp != null)
        {
            Debug.Log("Clicked on " + temp.Name);
            temp.GetComponent<Selectable>().Select();
        } else
        {
            GameManager.instance.ClearSelected();
        }
        //GameManager.instance.characters[0].SetTile(currentMap.tiles[e.x, e.y]);
        

        //mapView.DrawTile(cTile);
        //CameraController.instance.SetToTile(e.x, e.y);
        */
    }

    private void HandleTileChanged(object sender, TileChangedEventArgs e)
    {

        mapView.DrawTile(e.tile);
    }

}