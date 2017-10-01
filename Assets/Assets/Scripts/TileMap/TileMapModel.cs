using System;

using UnityEngine;

// Dispatched when the enemy's position changes
public class TileTypeChangedEventArgs : EventArgs
{
}

// Interface for the model
public interface ITileMapModel
{
    // Dispatched when the position changes
    event EventHandler<TileTypeChangedEventArgs> OnTileTypeChanged;

    
}

// Implementation of the enemy model interface
public class TileMapModel : ITileMapModel
{

    public event EventHandler<TileTypeChangedEventArgs> OnTileTypeChanged = (sender, e) => { };

    public Tile[,] tiles;

    public void BuildMap()
    {

    }

    /*
    public Vector3 Position
    {
        get { return position; }
        set
        {
            // Only if the position changes
            if (position != value)
            {
                // Set new position
                position = value;

                // Dispatch the 'position changed' event
                var eventArgs = new EnemyPositionChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }
    }
    */
}
