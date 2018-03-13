using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {
    ActionIconObject[] actions;

    private void Start()
    {
        actions = GetComponentsInChildren<ActionIconObject>();
    }

    public void SetActions(List<Action> playerActions)
    {

        for(int i = 0; i < actions.Length; i++)
        {
            if(i < playerActions.Count)
            {
                actions[i].SetAction(playerActions[i]);
            }
            else
            {
                actions[i].Clear();
            }
        }

    }

    public void SetAction(Action action, int index)
    {

        actions[index].SetAction(action);
    }

}
