using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyAIActions : MonoBehaviour {
    public bool hasMoved = false;
    public Entity target;
	// Update is called once per frame
	void Update () {
        

	}

    public void MoveSomewhere()
    {
        if (!hasMoved)
        {
            hasMoved = true;
            GetComponent<CharacterMovement>().MoveXYSpaces(Random.Range(-5, 5), Random.Range(-5, 5));
        }
    }

    public void MoveToHostile(Entity e)
    {
        if (!hasMoved)
        {
            hasMoved = true;
            GetComponent<CharacterMovement>().MoveToTile(TileMapManager.Instance.GetNearestNeighbour(GetComponent<CharacterMovement>().CurrentTile, e.GetComponent<CharacterMovement>().CurrentTile));
        }

    }

    public bool FindHostile()
    {
        
        

        return false;
    }

    public Entity ChooseTarget()
    {

        foreach (Tile t in TileMapManager.Instance.GetTilesInRange(GetComponent<CharacterMovement>().CurrentTile, GetComponent<Enemy>().SightRange))
        {
            if (t.Occupant != null && t.Occupant.GetComponent<Player>() != null)
            {
                return t.Occupant;
            }
        }

        return null;

    }
}
