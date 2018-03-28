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

        user.GetComponent<Health>().GainLife(hpValue);
    }

    public override bool CheckValid(Entity user)
    {
        if (user.GetComponent<Health>().IsHealthMax())
        {
            return false;
        } else
        {
            return true;
        }

    }
}