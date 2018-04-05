using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuardAction : Skill
{

    public GuardAction()
    {
        image = Resources.Load<Sprite>("Textures and Sprites/SwordSprite_1");
    }

    public override void Use(Entity user)
    {
        Debug.Log("Guarding!");
        user.GetComponent<ITurnHandler>().Guard = true;

    }

}