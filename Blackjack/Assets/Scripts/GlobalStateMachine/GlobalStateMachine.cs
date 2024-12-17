using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateMachine : MonoBehaviour, IGlobalMachineControl
{
    private Dictionary<Type, IGlobalState> states = new Dictionary<Type, IGlobalState>();

    private IGlobalState currentState;

    public GlobalStateMachine(
        UIGameRoot sceneRoot,
        PseudoChipPresenter pseudoChipPresenter, 
        ChipPresenter chipPresenter, 
        BetPresenter betPresenter,
        CardPresenter cardPresenter_Player,
        CardPresenter cardPresenter_Dealer,
        CardScorePresenter cardScorePresenter)
    {
        states[typeof(BettingState)] = new BettingState(this, sceneRoot, pseudoChipPresenter, chipPresenter, betPresenter);
        states[typeof(AutoCardState)] = new AutoCardState(this, cardPresenter_Player, cardPresenter_Dealer, cardScorePresenter);
        states[typeof(PlayerTurnState)] = new PlayerTurnState(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<BettingState>());
    }

    public void Dispose()
    {

    }

    public IGlobalState GetState<T>() where T : IGlobalState
    {
        return states[typeof(T)];
    }

    public void SetState(IGlobalState state)
    {
        currentState?.ExitState();

        currentState = state;
        currentState.EnterState();
    }
}

public interface IGlobalMachineControl
{
    IGlobalState GetState<T>() where T : IGlobalState;
    void SetState(IGlobalState state);
}

public interface IGlobalState
{
    void EnterState();
    void ExitState();
}