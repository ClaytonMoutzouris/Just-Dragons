using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    CharacterMovement movement;
    int sightRange = 10;

    public int SightRange
    {
        get
        {
            return sightRange;
        }

        set
        {
            sightRange = value;
        }
    }

    void Awake()
    {
        //characterData = gameObject.AddComponent<Entity>();
        movement = gameObject.AddComponent<CharacterMovement>();
        movement.Entity = GetComponent<Entity>();
        movement.SetToTile(TileMapManager.Instance.GetTile(Random.Range(0,50), Random.Range(0, 50)));
        gameObject.AddComponent<EnemyTurnHandler>();
        gameObject.layer = LayerMask.NameToLayer("Characters");

    }


    void Update()
    {
        //HandleMovementInput();

    }

}
