﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPotion : Potion {
    [SerializeField]
    List<Sprite> sprites;

    public override void Use(IEntity user)
    {
        base.Use(user);

        user.Graphics.sRenderer.sprite = sprites[Random.Range(0, sprites.Count - 1)];
    }

    public override bool CheckValid(IEntity user)
    {
        return true;

    }

}
