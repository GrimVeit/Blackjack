using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneState : IGlobalState
{
    private ChipPresenter chipPresenter;
    private BetPresenter betPresenter;

    private CardPresenter cardPresenter_Player;
    private CardPresenter cardPresenter_Dealer;

    private CardScorePresenter cardScorePresenter;

    private IGlobalMachineControl machineControl;

    public NoneState(IGlobalMachineControl machineControl, ChipPresenter chipPresenter, BetPresenter betPresenter, CardPresenter cardPresenter_Player, CardPresenter cardPresenter_Dealer, CardScorePresenter cardScorePresenter)
    {
        this.machineControl = machineControl;
        this.chipPresenter = chipPresenter;
        this.betPresenter = betPresenter;
        this.cardPresenter_Player = cardPresenter_Player;
        this.cardPresenter_Dealer = cardPresenter_Dealer;
        this.cardScorePresenter = cardScorePresenter;
    }

    public void EnterState()
    {
        cardPresenter_Dealer.OnClearNominal += cardScorePresenter.ClearScore_Dealer;
        cardPresenter_Player.OnClearNominal += cardScorePresenter.ClearScore_Player;

        chipPresenter.OnRemoveAllChips += betPresenter.ReturnBet;

        betPresenter.Activate();
        chipPresenter.RetractAllChips();

        Coroutines.Start(Timer());
    }

    public void ExitState()
    {
        chipPresenter.OnRemoveAllChips -= betPresenter.ReturnBet;

        cardPresenter_Dealer.ClearCards();
        cardPresenter_Player.ClearCards();

        cardPresenter_Dealer.OnClearNominal -= cardScorePresenter.ClearScore_Dealer;
        cardPresenter_Player.OnClearNominal -= cardScorePresenter.ClearScore_Player;

        betPresenter.Deactivate();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        ActivateState_BettingState();
    }

    private void ActivateState_BettingState()
    {
        Debug.Log("Start BettingState");
        machineControl.SetState(machineControl.GetState<BettingState>());
    }
}
