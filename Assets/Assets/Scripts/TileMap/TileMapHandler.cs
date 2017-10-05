using UnityEngine;
using System.Collections.Generic;

// Interface for the tilemap handler
public interface ITileMapHandler
{
    void NewMap(ITileMapModel model);
}

// Implementation of the enemy controller
public class TileMapHandler : ITileMapHandler
{
    // Keep references to the model and view
    //This is a list of all the maps in the game, ~TODO~ Make a better way to organize these, a list will do for now
    private readonly List<ITileMapModel> mapModels;
    private ITileMapModel currentMap;
    private readonly ITileMapView mapView;

    // Controller depends on interfaces for the model and view
    public TileMapHandler( ITileMapView view)
    {
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

    public void NewMap(ITileMapModel model)
    {
        model.OnTileTypeChanged += HandleTileTypeChanged;
        mapModels.Add(model);
        currentMap = model;
        SyncCurrentMap();
        
    }

    private void SyncCurrentMap()
    {
        mapView.DrawMap(currentMap.tiles, (int)currentMap.mapSize.x, (int)currentMap.mapSize.y);
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, OnClickedEventArgs e)
    {
        // Do something to the model
        
        mapView.RedrawTile(e.x, e.y);
        CameraController.instance.SetToTile(e.x, e.y);
    }

    private void HandleTileTypeChanged(object sender, TileTypeChangedEventArgs e)
    {
       

    }

    /*
    // Called when the model's position changes
    private void HandlePositionChanged(object sender, EnemyPositionChangedEventArgs e)
    {
        // Update the view with the new position
        SyncPosition();
    }
    */

    // Sync the view's position with the model's position
    /*
    private void SyncPosition()
    {
        view.Position = model.Position;
    }
    */
}