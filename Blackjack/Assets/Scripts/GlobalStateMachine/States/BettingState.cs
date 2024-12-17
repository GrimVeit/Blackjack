using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingState : IGlobalState
{
    private UIGameRoot sceneRoot;

    private PseudoChipPresenter pseudoChipPresenter;
    private ChipPresenter chipPresenter;
    private BetPresenter betPresenter;

    private IGlobalMachineControl machineControl;

    public BettingState(IGlobalMachineControl machineControl, UIGameRoot sceneRoot, PseudoChipPresenter pseudoChipPresenter, ChipPresenter chipPresenter, BetPresenter betPresenter)
    {
        this.machineControl = machineControl;
        this.sceneRoot = sceneRoot;
        this.pseudoChipPresenter = pseudoChipPresenter;
        this.chipPresenter = chipPresenter;
        this.betPresenter = betPresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToStartPlay += ActivateState_AutoCardState;

        betPresenter.OnCountForMaxBet += chipPresenter.SpawnNumbers;

        pseudoChipPresenter.OnSpawnChip += chipPresenter.SpawnChip;
        chipPresenter.OnAddChip += betPresenter.AddBet;
        chipPresenter.OnRemoveChip += betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips += betPresenter.ClearBet;

        pseudoChipPresenter.Activate();
        betPresenter.Activate();
        sceneRoot.OpenBetFooterPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToStartPlay -= ActivateState_AutoCardState;

        betPresenter.OnCountForMaxBet -= chipPresenter.SpawnNumbers;

        pseudoChipPresenter.OnSpawnChip -= chipPresenter.SpawnChip;
        chipPresenter.OnAddChip -= betPresenter.AddBet;
        chipPresenter.OnRemoveChip -= betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips -= betPresenter.ClearBet;

        pseudoChipPresenter.Deactivate();
        betPresenter.Deactivate();
        sceneRoot.CloseBetFooterPanel();
    }

    private void ActivateState_AutoCardState()
    {
        Debug.Log("Start AutoCardState");
        machineControl.SetState(machineControl.GetState<AutoCardState>());
    }
}
