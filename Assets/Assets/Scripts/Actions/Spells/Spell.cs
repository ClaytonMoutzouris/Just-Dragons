using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpellTargeting { Single, Area, Self };

[System.Serializable]
public class Spell : ScriptableObject, IAction {

    [SerializeField]
    public int spellID;
    [SerializeField]
    public Sprite image;
    [SerializeField]
    private string spellName = string.Empty; // I always initialize variables.
    [SerializeField]
    public float mpCost = 0;
    [SerializeField]
    protected SpellTargeting targeting;
    [SerializeField]
    protected int range;

    public string ActionName
    {
        get
        {
            return spellName;
        }

        set
        {
            spellName = value;
        }
    }

    public virtual void Use(Entity caster)
    {

    }
    public virtual void Use(Entity caster, List<Entity> targets)
    {

    }

    public virtual bool CheckValid(Entity caster)
    {
        return true;
    }
    
    public virtual void Select(Entity caster)
    {

    }

    public virtual void Deselect(Entity caster)
    {

    }

}

