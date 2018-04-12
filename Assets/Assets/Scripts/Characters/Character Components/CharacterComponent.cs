using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hostility { Friendly, Neutral, Hostile };

public abstract class CharacterComponent {

    protected bool activeCharacter = false;
    public List<Skill> actions;
    public CharacterCombatComponent controller;
    public Entity entity;

    public CharacterComponent(Entity entity)
    {
        actions = new List<Skill>();
        actions.Add(new AttackAction());
        this.entity = entity;
    }

    public virtual void Update(Entity entity)
    {

    }

}

public class PlayerCharacterComponent : CharacterComponent
{

    Inventory inventory;

    public PlayerCharacterComponent(Entity entity) : base(entity)
    {
        //turnHandler = PlayerTurnHandler
        controller = new PlayerCharacterCombatComponent(entity);
        CameraController.instance.target = entity.Graphics.entity.transform;
        //entity.isSelected = true;
    }

    public override void Update(Entity entity)
    {

    }
}


public class NPCCharacterComponent : CharacterComponent
{
    int sightRange = 10;
    NPCPrototype prototype;

    public NPCCharacterComponent(Entity entity, NPCPrototype proto) : base(entity)
    {
        controller = new NPCCharacterCombatComponent(entity);
        prototype = proto;
        entity.Name = prototype.characterName;
        entity.Graphics.sRenderer.sprite = proto.image;

    }

    public override void Update(Entity entity)
    {

    }
}