using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {
    static int tileSize = 1;
    bool moving = false;
    Tile target;
    int speed = 5;

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
                TileMapHandler.instance.CurrentMap.ChangeTile(target.TileX, target.TileY);

            }
        }
    }

    public void MoveTo(Tile target, int speed)
    {
        if (!moving)
        {
            moving = true;
            this.target = target;
            this.speed = speed;
        }
    }


    //Free movement controls for out of combat
	public void FreeMoveUp(int speed)
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + speed*Time.deltaTime);
    }

    public void FreeMoveDown(int speed)
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - speed * Time.deltaTime);

    }

    public void FreeMoveLeft(int speed)
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed * Time.deltaTime, gameObject.transform.position.y);

    }

    public void FreeMoveRight(int speed)
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - speed * Time.deltaTime, gameObject.transform.position.y);

    }

    //Tile movement for in combat
    public void TileMoveUp()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + tileSize);

    }

    //Tile movement for in combat
    public void TileMoveDown()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - tileSize);

    }

    //Tile movement for in combat
    public void TileMoveLeft()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - tileSize, gameObject.transform.position.y);

    }

    //Tile movement for in combat
    public void TileMoveRight()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + tileSize, gameObject.transform.position.y);

    }
}
