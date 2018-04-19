using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputComponent {

    public virtual void Update(Character IEntity)
    {

    }

    public virtual void UpdateCombatInput(Character IEntity)
    {

    }

    }

public class PlayerInputComponent : InputComponent
{

    public override void Update(Character IEntity)
    {
        //Handle Orthogonal movement
        //Left
        if (IEntity.controller.combat != null && IEntity.controller.turnstate != TurnState.ActionPhase)
            return;

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            IEntity.Movement.MoveXYSpaces(IEntity, 1, 0);

        }

        //Right
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            IEntity.Movement.MoveXYSpaces(IEntity, -1, 0);
        }

        //Up
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            IEntity.Movement.MoveXYSpaces(IEntity, 0, 1);
        }

        //Down
        else if (Input.GetAxisRaw("Vertical") == -1)
        {

            IEntity.Movement.MoveXYSpaces(IEntity, 0, -1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Tile t = Cursor.instance.SelectedTile();
            if(t != null)
            {
                if(t.Occupant != null)
                {
                    t.Occupant.Select();
                    IEntity.controller.target = t.Occupant;
                    IEntity.Movement.MoveToEntity(IEntity, t.Occupant);
                } else
                {
                    IEntity.Movement.MoveToTile(IEntity, t);

                }
            }
        }

   }
    
    public override void UpdateCombatInput(Character IEntity)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Pressed");
            if (IEntity.controller.target != null)
            {
                IEntity.actions[0].Use(IEntity);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IEntity.controller.turnstate = TurnState.EndPhase;
        }
    }
    
}

public class CharacterAIInputComponent : InputComponent
{

    public override void Update(Character IEntity)
    {
        //if(IEntity.character.controller.combat == null)
        //IEntity.Movement.MoveXYSpaces(IEntity, Random.Range(0,3)-1, Random.Range(0, 3) - 1);
    }

    public override void UpdateCombatInput(Character IEntity)
    {
       
    }

}