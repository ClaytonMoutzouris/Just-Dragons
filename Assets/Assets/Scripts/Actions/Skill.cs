using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Targeting { Single, Area, Self };


//=== Individual Skills: =============================
public interface IAction
{
    void Use(IEntity user);
    void Use(IEntity user, List<IEntity> targets);
    bool CheckValid(IEntity user);
    void Select(IEntity user);
    void Deselect(IEntity user);
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

    public virtual void Use(Character user)
    { // Coroutine so it can do stuff over time.

        
    }

    public virtual bool CheckValid(Character user)
    {
        return true;
    }

    public virtual void Select(Character user)
    {

    }

    public virtual void Deselect(Character user)
    {

    }


}

