using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAction : Skill
{
    public Spell spell;
    public bool awaitingConfirm = false;

    public SpellAction(Spell spell)
    {
        this.spell = spell;
        image = spell.image;
    }

    public override void Use(Character user)
    {
            spell.Select(user);
            Cursor.instance.cursorState = CursorState.ConfirmTarget;

    }

}