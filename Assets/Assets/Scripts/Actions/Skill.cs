using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Targeting { Single, Area, Self };


//=== Individual Skills: =============================
public interface IAction
{
    void Use(Entity user);
    void Use(Entity user, List<Entity> targets);
    bool CheckValid(Entity user);
    void Select(Entity user);
    void Deselect(Entity user);
    string ActionName { get; set; }
}

[System.Serializable]
public abstract class Skill
{
    public int actionID;
    public Sprite image;
    private string skillName = string.Empty; // I always initialize variables.
    public float mpCost = 0;
    public Targeting targeting = Targeting.Single;

    public string ActionName
    {
        get
        {
            return skillName;
        }

        set
        {
            skillName = value;
        }
    }

    public Skill()
    {

    }

    public virtual void Use(Entity user)
    { // Coroutine so it can do stuff over time.

        
    }

    public virtual bool CheckValid(Entity user)
    {
        return true;
    }

    public virtual void Select(Entity user)
    {

    }

    public virtual void Deselect(Entity user)
    {

    }


}

