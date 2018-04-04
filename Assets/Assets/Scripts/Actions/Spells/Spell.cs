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

public class DamageSpell : Spell
{
    [SerializeField]
    SpellTargeting targeting;
    [SerializeField]
    Vector2 DamageRange;
    [SerializeField]
    int range;

    public override bool Cast(Entity caster, List<Entity> Targets)
    {

        switch (targeting)
        {
            case SpellTargeting.Single:
                Targets[0].GetComponent<Health>().TakeDamage((int)Random.Range(DamageRange.x, DamageRange.y));
                    break;
            case SpellTargeting.Area:

                break;
        }

        Deselect(caster);
        return true;
    }

    public override void Select(Entity caster)
    {
        //Check to see if the requirements are met
        caster.GetComponent<PlayerTurnHandler>().spellToConfirm = this;
    }

    public override void Deselect(Entity caster)
    {
        //Check to see if the requirements are met
        caster.GetComponent<PlayerTurnHandler>().spellToConfirm = null;
    }
}