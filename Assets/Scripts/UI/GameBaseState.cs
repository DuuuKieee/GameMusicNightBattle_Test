
using System;
using UnityEngine;

[System.Serializable]
public abstract class GameBaseState
{
    public abstract string _stateName { set; get; }
    public abstract void EnterState(GameStateManager state);

    public abstract void UpdateState(GameStateManager state);
}