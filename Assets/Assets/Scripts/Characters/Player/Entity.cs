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
    //List<Component> components;
    //SpriteRenderer spriteRenderer;
    string name;
    Dictionary<string, Component> components;
    Stats stats;
    Tile currentTile;
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

    public Dictionary<string, Component> Components
    {
        get
        {
            return components;
        }

        set
        {
            components = value;
        }
    }

    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<MouseOverTooltip>();       
        gameObject.AddComponent<Selectable>();
        
        //name = "Object";


    }

    public void AddComponent(Type t)
    {
        Component c = gameObject.AddComponent(t);
        Components.Add(t.ToString(), c);
            }

    public void SetToTile(Tile target)
    {

        CurrentTile = target;
        transform.position = CurrentTile.GetWorldPos();
        //print("setting " + Entity + " to " + CurrentTile);
        CurrentTile.Occupant = this;
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
