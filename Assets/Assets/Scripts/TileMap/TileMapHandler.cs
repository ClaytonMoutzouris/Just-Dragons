using UnityEngine;
using System;
using System.Collections.Generic;

public class MapChangedEventArgs : EventArgs
{
    public Exit exit;
    public ITileMapModel map;
}

// Interface for the tilemap handler
public interface ITileMapHandler
{
    ITileMapModel CurrentMap { get; }
    void SetUpTestWorld(List<ITileMapModel> models);
}

// Implementation of the enemy controller
public class TileMapHandler : ITileMapHandler
{
    // Keep references to the model and view
    //This is a list of all the maps in the game, ~TODO~ Make a better way to organize these, a list will do for now
    private readonly List<ITileMapModel> mapModels;
    private ITileMapModel currentMap;
    private readonly ITileMapView mapView;
    public static TileMapHandler instance;
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

    // Controller depends on interfaces for the model and view
    public TileMapHandler( ITileMapView view)
    {
        if (instance == null)
            instance = this;

            

        mapModels = new List<ITileMapModel>();
        this.mapView = view;

        // Listen to input from the view
        view.OnClicked += HandleClicked;
        //model.OnTileTypeChanged += HandleTileTypeChanged;
        // Listen to changes in the model
        //model.OnPositionChanged += HandlePositionChanged;

        // Set the view's initial state by synching with the model
        //SyncPosition();
    }


    public void SetUpTestWorld(List<ITileMapModel> models)
    {
        foreach(ITileMapModel m in models)
        {
            m.OnTileTypeChanged += HandleTileTypeChanged;
            mapModels.Add(m);
        }
        CurrentMap = models[0];
        SyncCurrentMap();

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


        GameManager.instance.characters[0].SetTile(currentMap.tiles[e.x, e.y]);
        

        //mapView.DrawTile(cTile);
        //CameraController.instance.SetToTile(e.x, e.y);
    }

    private void HandleTileTypeChanged(object sender, TileTypeChangedEventArgs e)
    {

        mapView.DrawTile(e.tile);
    }

}