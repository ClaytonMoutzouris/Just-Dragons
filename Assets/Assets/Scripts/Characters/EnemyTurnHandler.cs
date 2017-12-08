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
                    GetComponent<EnemyAIActions>().hasMoved = false;
                    currentState = TurnState.Action;

                    break;

                case TurnState.Action:
                //The enemy must decide what to do
                //Decisions:
                //Is there a hostile in range?
                //If yes, we have to move close enough to attack
                //If we are done moving, are we close enough to attack?
                //If yes, attack
                //If no, end turn

                //GetComponent<EnemyAI>().MoveSomewhere();
                Entity target = GetComponent<EnemyAIActions>().ChooseTarget();
                    if (target != null){
                    GetComponent<EnemyAIActions>().MoveToHostile(target);

                    }

                    if (!GetComponent<CharacterMovement>().IsMoving())
                    {
                        currentState = TurnState.End;

                    }
                    break;

                case TurnState.End:
                    //Clean up before we go on to the next turn
                    //end of turn effects happen, until end of turn effects resolve

                    currentState = TurnState.Waiting;
                    EndTurn();
                    break;
            }

        
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
