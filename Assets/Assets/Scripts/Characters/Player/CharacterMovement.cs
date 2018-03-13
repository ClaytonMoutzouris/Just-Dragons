using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementController
{
    bool IsMoving();
    void MoveToTile(Tile target);
    void SetToTile(Tile target);
    void MoveXYSpaces(int x, int y);
    int Speed { get; set; }
}


public class CharacterMovement : MonoBehaviour, IMovementController {
    //static int tileSize = 1;

    Entity entity; // the entity this movement component belongs to
    bool moving = false;
    Tile destinationTile;
    Tile currentTile = null;
    int speed = 10;

    public Tile CurrentTile
    {
        get
        {
            return currentTile;
        }

        set
        {
            currentTile = value;
        }
    }

    public int Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    private void Start()
    {
       

    }

    public bool IsMoving()
    {
        return moving;
    }

    void Update()
    {
        if (moving && destinationTile != null)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destinationTile.GetWorldPos(), step);
            if (transform.position == destinationTile.GetWorldPos() /*|| entity.Stats.GetStat("Movement").finalValue() <= 0*/)
            {
                moving = false;
                currentTile = destinationTile;
                CurrentTile.Occupant = entity;

                //if(entity.GetComponent<ITurnHandler>().Combat == null)
                CombatManager.instance.CheckForCombat(entity);

            }
        }
    }

    public bool AtDestination()
    {
        if(currentTile == destinationTile)
        {
            return true;
        }

        return false;
    }

    public void MoveToTile(Tile target)
    {
        if (!target.IsWalkable())
            return;
        destinationTile = target;
        //check to see if where we want to be is where we already are
        if (AtDestination())
            return;

        CurrentTile.Occupant = null;
        if (!moving)
        {
            moving = true;
        }
    }

    public void MoveXYSpaces(int x, int y)
    {
        Tile temp = TileMapManager.Instance.GetTile(CurrentTile.TileX + x, CurrentTile.TileY + y);
        if (temp != null)
        {
            MoveToTile(temp);
        }
    }

    public void SetToTile(Tile target)
    {
        CurrentTile = target;
        transform.position = CurrentTile.GetWorldPos();
        //print("setting " + Entity + " to " + CurrentTile);
        CurrentTile.Occupant = entity;
    }


    public static CharacterMovement CreateComponent(GameObject where)
    {
        CharacterMovement temp = where.AddComponent<CharacterMovement>();
        temp.entity = where.GetComponent<Entity>();
        return temp;
    }

}
