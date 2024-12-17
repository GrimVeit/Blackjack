using UnityEngine;

public class PlayerTurnState : IGlobalState
{
    private UIGameRoot sceneRoot;

    private CardPresenter cardPresenter_Player;
    private CardPresenter cardPresenter_Dealer;
    private CardScorePresenter cardScorePresenter;

    private ChipPresenter chipPresenter;
    private BetPresenter betPresenter;

    private IGlobalMachineControl machineControl;

    public PlayerTurnState(IGlobalMachineControl machineControl, UIGameRoot sceneRoot, CardPresenter cardPresenter_Player, CardPresenter cardPresenter_Dealer, CardScorePresenter cardScorePresenter, BetPresenter betPresenter, ChipPresenter chipPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.machineControl = machineControl;
        this.cardPresenter_Player = cardPresenter_Player;
        this.cardPresenter_Dealer = cardPresenter_Dealer;
        this.cardScorePresenter = cardScorePresenter;
        this.betPresenter = betPresenter;
        this.chipPresenter = chipPresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToAddCard += SpawnCard;
        sceneRoot.OnClickToDealerTurn += ActivateState_DealerTurnState;

        betPresenter.OnCountForMaxBet += chipPresenter.SpawnNumbers;
        chipPresenter.OnAddChip += betPresenter.AddBet;
        chipPresenter.OnRemoveChip += betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips += betPresenter.ClearBet;

        cardScorePresenter.OnLose += ActivateState_BeitingState;

        cardPresenter_Player.OnAddNominal += cardScorePresenter.AddScore_Player;

        cardPresenter_Player.OnClearNominal += cardScorePresenter.ClearScore_Player;
        cardPresenter_Dealer.OnClearNominal += cardScorePresenter.ClearScore_Dealer;

        sceneRoot.OpenGameFooterPanel();
        betPresenter.Activate();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToAddCard -= SpawnCard;
        sceneRoot.OnClickToDealerTurn -= ActivateState_DealerTurnState;

        betPresenter.OnCountForMaxBet -= chipPresenter.SpawnNumbers;
        chipPresenter.OnAddChip -= betPresenter.AddBet;
        chipPresenter.OnRemoveChip -= betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips -= betPresenter.ClearBet;

        cardScorePresenter.OnLose -= ActivateState_BeitingState;

        cardPresenter_Player.OnAddNominal -= cardScorePresenter.AddScore_Player;

        cardPresenter_Player.OnClearNominal -= cardScorePresenter.ClearScore_Player;
        cardPresenter_Dealer.OnClearNominal -= cardScorePresenter.ClearScore_Dealer;

        sceneRoot.CloseGameFooterPanel();
        betPresenter.Deactivate();
    }

    private void SpawnCard()
    {
        cardPresenter_Player.SpawnCards(new bool[] { true });
    }

    private void ActivateState_DealerTurnState()
    {
        machineControl.SetState(machineControl.GetState<DealerTurnState>());
    }

    private void ActivateState_BeitingState()
    {
        cardPresenter_Player.ClearCards();
        cardPresenter_Dealer.ClearCards();

        Debug.Log("Start BettingState");
        machineControl.SetState(machineControl.GetState<BettingState>());
    }
}
