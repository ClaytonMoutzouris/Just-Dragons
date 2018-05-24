using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hostility { Friendly, Neutral, Hostile };


public abstract class Character : IEntity {

    #region ENTITYFIELDS
    //IEntity variables
    private string entityName;
    DrawComponent graphics;
    MovementComponent movement;
    Tile currentTile;

    public string EntityName
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



    #endregion

   
    public List<Skill> actions;
    public CombatHandler controller;
    Stats stats;
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


    //Base character constructor, called by children
    public Character()
    {
        actions = new List<Skill>();
        actions.Add(new AttackAction());
        Stats = new Stats();
        
        Graphics = new DrawComponent();


    }

    //Update for character, which is always happening
    public virtual void Update()
    {
        Movement.Update();
        Graphics.Update();

    }

    public void Select()
    {
        
    }

    public void SetToTile(Tile target)
    {
        CurrentTile = target;
        Graphics.SetWorldPosition(CurrentTile.GetWorldPos());
        CurrentTile.Occupant = this;
    }

    public string GetTooltip()
    {
        return entityName + "\n" +
            stats.GetHealth().CurrentHealth + "/" + stats.GetHealth().MaxHealth;
    }
}

public class PlayerCharacter : Character
{

    Inventory inventory;
    InputComponent input;

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

    public PlayerCharacter() : base()
    {
        controller = new PlayerCombatHandler(this);
        input = new PlayerInputComponent();
        Movement = new PlayerMovementComponent(this);
        SetToTile(TileMapManager.Instance.CurrentMap.GetTile((int)TileMapManager.Instance.CurrentMap.mapSize.x / 2, (int)TileMapManager.Instance.CurrentMap.mapSize.y / 2));
        Stats.GetHealth().Initialise(this);
        CameraController.instance.target = Graphics.entity.transform;
        //IEntity.isSelected = true;
    }

    public override void Update()
    {
        input.Update(this);
        base.Update();
    }


}


public class NonPlayerCharacter : Character
{
    int sightRange = 10;
    NPCPrototype prototype;

    public NonPlayerCharacter(NPCPrototype proto) : base()
    {
        controller = new NPCCombatHandler(this);
        prototype = proto;
        EntityName = prototype.characterName;
        Movement = new NPCMovementComponent(this);
        SetToTile(TileMapManager.Instance.CurrentMap.GetTile(Random.Range(1,(int)TileMapManager.Instance.CurrentMap.mapSize.x), Random.Range(1, (int)TileMapManager.Instance.CurrentMap.mapSize.y)));
        Stats = proto.stats;
        Stats.GetHealth().Initialise(this);
        Graphics.sRenderer.sprite = proto.image;
        // loot = new LootableComponent(IEntity);



    }

    public override void Update()
    {
        base.Update();
    }
}