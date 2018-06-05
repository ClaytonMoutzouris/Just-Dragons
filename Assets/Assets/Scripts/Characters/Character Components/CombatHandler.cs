using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { StartPhase, MovementPhase, ActionPhase, SelectingTarget, Attacking, EndPhase, Waiting };

public abstract class CombatHandler {

    public TurnState turnstate;
    public Combat combat = null;
    public int initiative;
    public IEntity target;
    public bool guard = false;
    public bool endTurnFlag = false;
    public Character character;
    public bool hasMoved = false;

    public CombatHandler()
    {
    }

    public virtual void HandleTurn()
    {

        switch (turnstate)
        {
            case TurnState.StartPhase:
                StartPhase();
                break;
            case TurnState.MovementPhase:
                MovementPhase();
                break;
            case TurnState.ActionPhase:
                ActionPhase();
                break;
            case TurnState.EndPhase:
                EndPhase();
                break;
        }
    }

    public virtual void EnterCombat()
    {

    }

    public virtual void StartPhase()
    {
        TextLog.instance.AddEntry(character.EntityName + " turn start!");
        guard = false;
        endTurnFlag = false;
        turnstate = TurnState.ActionPhase;
        hasMoved = false;
    }

    public virtual void MovementPhase()
    {
        Debug.Log(character.EntityName + " path empty " + character.Movement.PathEmpty());
        if (character.Movement.PathEmpty())
        {
            turnstate = TurnState.ActionPhase;
        }
    }

    public virtual void ActionPhase()
    {

    }

    public virtual void EndPhase()
    {
        turnstate = TurnState.Waiting;
        combat.NextTurn();
    }

    public bool TargetInRange(int range)
    {
        Debug.Log("X Range: " + Mathf.Abs(target.CurrentTile.TileX - character.CurrentTile.TileX) + ", Y Range: " + Mathf.Abs(target.CurrentTile.TileY - character.CurrentTile.TileY));
        if (Mathf.Abs(target.CurrentTile.TileX - character.CurrentTile.TileX) <= range && Mathf.Abs(target.CurrentTile.TileY - character.CurrentTile.TileY) <= range)
        {
            return true;
        }

        return false;
    }

    public void GetTarget()
    {
        List<Tile> tiles = TileMapManager.Instance.GetTilesInRange(character.CurrentTile, 10);
        foreach (Tile t in tiles)
        {
            if (t.Occupant != null)
            {
                if (t.Occupant is PlayerCharacter)
                {
                    target = t.Occupant;
                }
            }
        }
    }

}

public class PlayerCombatHandler: CombatHandler
{
    Spell spellToConfirm = null;

    public PlayerCombatHandler(PlayerCharacter IEntity)
    {
        character = IEntity;
    }

    public override void StartPhase()
    {
        base.StartPhase();
        Cursor.instance.currentPlayer = character;
        spellToConfirm = null;
        
    }

    public override void MovementPhase()
    {

        base.MovementPhase();

    }

    public override void ActionPhase()
    {
        //get input for the turn, selecting skills and whatever
        ((PlayerCharacter)character).Input.UpdateCombatInput(character);
        

    }

    public override void EndPhase()
    {
        base.EndPhase();
    }

}

public class NPCCombatHandler : CombatHandler
{
    int actionIndex;

    public NPCCombatHandler(Character IEntity)
    {
        character = IEntity;

    }

    public override void StartPhase()
    {
        base.StartPhase();
        hasMoved = false;
        
    }


    public override void MovementPhase()
    {
        if (!hasMoved)
        {
            character.Movement.MoveToEntity(target);
            hasMoved = true;

            if (character.Movement.PathEmpty())
            {
                turnstate = TurnState.EndPhase;
                return;
            }
        }

        base.MovementPhase();


    }


    public override void ActionPhase()
    {
        //get input for the turn, selecting skills and whatever
        //IEntity.Input.UpdateCombatInput(IEntity);
        
        GetTarget();
        if (target != null)
        {
            
            if (TargetInRange(1))
            {
                character.actions[0].Use(character);
                turnstate = TurnState.EndPhase;
            }
            else
            {
                if (!hasMoved)
                    turnstate = TurnState.MovementPhase;
                else
                    turnstate = TurnState.EndPhase;
            }
        } else
        {
            turnstate = TurnState.EndPhase;
        }
        

    }

    public override void EndPhase()
    {
        base.EndPhase();
    }
}