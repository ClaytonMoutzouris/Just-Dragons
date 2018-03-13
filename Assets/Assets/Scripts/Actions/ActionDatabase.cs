﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//=== Skil Database: =============================
public class ActionDatabase : ScriptableObject
{
    //[SerializeField]
    public List<Action> actions = new List<Action>();

    public void Awake()
    {
        EditorUtility.SetDirty(this);
        // At runtime, instantiate skills so you don't modify design-time originals.
        // Assumes this SkillDatabase is itself already an instantiated copy.
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i] = Instantiate(actions[i]);
        }
    }

    /*
    public void Update(MonoBehaviour parentMonoBehaviour)
    {
        actions.ForEach(action => action.Update(parentMonoBehaviour));
    }
    */
}