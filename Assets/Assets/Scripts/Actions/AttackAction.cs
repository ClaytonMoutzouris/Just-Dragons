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

    public override void Use(Character user)
    {
        
        //Health target = user.controller.target;
        if (user.controller.target != null && !user.controller.target.Stats.GetHealth().IsDead)
        {
            Debug.Log("Attacking");

            user.controller.target.Stats.GetHealth().TakeDamage(Random.Range(1,3));
        } else
        {
            Debug.Log("Target dead or null");
        }

    
    }

}

