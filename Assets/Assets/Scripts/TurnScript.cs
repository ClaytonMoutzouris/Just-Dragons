using UnityEngine;
using System.Collections;

public enum Round
{
    Info,
    Move,
    Attack,
    Item,
    Pickup,
    Scene
}
public class TurnScript : MonoBehaviour
{
    //Other Classes that handle actions;
    //public Turn_Info TII;
   // public Turn_Move TM;
   // public Turn_Attack TA;
   // public Turn_Item TI;
   // public Turn_Pickup TP;
   // public Turn_Scene TS;
    public Round turn = Round.Info;
    void Update()
    {
        switch (turn)
        {
            case Round.Info:
                //Tool Tips, Pause, ext. 
               // TII.InfoLogic();
                break;
            case Round.Move:
                //Move logic and anything pertaining to movement actions with characters.
              //  TM.MoveLogic();
                break;
            case Round.Attack:
                //Damage step, assigning attackers and taking actions.
               // TA.AttackLogic();
                break;
            case Round.Item:
                //Handle Items and inventory, or something invoking use of items. Levers,buttons,doors ect.
              //  TI.ItemLogic();
                break;
            case Round.Pickup:
                //Interactions with Dropped items
              //  TP.PickupLogic();
                break;
            case Round.Scene:
                //Animation logic with events in the level like talking, walking, ect.
              //  TS.SceneLogic();
                break;
            default:
                break;
        }
    }
}