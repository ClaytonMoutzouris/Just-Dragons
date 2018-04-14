using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementComponent {

    protected bool moving = false; //if the entity is currently moving
    protected Tile destinationTile = null; //Where we want to be
    protected int speed = 5; //this is just a constant, determines the actual speed a sprite moves around
    
    public MovementComponent()
    {

    }

    public virtual void Update(Entity entity)
    {

    }

    public void MoveToTile(Entity entity, Tile target)
    {
        if (moving)
            return;

        if (!target.IsWalkable())
            return;

        destinationTile = target;
        //check to see if where we want to be is where we already are
        if (entity.CurrentTile == destinationTile)
            return;

        entity.CurrentTile.Occupant = null;
        if (!moving)
        {
            moving = true;
        }
    }

    public void SetToTile(Entity entity, Tile target)
    {
        entity.CurrentTile = target;
        entity.worldPosition = entity.CurrentTile.GetWorldPos();
        Debug.Log("setting " + entity + " to " + target);
        entity.CurrentTile.Occupant = entity;
    }

    public void MoveToEntity(Entity mover, Entity target)
    {
        if (mover == target)
            return;
        MoveToTile(mover, TileMapManager.Instance.GetNearestNeighbour(mover.CurrentTile, target.CurrentTile));


    }

    public void MoveXYSpaces(Entity entity, int x, int y)
        
    {
        if (moving)
            return;

        Tile temp = TileMapManager.Instance.GetTile(entity.CurrentTile.TileX + x, entity.CurrentTile.TileY + y);
        if (temp != null)
        {
            MoveToTile(entity, temp);
        }
    }
}

public class PlayerMovementComponent : MovementComponent
{

    public PlayerMovementComponent()
    {
        speed = 10;
    }

    public override void Update(Entity entity)
    {
        if (moving && destinationTile != null)
        {
            float step = speed * Time.deltaTime;
            entity.worldPosition = Vector3.MoveTowards(entity.worldPosition, destinationTile.GetWorldPos(), step);
            if (entity.worldPosition == destinationTile.GetWorldPos() /*|| entity.Stats.GetStat("Movement").finalValue() <= 0*/)
            {
                moving = false;
                entity.CurrentTile = destinationTile;
                entity.CurrentTile.Occupant = entity;

                CombatManager.instance.CheckForCombat(entity);

            }
        }
    }

}

public class NPCMovementComponent : MovementComponent
{

    public override void Update(Entity entity)
    {
        if (moving && destinationTile != null)
        {
            float step = speed * Time.deltaTime;
            entity.worldPosition = Vector3.MoveTowards(entity.worldPosition, destinationTile.GetWorldPos(), step);
            if (entity.worldPosition == destinationTile.GetWorldPos() /*|| entity.Stats.GetStat("Movement").finalValue() <= 0*/)
            {
                moving = false;
                entity.CurrentTile = destinationTile;
                entity.CurrentTile.Occupant = entity;
            }
        }
    }

}