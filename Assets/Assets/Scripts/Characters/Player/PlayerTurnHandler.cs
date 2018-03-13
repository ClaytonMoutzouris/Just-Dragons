using System;
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

    Entity entity;
    EntityActions actions;
    Combat combat;

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

    private int initiative;
    private void Start()
    {
        entity = GetComponent<Entity>();
        actions = gameObject.AddComponent<EntityActions>();
        Camera.main.GetComponent<CameraController>().target = gameObject.transform;
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
                StartTurn();
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

                    if (!actions.TargetInRange(target, 1))
                    {
                        actions.MoveToHostile(target);

                    }
                    else
                    {
                        actions.Attack(target);
                        attackingFlag = false;
                        endTurnFlag = true;
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
            Entity target = null;
            //get the selected entity from game manager
            if (Selectable.currentSelected != null)
            {
               target = Selectable.currentSelected.GetComponent<Entity>();
            }

            //is there a target?
            if (target != null)
            {
                SetTarget(target);
                //if yes now set this characters target to that
                
            }

        }

        if (Input.GetButtonDown("Jump")){

            endTurnFlag = true;

        }

    }

    public void SetTarget(Entity e)
    {
        target = e;

        //if the target is an enemy, we're going to attack it
        if (target.GetComponent<NonPlayerCharacter>() != null)
            attackingFlag = true;
    }

    void StartTurn()
    {
        Camera.main.GetComponent<CameraController>().target = gameObject.transform;

        //Populate skills bar, set the UI for the current player character

    }

    void EndTurn()
    {
        //target = null;
        if(Selectable.currentSelected != null)
        Selectable.currentSelected.Deselect();
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
