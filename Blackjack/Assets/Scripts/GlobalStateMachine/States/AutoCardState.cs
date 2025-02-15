using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCardState : IGlobalState
{
    private CardPresenter cardPresenter_Player;
    private CardPresenter cardPresenter_Dealer;

    private CardScorePresenter cardScorePresenter;

    private IGlobalMachineControl machineControl;

    public AutoCardState(IGlobalMachineControl machineControl, CardPresenter cardPresenter_Player, CardPresenter cardPresenter_Dealer, CardScorePresenter cardScorePresenter)
    {
        this.machineControl = machineControl;
        this.cardPresenter_Player = cardPresenter_Player;
        this.cardPresenter_Dealer = cardPresenter_Dealer;
        this.cardScorePresenter = cardScorePresenter;
    }

    public void EnterState()
    {
        cardScorePresenter.OnLose += ActivateLose;

        cardPresenter_Player.OnAddNominal += cardScorePresenter.AddScore_Player;
        cardPresenter_Dealer.OnAddNominal += cardScorePresenter.AddScore_Dealer;

        cardPresenter_Dealer.OnClearNominal += cardScorePresenter.ClearScore_Dealer;
        cardPresenter_Player.OnClearNominal += cardScorePresenter.ClearScore_Player;

        cardPresenter_Dealer.SpawnCards(new bool[] { false, true }, SpawnPlayerCards);
    }

    public void ExitState()
    {
        cardScorePresenter.OnLose -= ActivateLose;

        cardPresenter_Player.OnAddNominal -= cardScorePresenter.AddScore_Player;
        cardPresenter_Dealer.OnAddNominal -= cardScorePresenter.AddScore_Dealer;

        cardPresenter_Dealer.OnClearNominal -= cardScorePresenter.ClearScore_Dealer;
        cardPresenter_Player.OnClearNominal -= cardScorePresenter.ClearScore_Player;
    }

    private void SpawnPlayerCards()
    {
        cardPresenter_Player.SpawnCards(new bool[] { true, true }, ActivateState_PlayerTurn);
    }

    private void ActivateLose()
    {
        ActivateState_LoseState();
    }

    private void ActivateState_PlayerTurn()
    {
        Debug.Log("Start PlayerTurnState");
        machineControl.SetState(machineControl.GetState<PlayerTurnState>());
    }

    private void ActivateState_LoseState()
    {
        Debug.Log("Start LoseState");
        machineControl.SetState(machineControl.GetState<LoseState>());
    }
}
