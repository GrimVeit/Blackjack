using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusPresenter
{
    private DailyBonusModel model;
    private DailyBonusView view;

    public DailyBonusPresenter(DailyBonusModel model, DailyBonusView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        model.Initialize();
        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        model.Dispose();
        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnClickToClaimButton += model.ClaimBonus;

        model.OnAlreadyClaimBonus += view.AlreadyClaim;
        model.OnCurrentClaimBonus += view.CurrentClaim;
        model.OnNotClaimBonus += view.CloseClaim;
        model.OnActivatedClaimButton += view.ActivateClaimButton;
        model.OnDeactivatedClaimButton += view.DeactivateClaimButton;

        model.OnActivateTimer += view.ActivateTimer;
        model.OnDeactivateTimer += view.DeactivateTimer;
        model.OnCountdownTimer += view.SetTime;
    }

    private void DeactivateEvents()
    {
        view.OnClickToClaimButton -= model.ClaimBonus;

        model.OnAlreadyClaimBonus -= view.AlreadyClaim;
        model.OnCurrentClaimBonus -= view.CurrentClaim;
        model.OnNotClaimBonus -= view.CloseClaim;
        model.OnActivatedClaimButton -= view.ActivateClaimButton;
        model.OnDeactivatedClaimButton -= view.DeactivateClaimButton;

        model.OnActivateTimer -= view.ActivateTimer;
        model.OnDeactivateTimer -= view.DeactivateTimer;
        model.OnCountdownTimer -= view.SetTime;
    }
}
