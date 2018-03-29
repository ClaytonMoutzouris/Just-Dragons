using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Initializing, ActiveCombat, End };

public class Combat {

    //Probably change these to combatcontrollers
    List<ITurnHandler> combatants;
    CombatState currentState = CombatState.Initializing;
    int currentCombatantIndex = 0;

    public CombatState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    public Combat(List<ITurnHandler> entities)
    {
        combatants = entities;
        
        currentCombatantIndex = 0;
        
    }

    public void CombatSetUp()
    {
        foreach(ITurnHandler c in combatants)
        {
            c.Combat = this;
        }
        currentState = CombatState.ActiveCombat;
        combatants[0].SetTurnState(TurnState.Start);
        TurnQueue.Instance.FillQueue(combatants);

    }

    public void JoinCombat(ITurnHandler e)
    {

        if (!combatants.Contains(e))
        {
            Debug.Log("Joining combat!");
            e.Combat = this;
            combatants.Add(e);
            TurnQueue.Instance.AddToQueue(e);
        }
    }

    public void EndCombat()
    {
        Debug.Log("End Combat");
        foreach (ITurnHandler c in combatants)
        {
            c.Combat = null;
        }
        combatants.Clear();
        TurnQueue.Instance.EmptyQueue();
    }

    public void CheckFinished()
    {
        if (combatants.Count <= 1)
        {
            currentState = CombatState.End;
        }

    }

    public void Update()
    {
        //Debug.Log("updating combat");
        switch (CurrentState)
        {
            case CombatState.Initializing:
                Debug.Log("Initializing combat");

                CombatSetUp();
                break;
            case CombatState.ActiveCombat:

                //Debug.Log(combatants[currentCombatantIndex]);

                combatants[currentCombatantIndex].HandleTurn();
                break;

            case CombatState.End:
                Debug.Log("End Of combat");
                
                break;
        }

    }

    public void RemoveFromCombat(ITurnHandler toRemove)
    {
        toRemove.Combat = null;
        int index = combatants.IndexOf(toRemove);
        if (index < currentCombatantIndex)
            currentCombatantIndex--;
        TurnQueue.Instance.RemoveAtIndex(index);
        combatants.Remove(toRemove);
        Debug.Log(toRemove + " removed from combat");
        CheckFinished();
    }

    public void NextTurn()
    {

        GameManager.instance.ClearSelected();

        currentCombatantIndex++;
        if (currentCombatantIndex >= combatants.Count)
        {
            currentCombatantIndex = 0;
        }


        combatants[currentCombatantIndex].SetTurnState(TurnState.Start);

        Debug.Log("Being called twice for some reason");
        TurnQueue.Instance.UpdateQueue(currentCombatantIndex);


    }
    
}
