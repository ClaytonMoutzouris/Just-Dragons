using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TurnState { Start, Action, End, Waiting }
public class PlayerTurnHandler : MonoBehaviour, ITurnHandler {
    public TurnState currentState = TurnState.Waiting;
    public List<string> flags;
    private Entity target;
    //we can probably make this into a list of flags for easiness
    public bool attackingFlag = false;
    public bool endTurnFlag = false;
    private bool guard = false;
    PlayerCharacter character;
    Entity entity;
    Combat combat;
    public Spell spellToConfirm = null;

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

    public bool Guard
    {
        get
        {
            return guard;
        }

        set
        {
            guard = value;
        }
    }

    private int initiative;
    private void Start()
    {
        Entity = GetComponent<Entity>();
        character = GetComponent<PlayerCharacter>();
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



                break;

                case TurnState.Action:
                //Wait for player input
                //options include moving, casting spells, attacking, inspecting stuff, etc.
                //need to figure out how to get input here, as combining multiple things into one actions isnt working

                //get the selected entity from game manager
                if (Selectable.currentSelected != null)
                {
                    target = Selectable.currentSelected.GetComponent<Entity>();
                }

                

                if (spellToConfirm != null)
                {
                    print("Waiting to confirm " + spellToConfirm);
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        spellToConfirm.Deselect(entity);
                    } 

                    //We have a spell at the ready, just waiting on a target

                } else
                {
                    //while in action phase, await player input
                    HandlePlayerInput();
                }
                


                

                //If we flagged the end of the turn and are done attacking
                if(endTurnFlag)
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

        

        if (Input.GetButtonDown("Jump")){

            endTurnFlag = true;

        }

        //Use a potion with Left Control, for some reason

        if (Input.GetButtonDown("Fire1"))
        {

            Potion p = (Potion)ItemDatabase.GetItem(1);
            p.Use(Entity);

        }

        if (Input.GetButtonDown("Fire2"))
        {

            Potion p = (Potion)ItemDatabase.GetItem(4);
            p.Use(Entity);

        }

        for (int i = 0; i < 3; ++i)
        {
            if (Input.GetKeyDown("" + (i +1)))
            {
                if (character.ActionList[i] != null)
                    character.ActionList[i].Use(entity);
            }
        }


    }

    public void SetTarget()
    {
        target = Selectable.currentSelected.GetComponent<Entity>();
    }

    void StartTurn()
    {
        Camera.main.GetComponent<CameraController>().target = gameObject.transform;

        if(target != null)
        {
           target.GetComponent<Selectable>().Select2();
        }

        guard = false;
        currentState = TurnState.Action;
        //Populate skills bar, set the UI for the current player character

    }

    void EndTurn()
    {
        //target = null;
        if(Selectable.currentSelected != null)
        Selectable.currentSelected.Deselect();

        //GetComponent<CharacterMovement>().
        endTurnFlag = false;

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
