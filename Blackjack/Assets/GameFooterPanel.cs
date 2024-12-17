using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFooterPanel : MovePanel
{
    public event Action OnClickToAddCardButton;

    [SerializeField] private Button buttonAddCard;

    public override void Initialize()
    {
        base.Initialize();

        buttonAddCard.onClick.AddListener(HandleClickToAddCardButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonAddCard.onClick.RemoveListener(HandleClickToAddCardButton);
    }

    #region Input

    private void HandleClickToAddCardButton()
    {
        OnClickToAddCardButton?.Invoke();
    }

    #endregion
}
