using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCardState : IGlobalState
{
    private CardPresenter cardPresenter_Player;
    private CardPresenter cardPresenter_Dealer;

    private IGlobalMachineControl machineControl;

    public AutoCardState(IGlobalMachineControl machineControl, CardPresenter cardPresenter_Player, CardPresenter cardPresenter_Dealer)
    {
        this.cardPresenter_Player = cardPresenter_Player;
        this.cardPresenter_Dealer = cardPresenter_Dealer;
        this.machineControl = machineControl;
    }

    public void EnterState()
    {
        cardPresenter_Dealer.SpawnCards(new bool[] { false, true }, SpawnPlayerCards);
    }

    public void ExitState()
    {

    }

    private void SpawnPlayerCards()
    {
        cardPresenter_Player.SpawnCards(new bool[] { true, true }, ActivateState_Other);
    }

    private void ActivateState_Other()
    {

    }
}
