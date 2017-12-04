using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    CharacterMovement movement;

    void Awake()
    {
        //characterData = gameObject.AddComponent<Entity>();
        movement = gameObject.AddComponent<CharacterMovement>();
        movement.SetToTile(TileMapManager.Instance.GetTile(Random.Range(0,50), Random.Range(0, 50)));
        gameObject.AddComponent<EnemyMovementAI>();
        gameObject.AddComponent<TurnHandler>();

    }


    void Update()
    {
        //HandleMovementInput();

    }

}
