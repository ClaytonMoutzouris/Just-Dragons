using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    [SerializeField]
    int hpValue;

    public override void Use(Entity user)
    {
        base.Use(user);

        user.Stats.GetHealth().GainLife(hpValue);
    }

    public override bool CheckValid(Entity user)
    {
        if (user.Stats.GetHealth().IsHealthMax())
        {
            return false;
        } else
        {
            return true;
        }

    }
}