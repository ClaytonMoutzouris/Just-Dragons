using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    protected IMovementController movement;
    protected Entity entity;
    protected Stats stats;
    protected bool activePlayer = true;


}
