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

    public virtual void Use(IEntity caster)
    {

    }
    public virtual void Use(IEntity caster, List<IEntity> targets)
    {

    }

    public virtual bool CheckValid(IEntity caster)
    {
        return true;
    }
    
    public virtual void Select(IEntity caster)
    {

    }

    public virtual void Deselect(IEntity caster)
    {

    }

}

