using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Character : MonoBehaviour {

    protected Stats stats;
    protected bool activeCharacter = true;
    private NPCPrototype charData;
    protected List<Skill> actions;

    public NPCPrototype CharData
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

    public List<Skill> Actions
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
