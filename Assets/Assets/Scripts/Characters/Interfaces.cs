﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnHandler {

    TurnState GetTurnState();
    void SetTurnState(TurnState state);
    void HandleTurn();
    Combat Combat { get; set; }
    int Initiative { get; set; }
    void DeactivateTurnHandler();
    Entity Target { get; set; }
    bool Guard { get; set; }
}

public interface ICharacterInfo
{
    Sprite Sprite { get; }
    Stats Stats { get; set; }
    bool ActivePlayer { get; set; }
    Hostility Hostility { get; set; }

}