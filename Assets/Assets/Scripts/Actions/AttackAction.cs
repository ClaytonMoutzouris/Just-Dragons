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
        ITurnHandler turnhandler = user.GetComponent<ITurnHandler>();
        if (turnhandler.Target != null && !turnhandler.Target.GetComponent<Health>().isDead)
        {

            if (!EntityActions.TargetInRange(user, turnhandler.Target, 1))
            {
                Debug.Log("Target not in range");

                EntityActions.MoveToEntity(user, turnhandler.Target);

            }
            else
            {
                Debug.Log("Attacking target");

                EntityActions.Attack(user.GetComponent<ITurnHandler>().Target);
                user.GetComponent<ITurnHandler>().SetTurnState(TurnState.End);
            }
        } else
        {
            Debug.Log("Target dead or null");
        }


    }

}

