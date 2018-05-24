using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementComponent {

    protected bool moving = false; //if the IEntity is currently moving
    protected Tile destinationTile = null; //Where we want to be
    protected int speed = 5; //this is just a constant, determines the actual speed a sprite moves around
    protected IEntity IEntity;
    
    public MovementComponent(IEntity IEntity)
    {
        this.IEntity = IEntity;
    }

    public virtual void Update()
    {

    }

    public void MoveToTile(IEntity IEntity, Tile target)
    {
        if (moving)
            return;

        if (!target.IsWalkable())
            return;

        destinationTile = target;
        //check to see if where we want to be is where we already are
        if (IEntity.CurrentTile == destinationTile)
            return;

        IEntity.CurrentTile.Occupant = null;
        if (!moving)
        {
            moving = true;
        }
    }

    public void MoveToEntity(IEntity mover, IEntity target)
    {
        if (mover == target)
            return;
        MoveToTile(mover, TileMapManager.Instance.GetNearestNeighbour(mover.CurrentTile, target.CurrentTile));


    }

    public void MoveXYSpaces(IEntity IEntity, int x, int y)
        
    {
        if (moving)
            return;

        Tile temp = TileMapManager.Instance.CurrentMap.GetTile(IEntity.CurrentTile.TileX + x, IEntity.CurrentTile.TileY + y);
        if (temp != null)
        {
            MoveToTile(IEntity, temp);
        }
    }
}

public class PlayerMovementComponent : MovementComponent
{

    public PlayerMovementComponent(IEntity IEntity):base(IEntity)
    {
        speed = 10;
    }

    public override void Update()
    {
        if (moving && destinationTile != null)
        {
            float step = speed * Time.deltaTime;
            IEntity.Graphics.SetWorldPosition(Vector3.MoveTowards(IEntity.Graphics.GetWorldPosition(), destinationTile.GetWorldPos(), step));
            if (IEntity.Graphics.GetWorldPosition() == destinationTile.GetWorldPos() /*|| IEntity.Stats.GetStat("Movement").finalValue() <= 0*/)
            {
                moving = false;
                IEntity.CurrentTile = destinationTile;
                IEntity.CurrentTile.Occupant = IEntity;

                CombatManager.instance.CheckForCombat((Character)IEntity);

            }
        }
    }

}

public class NPCMovementComponent : MovementComponent
{

    public NPCMovementComponent(IEntity IEntity):base(IEntity)
    {
        speed = 5;
    }

    public override void Update()
    {
        if (moving && destinationTile != null)
        {
            float step = speed * Time.deltaTime;
            IEntity.Graphics.SetWorldPosition(Vector3.MoveTowards(IEntity.Graphics.GetWorldPosition(), destinationTile.GetWorldPos(), step));
            if (IEntity.Graphics.GetWorldPosition() == destinationTile.GetWorldPos() /*|| IEntity.Stats.GetStat("Movement").finalValue() <= 0*/)
            {
                moving = false;
                IEntity.CurrentTile = destinationTile;
                IEntity.CurrentTile.Occupant = IEntity;
            }
        }
    }

}