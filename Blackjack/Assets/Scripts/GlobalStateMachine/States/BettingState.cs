using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingState : IGlobalState
{
    private PseudoChipPresenter pseudoChipPresenter;
    private ChipPresenter chipPresenter;
    private BetPresenter betPresenter;

    private IGlobalMachineControl machineControl;

    public BettingState(IGlobalMachineControl machineControl, PseudoChipPresenter pseudoChipPresenter, ChipPresenter chipPresenter, BetPresenter betPresenter)
    {
        this.machineControl = machineControl;
        this.pseudoChipPresenter = pseudoChipPresenter;
        this.chipPresenter = chipPresenter;
        this.betPresenter = betPresenter;
    }

    public void EnterState()
    {
        betPresenter.OnStartGame += ActivateState_AutoCardState;

        betPresenter.OnCountForMaxBet += chipPresenter.SpawnNumbers;

        pseudoChipPresenter.OnSpawnChip += chipPresenter.SpawnChip;
        chipPresenter.OnAddChip += betPresenter.AddBet;
        chipPresenter.OnRemoveChip += betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips += betPresenter.ClearBet;

        pseudoChipPresenter.Activate();
        betPresenter.Activate();
    }

    public void ExitState()
    {
        betPresenter.OnStartGame -= ActivateState_AutoCardState;

        betPresenter.OnCountForMaxBet -= chipPresenter.SpawnNumbers;

        pseudoChipPresenter.OnSpawnChip -= chipPresenter.SpawnChip;
        chipPresenter.OnAddChip -= betPresenter.AddBet;
        chipPresenter.OnRemoveChip -= betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips -= betPresenter.ClearBet;

        pseudoChipPresenter.Activate();
        betPresenter.Activate();
    }

    private void ActivateState_AutoCardState()
    {
        Debug.Log("Start auto card state");
        machineControl.SetState(machineControl.GetState<AutoCardState>());
    }
}
