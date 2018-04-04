using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        caster.GetComponent<ITurnHandler>().SetTurnState(TurnState.End);
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
