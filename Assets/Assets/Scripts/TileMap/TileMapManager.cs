using UnityEngine;
using System;
using System.Collections.Generic;

public class MapChangedEventArgs : EventArgs
{
    public Exit exit;
    public ITileMapData map;
}

// This will serve as a sort of database for the tilemaps
public class TileMapManager : MonoBehaviour
{
    // Keep references to the model and view
    //This is a list of all the maps in the game, ~TODO~ Make a better way to organize these, a list will do for now
    private List<ITileMapData> mapModels;
    private ITileMapData currentMap;
    private ITileMapObject mapView;
    public List<IEntity> entities;
    public int currentMapIndex;

    public static TileMapManager Instance { get; private set; }
    public ITileMapData CurrentMap
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

        entities = new List<IEntity>();
        mapModels = new List<ITileMapData>();
        // Listen to input from the view

        mapView = view;
        currentMapIndex = 0;
    }


    public void SetUpTestWorld(List<ITileMapData> models)
    {
        //draws the initial map
        foreach(ITileMapData m in models)
        {
            mapModels.Add(m);
        }
        CurrentMap = models[0];
        print("Map is set up");
        SyncCurrentMap();

    }


    public Vector2 MouseToTilePosition(Vector2 mousePos)
    {
        int tilex = Mathf.FloorToInt(mousePos.x / mapView.TileSize());
        int tiley = Mathf.FloorToInt(mousePos.y / mapView.TileSize());
        return new Vector3(tilex, tiley) + GetTileOffset();
    }

    public Tile GetTileClicked(Vector2 mousePos)
    {

        Vector2 tilePos = MouseToTilePosition(mousePos);
        return CurrentMap.GetTile((int)tilePos.x, (int)tilePos.y);
    }

    /*
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

        */


    

    public Tile GetNearestNeighbour(Tile start, Tile target)
    {
        Tile[] neighbours = target.Neighbours;
        Tile nearestNeighbour = null;
        if (start.TileX < target.TileX)
        {
            if (start.TileY < target.TileY)
            {
                nearestNeighbour = neighbours[5];
            }
            else if (start.TileY == target.TileY)
            {
                nearestNeighbour = neighbours[3];

            }
            else if (start.TileY > target.TileY)
            {
                nearestNeighbour = neighbours[0];

            }

        }
        else if (start.TileX == target.TileX)
        {
            if (start.TileY < target.TileY)
            {
                nearestNeighbour = neighbours[6];
            }
            else if (start.TileY > target.TileY)
            {
                nearestNeighbour = neighbours[1];

            }

        }
        else if (start.TileX > target.TileX)
        {
            if (start.TileY < target.TileY)
            {
                nearestNeighbour = neighbours[7];
            }
            else if (start.TileY == target.TileY)
            {
                nearestNeighbour = neighbours[4];
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
                
                temp = currentMap.GetTile(i, j);
                if(temp != null)
                tilesInRange.Add(temp);

            }
        }

        return tilesInRange;

    }

    public void ChangeMap()
    {
        currentMapIndex++;
        if(currentMapIndex > mapModels.Count - 1)
        {
            CurrentMap = mapModels[0];
            currentMapIndex = 0;
        } else
        {
            CurrentMap = mapModels[currentMapIndex];

        }

        Debug.Log("Changed to Map: " + currentMap.mapID);

        SyncCurrentMap();
        GameManager.instance.characters[0].SetToTile(currentMap.tiles[(int)CurrentMap.mapSize.x/2, (int)CurrentMap.mapSize.y / 2]);

    }

    private void Update()
    {

    }

    public Vector3 GetTileOffset()
    {
        return mapView.TileOffset();
    }


    private void SyncCurrentMap()
    {
        foreach(IEntity e in entities)
        {
            Destroy(e.Graphics.entity);
        }

        mapView.DrawMap(CurrentMap.tiles, (int)CurrentMap.mapSize.x, (int)CurrentMap.mapSize.y);
        

        foreach (NPCPrototype cd in currentMap.Characters)
        {
            entities.Add(CharacterDatabase.CreateCharacter(cd));
        }

        TileMapPathfinder.SetMap(currentMap);
        
        List<Tile> path = TileMapPathfinder.Path(currentMap.GetTile((int)currentMap.mapSize.x / 2, (int)currentMap.mapSize.y / 2), currentMap.GetTile((int)UnityEngine.Random.Range(1,currentMap.mapSize.x-1), (int)UnityEngine.Random.Range(1, currentMap.mapSize.y - 1)));

        foreach(Tile t in path)
        {
            t.TileType = TileType.Wall;
            mapView.RedrawTile(t);
        }
    }




}