using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetFooterPanel : MovePanel
{
    public event Action OnClickToPlayGame;

    [SerializeField] private List<ScaleEffectCombination> combinations = new List<ScaleEffectCombination>();

    [SerializeField] private Button buttonStartPlayGame;

    public override void Initialize()
    {
        base.Initialize();

        buttonStartPlayGame.onClick.AddListener(HandleClickToPlayGameButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonStartPlayGame.onClick.RemoveListener(HandleClickToPlayGameButton);
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

    private void HandleClickToPlayGameButton()
    {
        OnClickToPlayGame?.Invoke();
    }

    #endregion
}
