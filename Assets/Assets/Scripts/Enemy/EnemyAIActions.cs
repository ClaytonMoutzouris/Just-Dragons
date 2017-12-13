using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//consider making this a static class and giving each entity a list of actions they can potentially perform
//this does not work with components so a solution would need to be found
[RequireComponent(typeof(CharacterMovement))]
public class EnemyAIActions : MonoBehaviour {
    public bool hasMoved = false;
    public Entity target;
    public bool actionInProgress;



    public void MoveToHostile(Entity e)
    {
        CharacterMovement movement = GetComponent<CharacterMovement>();

        
        movement.MoveToTile(TileMapManager.Instance.GetNearestNeighbour(movement.CurrentTile, e.GetComponent<CharacterMovement>().CurrentTile));

        

    }

    public bool TargetInRange(Entity target, int range)
    {
        Debug.Log("Checking");
        Debug.Log(GetComponent<Entity>().Name + " X: " + GetComponent<CharacterMovement>().CurrentTile.TileX + target.Name + " X:" + target.GetComponent<CharacterMovement>().CurrentTile.TileX);
        Debug.Log(GetComponent<Entity>().Name + " Y: " + GetComponent<CharacterMovement>().CurrentTile.TileY + target.Name + " Y:" + target.GetComponent<CharacterMovement>().CurrentTile.TileY);


        if (Mathf.Abs(GetComponent<CharacterMovement>().CurrentTile.TileX - target.GetComponent<CharacterMovement>().CurrentTile.TileX) <= range && Mathf.Abs(GetComponent<CharacterMovement>().CurrentTile.TileY - target.GetComponent<CharacterMovement>().CurrentTile.TileY) <= range)
        {
            return true;
        }

        return false;
    }

    public void Attack(Entity target)
    {
        Debug.Log("Attacking " + target);
        Health healthComponent = target.GetComponent<Health>();
        if(healthComponent != null)
        {
            healthComponent.TakeDamage(5);
        }

    }

    public List<Entity> FindHostiles()
    {
        List<Entity> hostiles = new List<Entity>();
        foreach (Tile t in TileMapManager.Instance.GetTilesInRange(GetComponent<CharacterMovement>().CurrentTile, GetComponent<Enemy>().SightRange))
        {
            if (t.Occupant != null && t.Occupant.GetComponent<Player>() != null)
            {
                hostiles.Add(t.Occupant);
            }
        }

        return hostiles;
    }

    public Entity ChooseTarget()
    {
        List<Entity> possibleTargets = FindHostiles();

        if (possibleTargets.Count > 0)
            return FindHostiles()[0];
        else
            return null;

    }
}
