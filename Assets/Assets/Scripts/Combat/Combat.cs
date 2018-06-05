using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Initializing, ActiveCombat, End };

public class Combat {

    //Probably change these to combatcontrollers
    List<Character> combatants;
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

    public Combat(List<Character> entities)
    {
        combatants = entities;
        
        currentCombatantIndex = 0;
        
    }

    public void CombatSetUp()
    {
        foreach(Character c in combatants)
        {
            c.controller.combat = this;
        }
        currentState = CombatState.ActiveCombat;
        combatants[0].controller.turnstate = TurnState.StartPhase;
        TurnQueue.Instance.FillQueue(combatants);

    }

    public void JoinCombat(Character e)
    {

        if (!combatants.Contains(e))
        {
            e.controller.combat = this;
            combatants.Add(e);
            TurnQueue.Instance.AddToQueue(e);
        }
    }

    public void EndCombat()
    {
        TextLog.instance.AddEntry("End of combat");
        foreach (Character c in combatants)
        {
            c.controller.combat = null;
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
                TextLog.instance.AddEntry("<color=red>Entering Combat</color>");
                CombatSetUp();
                break;
            case CombatState.ActiveCombat:


                
                combatants[currentCombatantIndex].controller.HandleTurn();
                break;

            case CombatState.End:
                //NEVER ACTUALLY CALLED
                TextLog.instance.AddEntry("End of combat");

                break;
        }

    }

    public void RemoveFromCombat(Character toRemove)
    {
        toRemove.controller.combat = null;

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


        combatants[currentCombatantIndex].controller.turnstate = TurnState.StartPhase;


        TurnQueue.Instance.UpdateQueue(currentCombatantIndex);


    }
    
}
