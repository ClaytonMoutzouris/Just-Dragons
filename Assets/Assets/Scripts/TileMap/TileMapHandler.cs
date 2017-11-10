using UnityEngine;
using System.Collections.Generic;

// Interface for the tilemap handler
public interface ITileMapHandler
{
    void NewMap(ITileMapModel model);
    ITileMapModel CurrentMap { get; }
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

    public void NewMap(ITileMapModel model)
    {
        model.OnTileTypeChanged += HandleTileTypeChanged;
        mapModels.Add(model);
        CurrentMap = model;
        SyncCurrentMap();
        
    }

    public float GetTileOffset()
    {
        return mapView.TileOffset();
    }

    /*
    public Vector3 GetTileWorldPos(Tile t)
    {

        return new Vector3(t.TileX + mapView.TileOffset(), t.TileY + mapView.TileOffset(), 0);
    }
    */

    private void SyncCurrentMap()
    {
        mapView.DrawMap(CurrentMap.tiles, (int)CurrentMap.mapSize.x, (int)CurrentMap.mapSize.y);
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, OnClickedEventArgs e)
    {


        GameManager.instance.characters[0].GetComponent<Moveable>().MoveTo(currentMap.tiles[e.x, e.y], 10);
        

        //mapView.DrawTile(cTile);
        //CameraController.instance.SetToTile(e.x, e.y);
    }

    private void HandleTileTypeChanged(object sender, TileTypeChangedEventArgs e)
    {

        mapView.DrawTile(e.tile);
    }

}