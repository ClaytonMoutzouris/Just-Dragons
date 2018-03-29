using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Targeting { Self, CurrentTarget, AutoSelect }; // et cetera for other settings.


//=== Individual Skills: =============================

[System.Serializable]
public abstract class Action : ScriptableObject
{
    [SerializeField]
    public int actionID;
    [SerializeField]
    public Sprite image;
    [SerializeField]
    public string skillName = string.Empty; // I always initialize variables.
    [SerializeField]
    public float mpCost = 0;
    [SerializeField]
    public Targeting targeting = Targeting.CurrentTarget;
   

    
    public virtual void Use(Entity user)
    { // Coroutine so it can do stuff over time.

        
    }
    
}

