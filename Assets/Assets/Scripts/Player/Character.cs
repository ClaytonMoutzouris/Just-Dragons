using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnTileChangedEventArgs : EventArgs
{
    public Tile tile;
}

public class Character : MonoBehaviour {

    public Sprite characterSprite;
    //string name;
    int level;
    int health;
    int speed = 5;
    bool inCombat = false;
    bool activePlayer = true;
    Tile tile;
    //public event EventHandler<OnTileChangedEventArgs> TileChanged = (sender, e) => { };
    public delegate void OnTileChanged (Tile t);
    public event OnTileChanged TileChanged;

    public Tile Tile
    {
        get
        {
            return tile;
        }

        set
        {
            Tile old = tile;
            if (old != value)
            {
                tile = value;
                TileChanged(tile);
            }
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

    private void OnEnable()
    {
        DelegatesAndEvents.MapChanged += TileMapChanged;
    }

    public void CharacterTileChanged(Tile t)
    {
        // does this delegate have at least one subscriber? 
        if (TileChanged != null)
        {
            TileChanged(t);
        }
    }

    private void Start()
    {
        //TileMapHandler.instance.MapChanged  += OnMapChanged;

        Moveable m = gameObject.AddComponent(typeof(Moveable)) as Moveable;
        TileChanged += m.CharacterTileChanged;
        SetTile(TileMapHandler.instance.CurrentMap.tiles[25, 25]);
        transform.SetPositionAndRotation(Tile.GetWorldPos(), Quaternion.identity);

    }

    public void TileMapChanged(Exit e, ITileMapModel map)
    {
        SetTile(map.tiles[e.Destination_X, e.Destination_Y], true);
    }



    public void SetTile(Tile t, bool directSet = false)
    {
        if (t.GetMovementCost() == 0)
            return;

        Tile = t;
        Moveable m = GetComponent<Moveable>();

        if (directSet)
        {
            m.SetToTile();
        } else
        {
            m.MoveToTile();
        }
        
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
            //Handle Orthogonal movement
            //Left
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                SetTile(TileMapHandler.instance.CurrentMap.tiles[Tile.TileX +1, Tile.TileY]);

            }

            //Right
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                SetTile(TileMapHandler.instance.CurrentMap.tiles[Tile.TileX - 1, Tile.TileY]);

            }

            //Up
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                SetTile(TileMapHandler.instance.CurrentMap.tiles[Tile.TileX, Tile.TileY + 1]);

            }


            //Down
            if (Input.GetAxisRaw("Vertical") == -1)
            {

                SetTile(TileMapHandler.instance.CurrentMap.tiles[Tile.TileX, Tile.TileY - 1]);

            }
        }

    }

    private void HandleCombatInput()
    {


    }
}
