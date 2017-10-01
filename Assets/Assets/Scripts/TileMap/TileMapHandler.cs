using UnityEngine;

// Interface for the tilemap handler
public interface ITileMapHandler
{
}

// Implementation of the enemy controller
public class TileMapHandler : ITileMapHandler
{
    // Keep references to the model and view
    //private readonly IEnemyModel model;
    private readonly ITileMapView view;

    // Controller depends on interfaces for the model and view
    public TileMapHandler(/*IEnemyModel model,*/ ITileMapView view)
    {
        //this.model = model;
        this.view = view;

        // Listen to input from the view
        view.OnClicked += HandleClicked;

        // Listen to changes in the model
        //model.OnPositionChanged += HandlePositionChanged;

        // Set the view's initial state by synching with the model
        //SyncPosition();
    }

    // Called when the view is clicked
    private void HandleClicked(object sender, OnClickedEventArgs e)
    {
        // Do something to the model

        view.RedrawTile(e.x, e.y);
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