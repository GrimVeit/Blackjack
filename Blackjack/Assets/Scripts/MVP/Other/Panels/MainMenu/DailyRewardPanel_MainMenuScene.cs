using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardPanel_MainMenuScene : MovePanel
{
    [SerializeField] private List<ScaleEffectCombination> scaleEffects = new List<ScaleEffectCombination>();
    [SerializeField] private Button homeButton;

    public override void Initialize()
    {
        base.Initialize();

        homeButton.onClick.AddListener(HandlerClickToHomeButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        homeButton.onClick.AddListener(HandlerClickToHomeButton);
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        scaleEffects.ForEach(data => data.ActivateEffect());
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        scaleEffects.ForEach(data => data.DeactivateEffect());
    }

    #region Input

    public event Action OnClickToHomeButton;

    private void HandlerClickToHomeButton()
    {
        OnClickToHomeButton?.Invoke();
    }

    #endregion
}
