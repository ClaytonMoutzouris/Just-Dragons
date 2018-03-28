using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class PlayerCharacter : Character {

    
   // Entity characterData;
    List<Action> actions;
    Inventory inventory;

    public List<Action> Actions
    {
        get
        {
            return actions;
        }

        set
        {
            actions = value;
        }
    }

    public static PlayerCharacter CreateComponent (GameObject where)
    {
        PlayerCharacter temp = where.AddComponent<PlayerCharacter>();
        temp.movement = CharacterMovement.CreateComponent(where);

        temp.gameObject.AddComponent<PlayerTurnHandler>();
        temp.movement.SetToTile(TileMapManager.Instance.GetTile(25, 25));
        temp.gameObject.layer = LayerMask.NameToLayer("Characters");
        temp.Actions = new List<Action>();
        temp.inventory = Inventory.CreateComponent(where);
        temp.Hostility = Hostility.Friendly;
        temp.Portrait = temp.GetComponent<SpriteRenderer>().sprite;

        print("looking for actions");
        foreach (Action a in Resources.LoadAll<Action>("Actions"))
        {
            print("Loading an action");
            if (a != null)
            {
                temp.Actions.Add(a);
            }

        }
        temp.stats = Stats.CreateComponent(where);
        temp.GetComponent<Health>().Initialise(50);

        //print(UIManager.Instance);
        //
        return temp;
    }


    // Update is called once per frame
    void Update () {
        HandleMovementInput();

    }

    private void HandleMovementInput()
    {
        if (GetComponent<PlayerTurnHandler>().currentState != TurnState.Action)
            return;
        //Handle Keyboard Movement
        if (!movement.IsMoving())
        {
            //Handle Orthogonal movement
            //Left
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                movement.MoveXYSpaces(1, 0);

            }

            //Right
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                movement.MoveXYSpaces(-1, 0);

            }

            //Up
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                movement.MoveXYSpaces(0, 1);

            }

            //Down
            if (Input.GetAxisRaw("Vertical") == -1)
            {

                movement.MoveXYSpaces(0, -1);
            }
        }



    }


}
