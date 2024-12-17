using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : IGlobalState
{
    private UIGameRoot sceneRoot;

    private IGlobalMachineControl machineControl;

    public PlayerTurnState(IGlobalMachineControl machineControl, UIGameRoot sceneRoot)
    {
        this.sceneRoot = sceneRoot;
        this.machineControl = machineControl;
    }

    public void EnterState()
    {
        sceneRoot.OpenGameFooterPanel();
    }

    public void ExitState()
    {
        sceneRoot.CloseGameFooterPanel();
    }
}
