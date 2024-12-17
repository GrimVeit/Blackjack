using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFooterPanel : MovePanel
{
    public event Action OnClickToAddCardButton;
    public event Action OnClickToDealerTurnButton;

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
