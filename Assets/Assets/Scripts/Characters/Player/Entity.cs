using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class OnEntitySelectedEventArgs : EventArgs
{
    public int x { get; set; }
    public int y { get; set; }
}

public class Entity : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    string name;
    Dictionary<string, Component> components;
    List<Component> componentsList;
    Stats stats;
    //public event EventHandler<OnMapClickedEventArgs> OnEntityClicked = (sender, e) => { };
    //public event EventHandler<OnTileChangedEventArgs> TileChanged = (sender, e) => { };
    //Stats entityStats;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Stats Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        gameObject.AddComponent<MouseOverTooltip>();       
        gameObject.AddComponent<Selectable>();
        Stats = gameObject.AddComponent<Stats>();



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

    public string GetTooltip()
    {
        string tooltip = "";
        tooltip += name;
        if(GetComponent<Health>() != null)
        {
            tooltip += "\n" + GetComponent<Health>().currentHealth + " / " + GetComponent<Health>().MaxHealth;
        }

        return tooltip;
    }

    public void SelectEntity()
    {
        
    }
}
