using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardPanel_MainMenuScene : MovePanel
{
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

    #region Input

    public event Action OnClickToHomeButton;

    private void HandlerClickToHomeButton()
    {
        OnClickToHomeButton?.Invoke();
    }

    #endregion
}
