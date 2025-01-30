using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_MainMenuScene : MovePanel
{
    [SerializeField] private List<ScaleEffectCombination> effects = new List<ScaleEffectCombination>();

    [SerializeField] private Button buttonPlayGame;
    [SerializeField] private Button buttonDailyReward;
    [SerializeField] private Button buttonRoulette;

    public override void Initialize()
    {
        base.Initialize();

        buttonPlayGame.onClick.AddListener(HandleClickToPlayGame);
        buttonDailyReward.onClick.AddListener(HandleClickToDailyReward);
        buttonRoulette.onClick.AddListener(HandleClickToRoulette);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonPlayGame.onClick.RemoveListener(HandleClickToPlayGame);
        buttonPlayGame.onClick.RemoveListener(HandleClickToDailyReward);
        buttonRoulette.onClick.RemoveListener(HandleClickToRoulette);
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
    public event Action OnClickToRoulette;

    private void HandleClickToPlayGame()
    {
        OnClickToPlayGame?.Invoke();
    }

    private void HandleClickToDailyReward()
    {
        OnClickToDailyReward?.Invoke();
    }

    private void HandleClickToRoulette()
    {
        OnClickToRoulette?.Invoke();
    }

    #endregion
}
