using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public interface ISelectable
{
    void Select();
    void Deselect();
}


public class Entity {
    //List<Component> components;
    //SpriteRenderer spriteRenderer;
    [SerializeField]
    string entityName;

    [SerializeField]
    Stats stats;

    [SerializeField]
    Tile currentTile;
    public Vector3 worldPosition;

    [SerializeField]
    MovementComponent movement;
    [SerializeField]
    InputComponent input;
    [SerializeField]
    DrawComponent graphics;

    public bool isSelected = false;
    //public event EventHandler<OnMapClickedEventArgs> OnEntityClicked = (sender, e) => { };
    //public event EventHandler<OnTileChangedEventArgs> TileChanged = (sender, e) => { };
    //Stats entityStats;

    public string Name
    {
        get
        {
            return entityName;
        }

        set
        {
            entityName = value;
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

    public MovementComponent Movement
    {
        get
        {
            return movement;
        }

        set
        {
            movement = value;
        }
    }

    public InputComponent Input
    {
        get
        {
            return input;
        }

        set
        {
            input = value;
        }
    }

    public DrawComponent Graphics
    {
        get
        {
            return graphics;
        }

        set
        {
            graphics = value;
        }
    }

    public CharacterComponent character;

    public Entity(InputComponent i, MovementComponent m)
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //gameObject.AddComponent<MouseOverTooltip>();
        Input = i;
        Movement = m;
        Graphics = new DrawComponent(this);
        movement.SetToTile(this, TileMapManager.Instance.GetTile(UnityEngine.Random.Range(0, 48), UnityEngine.Random.Range(0, 48)));
        stats = new Stats(this);

        

    }

    public void Select()
    {
        isSelected = !isSelected;
        if (isSelected)
            GameManager.instance.currentSelected = this;
        else
            GameManager.instance.currentSelected = null;
    }


    public void SetToTile(Tile target)
    {

        CurrentTile = target;
        //Graphics.entity.transform.position = CurrentTile.GetWorldPos();
        //print("setting " + Entity + " to " + CurrentTile);
        CurrentTile.Occupant = this;
    }

    public void TileMapChanged(Exit e, ITileMapModel map)
    {
        //SetTile(map.tiles[e.Destination_X, e.Destination_Y], true);
    }



    public void Update()
    {
        Input.Update(this);
        Movement.Update(this);
        Graphics.Update(this);

    }

    private void LateUpdate()
    {

    }

    public string GetTooltip()
    {
        string tooltip = "";
        tooltip += entityName;
        
        
        tooltip += "\n" + Stats.GetHealth().currentHealth + " / " + Stats.GetHealth().MaxHealth;
        
        
        return tooltip;
    }
}
