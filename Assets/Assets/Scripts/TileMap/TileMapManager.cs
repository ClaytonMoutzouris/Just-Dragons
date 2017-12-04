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

        
        //model.OnTileTypeChanged += HandleTileTypeChanged;
        // Listen to changes in the model
        //model.OnPositionChanged += HandlePositionChanged;

        // Set the view's initial state by synching with the model
        //SyncPosition();
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

    public float GetTileOffset()
    {
        return mapView.TileOffset();
    }


    private void SyncCurrentMap()
    {
        mapView.DrawMap(CurrentMap.tiles, (int)CurrentMap.mapSize.x, (int)CurrentMap.mapSize.y);
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, OnClickedEventArgs e)
    {


        //GameManager.instance.characters[0].SetTile(currentMap.tiles[e.x, e.y]);
        

        //mapView.DrawTile(cTile);
        //CameraController.instance.SetToTile(e.x, e.y);
    }

    private void HandleTileChanged(object sender, TileChangedEventArgs e)
    {

        mapView.DrawTile(e.tile);
    }

}