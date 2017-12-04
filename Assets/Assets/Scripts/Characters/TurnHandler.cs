using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TurnState { Start, Action, End, Waiting }
public class TurnHandler : MonoBehaviour {
    public TurnState currentState = TurnState.Waiting;

    public void HandleTurn()
    {
        if(gameObject.GetComponent<Player>() != null)
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

                        break;

                case TurnState.Action:
                    //Wait for player input
                    //options include moving, casting spells, attacking, inspecting stuff, etc.
                    if (Input.GetButton("Jump"))
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

        } else if(gameObject.GetComponent<Enemy>() != null)
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
                    GetComponent<EnemyMovementAI>().hasMoved = false;
                    currentState = TurnState.Action;

                    break;

                case TurnState.Action:
                    //Wait for player input
                    //options include moving, casting spells, attacking, inspecting stuff, etc.
                    GetComponent<EnemyMovementAI>().MoveSomewhere();

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
    }

    public void EndTurn()
    {
        GameManager.instance.NextTurn();
    }
}
