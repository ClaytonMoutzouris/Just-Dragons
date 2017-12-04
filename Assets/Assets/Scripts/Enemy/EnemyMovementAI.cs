using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyMovementAI : MonoBehaviour {
    public bool hasMoved = false;
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

    public void MoveToPlayer()
    {
        
    }
}
