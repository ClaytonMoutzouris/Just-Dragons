using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpell : Spell
{
    
    [SerializeField]
    Vector2 DamageRange;
    

    public override void Use(IEntity caster, List<IEntity> Targets)
    {

        switch (targeting)
        {
            case SpellTargeting.Single:
                //Targets[0].character.GetComponent<Health>().TakeDamage((int)Random.Range(DamageRange.x, DamageRange.y));
                break;
            case SpellTargeting.Area:

                break;
        }
        //caster.character.GetComponent<ITurnHandler>().SetTurnState(TurnState.End);
        Deselect(caster);
    }

    public override void Select(IEntity caster)
    {
        //Check to see if the requirements are met
        //caster.character.controller.spellToConfirm = this;
    }

    public override void Deselect(IEntity caster)
    {
        //Check to see if the requirements are met
        //caster.character.GetComponent<PlayerTurnHandler>().spellToConfirm = null;
    }
}
