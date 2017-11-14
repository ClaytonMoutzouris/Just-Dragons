using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {
    static int tileSize = 1;
    bool moving = false;
    Tile target;
    int speed = 5;

    void Start()
    {

    }

    public bool IsMoving()
    {
        return moving;
    }

    void Update()
    {
        if (moving && target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.GetWorldPos(), step);
            if (transform.position == target.GetWorldPos())
            {
                moving = false;
                //TileMapHandler.instance.CurrentMap.ChangeTile(target.TileX, target.TileY);
                if(target.GetExit() != null)
                {
                    TileMapHandler.instance.ChangeMap(target.GetExit());
                }
            }
        }
    }

    public void MoveToTile()
    {
        if (!moving)
        {
            moving = true;
        }
    }

    public void SetToTile()
    {
        transform.position = target.GetWorldPos();
    }

    public void CharacterTileChanged(Tile t)
    {
        target = t;
    }


}
