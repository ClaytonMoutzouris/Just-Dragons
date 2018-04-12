using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackAction : Skill
{
    
    public AttackAction()
    {
        image = Resources.Load<Sprite>("Textures and Sprites/SwordSprite_1");
    }

    public override void Use(Entity user)
    {
        CharacterCombatComponent turnhandler = user.character.controller;
        if (turnhandler.target != null && !turnhandler.target.Stats.GetHealth().isDead)
        {
            Debug.Log("Attacking");

            turnhandler.target.Stats.GetHealth().TakeDamage(Random.Range(0,2));
        } else
        {
            Debug.Log("Target dead or null");
        }


    }

}

