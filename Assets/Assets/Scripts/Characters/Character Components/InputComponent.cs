using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputComponent {

    public virtual void Update(Entity entity)
    {

    }

    public virtual void UpdateCombatInput(Entity entity)
    {

    }

    }

public class PlayerInputComponent : InputComponent
{

    public override void Update(Entity entity)
    {
        //Handle Orthogonal movement
        //Left
        if (entity.character.controller.combat != null && entity.character.controller.turnstate != TurnState.ActionPhase)
            return;

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            entity.Movement.MoveXYSpaces(entity, 1, 0);

        }

        //Right
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            entity.Movement.MoveXYSpaces(entity, -1, 0);
        }

        //Up
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            entity.Movement.MoveXYSpaces(entity, 0, 1);
        }

        //Down
        else if (Input.GetAxisRaw("Vertical") == -1)
        {

            entity.Movement.MoveXYSpaces(entity, 0, -1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Tile t = Cursor.instance.SelectedTile();
            if(t != null)
            {
                if(t.Occupant != null)
                {
                    t.Occupant.Select();
                    entity.character.controller.target = t.Occupant;
                    entity.Movement.MoveToEntity(entity, t.Occupant);
                } else
                {
                    entity.Movement.MoveToTile(entity, t);

                }
            }
        }

   }
    
    public override void UpdateCombatInput(Entity entity)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Pressed");
            if (entity.character.controller.target != null)
            {
                entity.character.actions[0].Use(entity);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.character.controller.turnstate = TurnState.EndPhase;
        }
    }
    
}

public class CharacterAIInputComponent : InputComponent
{

    public override void Update(Entity entity)
    {
        //if(entity.character.controller.combat == null)
        //entity.Movement.MoveXYSpaces(entity, Random.Range(0,3)-1, Random.Range(0, 3) - 1);
    }

    public override void UpdateCombatInput(Entity entity)
    {
       
    }

}