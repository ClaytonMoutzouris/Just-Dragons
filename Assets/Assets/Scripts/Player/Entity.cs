using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;


public class Entity : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    //public event EventHandler<OnTileChangedEventArgs> TileChanged = (sender, e) => { };
    Stats entityStats;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        entityStats = gameObject.AddComponent<Stats>();
        //Dictionary<string, int> entityStats = new Dictionary<string, int>();
        //TileMapHandler.instance.MapChanged  += OnMapChanged;
    }

    public void TileMapChanged(Exit e, ITileMapModel map)
    {
        //SetTile(map.tiles[e.Destination_X, e.Destination_Y], true);
    }


    public void InitializeCharacter()
    {

    }


    private void Update()
    {

        //HandleMovementInput();

}

    private void LateUpdate()
    {

    }

    

    private void HandleCombatInput()
    {


    }
}
