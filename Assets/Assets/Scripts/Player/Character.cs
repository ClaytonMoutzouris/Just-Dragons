using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Sprite characterSprite;
    string name;
    int level;
    int health;
    int speed = 5;
    bool inCombat = false;
    bool activePlayer = true;
    Tile tile;

    public Tile Tile
    {
        get
        {
            return tile;
        }

        set
        {
            tile = value;
        }
    }

    private void Start()
    {
        gameObject.AddComponent(typeof(Moveable));
    }

    public void SetTile(Tile t)
    {
        Tile = t;
        //transform.SetPositionAndRotation(t.GetWorldPos(), Quaternion.identity);
        TileMapHandler.instance.CurrentMap.ChangeTile(t.TileX, t.TileY);
       
    }


    public void InitializeCharacter()
    {

    }

    private void Update()
    {

        HandleMovementInput();

}

    private void LateUpdate()
    {

        if (activePlayer)
        {
            //Camera.main.GetComponent<CameraController>().SetPosition(transform.position.x, transform.position.y);
        }
        }

    private void HandleMovementInput()
    {
        //Handle Movement
        if (!GetComponent<Moveable>().IsMoving())
        {
            //Left
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                Tile = TileMapHandler.instance.CurrentMap.tiles[Tile.TileX +1, Tile.TileY];

                Moveable m = GetComponent<Moveable>();
                m.MoveTo(Tile, speed);
            }

            //Right
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                Tile = TileMapHandler.instance.CurrentMap.tiles[Tile.TileX - 1, Tile.TileY];

                Moveable m = GetComponent<Moveable>();
                m.MoveTo(Tile, speed);
            }

            //Up
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                Tile = TileMapHandler.instance.CurrentMap.tiles[Tile.TileX, Tile.TileY + 1];

                Moveable m = GetComponent<Moveable>();
                m.MoveTo(Tile, speed);
            }


            //Down
            if (Input.GetAxisRaw("Vertical") == -1)
            {

                Tile = TileMapHandler.instance.CurrentMap.tiles[Tile.TileX, Tile.TileY - 1];

                Moveable m = GetComponent<Moveable>();
                m.MoveTo(Tile, speed);
            }
        }

    }

    private void HandleCombatInput()
    {


    }
}
