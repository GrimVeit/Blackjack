using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFooterPanel : MovePanel
{
    public event Action OnClickToAddCardButton;
    public event Action OnClickToDealerTurnButton;

    [SerializeField] private List<ScaleEffectCombination> combinations = new List<ScaleEffectCombination>();

    [SerializeField] private Button buttonAddCard;
    [SerializeField] private Button buttonDealerTurn;

    public override void Initialize()
    {
        base.Initialize();

        buttonAddCard.onClick.AddListener(HandleClickToAddCardButton);
        buttonDealerTurn.onClick.AddListener(HandleClickToDealerTurnButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonAddCard.onClick.RemoveListener(HandleClickToAddCardButton);
        buttonDealerTurn.onClick.RemoveListener(HandleClickToDealerTurnButton);
    }

    public override void ActivatePanel()
    {
        combinations.ForEach(data => data.ActivateEffect());

        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        combinations.ForEach(data => data.DeactivateEffect());

        base.DeactivatePanel();
    }

    #region Input

    private void HandleClickToAddCardButton()
    {
        OnClickToAddCardButton?.Invoke();
    }

    private void HandleClickToDealerTurnButton()
    {
        OnClickToDealerTurnButton?.Invoke();
    }

    #endregion
}
