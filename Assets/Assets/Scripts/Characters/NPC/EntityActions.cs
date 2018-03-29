﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//consider making this a static class and giving each entity a list of actions they can potentially perform
//this does not work with components so a solution would need to be found

public static class EntityActions {

    public static void MoveToEntity(Entity mover, Entity target)
    {
        

        
        mover.GetComponent<CharacterMovement>().MoveToTile(TileMapManager.Instance.GetNearestNeighbour(mover.GetComponent<CharacterMovement>().CurrentTile, target.GetComponent<CharacterMovement>().GetComponent<CharacterMovement>().CurrentTile));

        

    }

    public static bool TargetInRange(Entity looker, Entity target, int range)
    {



        if (Mathf.Abs(looker.GetComponent<CharacterMovement>().CurrentTile.TileX - target.GetComponent<CharacterMovement>().CurrentTile.TileX) <= range && Mathf.Abs(looker.GetComponent<CharacterMovement>().CurrentTile.TileY - target.GetComponent<CharacterMovement>().CurrentTile.TileY) <= range)
        {
            return true;
        }

        return false;
    }

    public static void Attack(Entity target)
    {
        //Debug.Log("Attacking " + target);
        Health healthComponent = target.GetComponent<Health>();
        if(healthComponent != null && !target.GetComponent<ITurnHandler>().Guard)
        {
            healthComponent.TakeDamage(Random.Range(2, 8));
            if (healthComponent.isDead)
            {
                target = null;
            }
        }

    }

    public static List<Entity> FindHostiles(Character e)
    {
        List<Entity> hostiles = new List<Entity>();
        foreach (Tile t in TileMapManager.Instance.GetTilesInRange(e.GetComponent<CharacterMovement>().CurrentTile, e.GetComponent<NonPlayerCharacter>().SightRange))
        {
            if (t.Occupant != null && t.Occupant.GetComponent<PlayerCharacter>() != null)
            {
                hostiles.Add(t.Occupant);
            }
        }

        return hostiles;
    }

    public static Entity ChooseTarget(Character e)
    {
        List<Entity> possibleTargets = FindHostiles(e);

        if (possibleTargets.Count > 0)
            return possibleTargets[0];
        else
            return null;

    }
}
