using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellAction : Action
{
    [SerializeField]
    public Spell spell;


    public override void Use(Entity user)
    {
        spell.Select(user);

    }

}