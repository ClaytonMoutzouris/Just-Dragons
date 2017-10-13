using System;

using UnityEngine;

// Dispatched when the enemy's position changes
public class EnemyPositionChangedEventArgs : EventArgs
{
}

// Interface for the model
public interface IEnemyModel
{
    // Dispatched when the position changes
    event EventHandler<EnemyPositionChangedEventArgs> OnPositionChanged;

    // Position of the enemy
    Vector3 Position { get; set; }
}

// Implementation of the enemy model interface
public class EnemyModel : IEnemyModel
{
    // Backing field for the enemy's position
    private Vector3 position;

    public event EventHandler<EnemyPositionChangedEventArgs> OnPositionChanged = (sender, e) => { };

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
}