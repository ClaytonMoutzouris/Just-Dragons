using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnHandler :  MonoBehaviour, ITurnHandler
{

    public TurnState currentState = TurnState.Waiting;

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
                    GetComponent<EntityActions>().hasMoved = false;
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

        Entity target = GetComponent<EntityActions>().ChooseTarget();
        if (target != null)
        {
            if(!GetComponent<EntityActions>().TargetInRange(target, 1))
            {
                GetComponent<EntityActions>().MoveToHostile(target);

            } else
            {
                GetComponent<EntityActions>().Attack(target);
                currentState = TurnState.End;
            }
        } else
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
        GameManager.instance.NextTurn();
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
