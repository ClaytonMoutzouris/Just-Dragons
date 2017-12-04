using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    //static int tileSize = 1;
    bool moving = false;
    Tile destinationTile;
    Tile currentTile;
    int speed = 5;

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

    void Initialize(int speed = 5)
    {
        this.speed = speed;
    }

    public bool IsMoving()
    {
        return moving;
    }

    void Update()
    {
        if (moving && destinationTile != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destinationTile.GetWorldPos(), step);
            if (transform.position == destinationTile.GetWorldPos())
            {
                moving = false;
                currentTile = destinationTile;
            }
        }
    }

    public void MoveToTile(Tile target)
    {
        destinationTile = target;
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
    }


}
