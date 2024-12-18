using System;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_MainMenuScene : MovePanel
{
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
