using System.Collections;
using UnityEngine;

public class WinState : IGlobalState
{
    private UIGameRoot sceneRoot;
    private ChipPresenter chipPresenter;
    private BetPresenter betPresenter;
    private BankPresenter bankPresenter;

    private CardPresenter cardPresenter_Player;
    private CardPresenter cardPresenter_Dealer;

    private CardScorePresenter cardScorePresenter;

    private IGlobalMachineControl machineControl;

    public WinState(IGlobalMachineControl machineControl, UIGameRoot sceneRoot, ChipPresenter chipPresenter, BetPresenter betPresenter, BankPresenter bankPresenter, CardPresenter cardPresenter_Player, CardPresenter cardPresenter_Dealer, CardScorePresenter cardScorePresenter)
    {
        this.machineControl = machineControl;
        this.sceneRoot = sceneRoot;
        this.chipPresenter = chipPresenter;
        this.betPresenter = betPresenter;
        this.bankPresenter = bankPresenter;
        this.cardPresenter_Player = cardPresenter_Player;
        this.cardPresenter_Dealer = cardPresenter_Dealer;
        this.cardScorePresenter = cardScorePresenter;
    }

    public void EnterState()
    {
        cardPresenter_Dealer.OnClearNominal += cardScorePresenter.ClearScore_Dealer;
        cardPresenter_Player.OnClearNominal += cardScorePresenter.ClearScore_Player;

        chipPresenter.OnRemoveChip += betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips += betPresenter.ReturnBet;

        betPresenter.Activate();
        betPresenter.DisplayWin(betPresenter.CurrentBet / 2);
        bankPresenter.SendMoney(betPresenter.CurrentBet / 2);
        chipPresenter.RetractAllChips();

        sceneRoot.OpenWinPanel();

        Coroutines.Start(Timer());
    }

    public void ExitState()
    {
        chipPresenter.OnRemoveChip -= betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips -= betPresenter.ReturnBet;

        cardPresenter_Dealer.ClearCards();
        cardPresenter_Player.ClearCards();

        cardPresenter_Dealer.OnClearNominal -= cardScorePresenter.ClearScore_Dealer;
        cardPresenter_Player.OnClearNominal -= cardScorePresenter.ClearScore_Player;

        betPresenter.Deactivate();
        sceneRoot.CloseWinPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);

        ActivateState_BettingState();
    }

    private void ActivateState_BettingState()
    {
        Debug.Log("Start BettingState");
        machineControl.SetState(machineControl.GetState<BettingState>());
    }
}
