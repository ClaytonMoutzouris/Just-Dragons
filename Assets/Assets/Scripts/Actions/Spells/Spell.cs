using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpellTargeting { Single, Area };

[System.Serializable]
public class Spell : ScriptableObject {

    [SerializeField]
    public int spellID;
    [SerializeField]
    public Sprite image;
    [SerializeField]
    public string spellName = string.Empty; // I always initialize variables.
    [SerializeField]
    public float mpCost = 0;

    
    public virtual bool Cast(Entity caster, List<Entity> Targets)
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

