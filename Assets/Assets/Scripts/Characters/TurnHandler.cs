﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnHandler {

    TurnState GetTurnState();
    void SetTurnState(TurnState state);
    void HandleTurn();

}
