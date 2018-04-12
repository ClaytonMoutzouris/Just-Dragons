using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { StartPhase, ActionPhase, SelectingTarget, Attacking, EndPhase, Waiting };

public abstract class CharacterCombatComponent {

    public TurnState turnstate;
    public Combat combat = null;
    public int initiative;
    public Entity target;
    public bool guard = false;
    public bool endTurnFlag = false;
    public Entity entity;

    public CharacterCombatComponent(Entity entity)
    {
        this.entity = entity;
    }

    public virtual void HandleTurn()
    {
        switch (turnstate)
        {
            case TurnState.StartPhase:
                StartPhase();
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
        guard = false;
        endTurnFlag = false;
        turnstate = TurnState.ActionPhase;
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
        if (Mathf.Abs(target.CurrentTile.TileX - entity.CurrentTile.TileX) <= range && Mathf.Abs(target.CurrentTile.TileY - entity.CurrentTile.TileY) <= range)
        {
            return true;
        }

        return false;
    }

    public void GetTarget()
    {
        List<Tile> tiles = TileMapManager.Instance.GetTilesInRange(entity.CurrentTile, 10);
        foreach (Tile t in tiles)
        {
            if (t.Occupant != null)
            {
                if (t.Occupant.character is PlayerCharacterComponent)
                {
                    target = t.Occupant;
                }
            }
        }
    }

}

public class PlayerCharacterCombatComponent: CharacterCombatComponent
{
    Spell spellToConfirm = null;

    public PlayerCharacterCombatComponent(Entity entity): base(entity)
    {

    }

    public override void StartPhase()
    {
        base.StartPhase();
        Cursor.instance.currentPlayer = entity;
        spellToConfirm = null;
    }

    public override void ActionPhase()
    {
        //get input for the turn, selecting skills and whatever
        entity.Input.UpdateCombatInput(entity);
        

    }

    public override void EndPhase()
    {
        base.EndPhase();
    }

}

public class NPCCharacterCombatComponent : CharacterCombatComponent
{
    bool hasMoved;
    int actionIndex;

    public NPCCharacterCombatComponent(Entity entity): base(entity)
    {

    }

    public override void StartPhase()
    {
        base.StartPhase();
        hasMoved = false;
        
    }

    public override void ActionPhase()
    {
        //get input for the turn, selecting skills and whatever
        //entity.Input.UpdateCombatInput(entity);
        
        GetTarget();
        if (target != null)
        {
            if (TargetInRange(1))
            {
                entity.character.actions[0].Use(entity);
                turnstate = TurnState.EndPhase;
            }
            else
            {
                entity.Movement.MoveToEntity(entity, target);
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