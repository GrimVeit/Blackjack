using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_MainMenuScene : MovePanel
{
    [SerializeField] private List<ScaleEffectCombination> effects = new List<ScaleEffectCombination>();

    [SerializeField] private Button buttonPlayGame;
    [SerializeField] private Button buttonDailyReward;

    public override void Initialize()
    {
        base.Initialize();

        buttonPlayGame.onClick.AddListener(HandleClickToPlayGame);
        buttonDailyReward.onClick.AddListener(HandleClickToDailyReward);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonPlayGame.onClick.RemoveListener(HandleClickToPlayGame);
        buttonPlayGame.onClick.RemoveListener(HandleClickToDailyReward);
    }

    public override void ActivatePanel()
    {
        effects.ForEach(data => data.ActivateEffect());

        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        effects.ForEach(data => data.DeactivateEffect());

        base.DeactivatePanel();
    }

    #region Input

    public event Action OnClickToPlayGame;
    public event Action OnClickToDailyReward;

    private void HandleClickToPlayGame()
    {
        OnClickToPlayGame?.Invoke();
    }

    private void HandleClickToDailyReward()
    {
        OnClickToDailyReward?.Invoke();
    }

    #endregion
}
