using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparisionState : IGlobalState
{
    private CardScorePresenter cardScorePresenter;

    private IGlobalMachineControl machineControl;

    public ComparisionState(IGlobalMachineControl machineControl, CardScorePresenter cardScorePresenter)
    {
        this.machineControl = machineControl;
        this.cardScorePresenter = cardScorePresenter;
    }

    public void EnterState()
    {
        if(cardScorePresenter.ScorePlayer > cardScorePresenter.ScoreDealer)
        {
            ActivateState_Win();
        }
        else if(cardScorePresenter.ScorePlayer < cardScorePresenter.ScoreDealer)
        {
            ActivateState_Lose();
        }
        else
        {
            ActivateState_None();
        }
    }

    public void ExitState()
    {

    }

    private void ActivateState_Win()
    {
        machineControl.SetState(machineControl.GetState<WinState>());
    }

    private void ActivateState_Lose()
    {

    }

    private void ActivateState_None()
    {

    }
}
