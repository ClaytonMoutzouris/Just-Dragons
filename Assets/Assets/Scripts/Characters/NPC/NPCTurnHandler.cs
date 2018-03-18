using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTurnHandler :  MonoBehaviour, ITurnHandler
{

    public TurnState currentState = TurnState.Waiting;
    Entity entity;
    Combat combat;
    NonPlayerCharacter character;
    Entity target;
    bool hasMoved;

    public Combat Combat
    {
        get
        {
            return combat;
        }

        set
        {
            combat = value;
        }
    }
    public int Initiative
    {
        get
        {
            return initiative;
        }

        set
        {
            initiative = value;
        }
    }

    public Entity Entity
    {
        get
        {
            return entity;
        }

        set
        {
            entity = value;
        }
    }

    public Character Character
    {
        get
        {
            return character;
        }
    }

    public Entity Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    private int initiative;

    private void Start()
    {
        Entity = GetComponent<Entity>();
    
        character = GetComponent<NonPlayerCharacter>();
    }

    public void DeactivateTurnHandler()
    {
        Destroy(this);
    }

    public void HandleTurn()
    {
       

            switch (currentState)
            {
                case TurnState.Start:
                    //Set camera to character
                    //regain AP
                    //regain movement
                    //Any start of turn ability or effects

                    //Move on to action phase, where moving and selecting actions can happen
                    Camera.main.GetComponent<CameraController>().target = gameObject.transform;
                    hasMoved = false;
                    currentState = TurnState.Action;

                    break;

                case TurnState.Action:

                //yield WaitForSeconds (2);
                //GetComponent<EnemyAI>().MoveSomewhere();
                ActionPhase();
                                
                break;

                case TurnState.End:
                    //Clean up before we go on to the next turn
                    //end of turn effects happen, until end of turn effects resolve

                    currentState = TurnState.Waiting;
                    EndTurn();
                    break;
            }

        
    }
    //Action phase Coroutine
    //will atleast wait 1 second before ending the turn
    public void ActionPhase()
    {
        //The enemy must decide what to do
        //Decisions:
        //Is there a hostile in range?
        //If yes, we have to move close enough to attack
        //If we are done moving, are we close enough to attack?
        //If yes, attack
        //If no, end turn
        //This is a coroutine

        target = EntityActions.ChooseTarget(character);
        if (target != null && GetComponent<NonPlayerCharacter>().Hostility == Hostility.Hostile)
        {
            if(!EntityActions.TargetInRange(entity, target, 1))
            {
                if (!GetComponent<CharacterMovement>().IsMoving())
                EntityActions.MoveToHostile(Entity, target);

            } else
            {
                EntityActions.Attack(target);
                currentState = TurnState.End;
            }
        } else if(!GetComponent<CharacterMovement>().IsMoving())
        {
            currentState = TurnState.End;
        }

       // yield return new WaitForSeconds(1.0f);    //Wait one second
       /*
        if (!GetComponent<CharacterMovement>().IsMoving())
        {
            currentState = TurnState.End;

        }
        */
    }

    public void EndTurn()
    {
        combat.NextTurn();
    }

    public TurnState GetTurnState()
    {
        return currentState;
    }

    public void SetTurnState(TurnState state)
    {
        currentState = state;
    }
}
