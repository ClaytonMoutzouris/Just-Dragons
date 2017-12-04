using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    bool activePlayer = true;
   // Entity characterData;
    CharacterMovement movement;
    // Use this for initialization
    void Awake () {
        //characterData = gameObject.AddComponent<Entity>();
        movement = gameObject.AddComponent<CharacterMovement>();
        gameObject.AddComponent<TurnHandler>();
        movement.SetToTile(TileMapManager.Instance.GetTile(25, 25));
    }

    // Update is called once per frame
    void Update () {
        HandleMovementInput();

    }

    private void HandleMovementInput()
    {
        if (GetComponent<TurnHandler>().currentState != TurnState.Action)
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
