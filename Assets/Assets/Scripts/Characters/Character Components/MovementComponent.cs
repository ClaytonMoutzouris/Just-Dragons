using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementComponent {

    public bool moving = false; //if the IEntity is currently moving
    protected Tile destinationTile = null; //Where we want to be
    protected int speed = 5; //this is just a constant, determines the actual speed a sprite moves around
    protected IEntity IEntity;
    protected List<Tile> path;
    
    public MovementComponent(IEntity IEntity)
    {
        this.IEntity = IEntity;
        path = new List<Tile>();
    }

    public virtual void Update()
    {

    }

    public bool PathEmpty()
    {
        return path.Count <= 0;
    }

    public virtual void MoveToTile(Tile target)
    {
        //save us some time not looking for paths we know we can't get to
        if (!target.IsWalkable())
            return;

        path = TileMapPathfinder.Path(IEntity.CurrentTile, target);
        Debug.Log(IEntity.EntityName + " moving to " + target.GetMovementCost());

    }

    public void MoveToEntity(IEntity target)
    {
        if (IEntity == target)
            return;
        MoveToTile(target.CurrentTile);

    }

    public void MoveXYSpaces(int x, int y)
        
    {
        if (moving)
            return;

        Tile temp = TileMapManager.Instance.CurrentMap.GetTile(IEntity.CurrentTile.TileX + x, IEntity.CurrentTile.TileY + y);
        if (temp != null)
        {
            MoveToTile(temp);
        }
    }

}

public class PlayerMovementComponent : MovementComponent
{

    public PlayerMovementComponent(IEntity IEntity) : base(IEntity)
    {
        speed = 10;
    }

    public override void Update()
    {
        if (path.Count > 0) //this means we have movements to make
        {
            if (!moving) //we currently arent moving
            {
                moving = true;
                IEntity.CurrentTile.Occupant = null;
                destinationTile = path[0];
            }

            if (moving)
            {
                float step = speed * Time.deltaTime;
                IEntity.Graphics.SetWorldPosition(Vector3.MoveTowards(IEntity.Graphics.GetWorldPosition(), destinationTile.GetWorldPos(), step));
                if (IEntity.Graphics.GetWorldPosition() == destinationTile.GetWorldPos() /*|| IEntity.Stats.GetStat("Movement").finalValue() <= 0*/)
                {
                    moving = false;
                    IEntity.CurrentTile = destinationTile;

                    path.RemoveAt(0);

                    if (PathEmpty())
                    {
                        IEntity.CurrentTile.Occupant = IEntity;
                        CombatManager.instance.CheckForCombat((Character)IEntity);
                    }

                }

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

        if(path.Count > 0) //this means we have movements to make
        {
            if (!moving) //we currently arent moving
            {
                moving = true;
                IEntity.CurrentTile.Occupant = null;
                destinationTile = path[0];

            }

            if (moving)
            {
                float step = speed * Time.deltaTime;
                IEntity.Graphics.SetWorldPosition(Vector3.MoveTowards(IEntity.Graphics.GetWorldPosition(), destinationTile.GetWorldPos(), step));
                if (IEntity.Graphics.GetWorldPosition() == destinationTile.GetWorldPos() /*|| IEntity.Stats.GetStat("Movement").finalValue() <= 0*/)
                {
                    moving = false;
                    IEntity.CurrentTile = destinationTile;

                    path.RemoveAt(0);

                    if (PathEmpty())
                    {
                        IEntity.CurrentTile.Occupant = IEntity;
                    }
                }

            }
        }
    }

}