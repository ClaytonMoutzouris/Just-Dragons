using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TurnState { Start, Action, End, Waiting }
public class PlayerTurnHandler : MonoBehaviour, ITurnHandler {
    public TurnState currentState = TurnState.Waiting;
    public List<string> flags;
    public Entity target;

    //we can probably make this into a list of flags for easiness
    public bool attackingFlag = false;
    public bool endTurnFlag = false;

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
                    currentState = TurnState.Action;
                    endTurnFlag = false;

                        break;

                case TurnState.Action:
                //Wait for player input
                //options include moving, casting spells, attacking, inspecting stuff, etc.
                //need to figure out how to get input here, as combining multiple things into one actions isnt working

                //while in action phase, await player input
                HandlePlayerInput();


                if (attackingFlag)
                {

                    if (!GetComponent<EntityActions>().TargetInRange(target, 1))
                    {
                        GetComponent<EntityActions>().MoveToHostile(target);

                    }
                    else
                    {
                        GetComponent<EntityActions>().Attack(target);
                        attackingFlag = false;

                    }


                }

                //If we flagged the end of the turn and are done attacking
                if(endTurnFlag && !attackingFlag)
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


    public void HandlePlayerInput()
    {
        //what happens when we press the submit button
        if (Input.GetButtonDown("Submit"))
        {
            //get the selected entity from game manager
            Entity target = GameManager.instance.selectedEntity;

            //is there a target?
            if (target != null)
            {
                //if yes now set this characters target to that
                this.target = target;

                //if the target is an enemy, we're going to attack it
                if (target.GetComponent<Enemy>() != null)
                attackingFlag = true;
            }

        }

        if (Input.GetButtonDown("Jump")){

            endTurnFlag = true;

        }

    }

    public void EndTurn()
    {
        //target = null;
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
