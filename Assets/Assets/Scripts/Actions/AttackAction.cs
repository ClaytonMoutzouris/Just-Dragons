using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackAction : Action
{



    public override void Use(Entity user)
    {
        if (user.GetComponent<ITurnHandler>().Target != null)
        {
            EntityActions.Attack(user.GetComponent<ITurnHandler>().Target);
        }
        else
        {
            Debug.Log("No Target");
        }

    }

}

