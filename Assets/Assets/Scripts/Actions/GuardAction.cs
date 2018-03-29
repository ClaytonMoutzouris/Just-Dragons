using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuardAction : Action
{



    public override void Use(Entity user)
    {
        Debug.Log("Guarding!");
        user.GetComponent<ITurnHandler>().Guard = true;

    }

}