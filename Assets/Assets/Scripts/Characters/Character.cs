using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Character : MonoBehaviour {
    protected IMovementController movement;
    protected Entity entity;
    protected Stats stats;
    protected bool activePlayer = true;
    private CharacterData charData;
    protected List<Action> actions;

    public CharacterData CharData
    {
        get
        {
            return charData;
        }

        set
        {
            charData = value;
        }
    }

    public Hostility Hostility
    {
        get
        {
            return hostility;
        }

        set
        {
            hostility = value;
        }
    }

    public Sprite Portrait
    {
        get
        {
            return portrait;
        }

        set
        {
            portrait = value;
        }
    }

    public List<Action> Actions
    {
        get
        {
            return actions;
        }

        set
        {
            actions = value;
        }
    }

    private Hostility hostility;

    protected Sprite portrait;

}
