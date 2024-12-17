using System;
using UnityEngine;
using UnityEngine.UI;

public class BetFooterPanel : MovePanel
{
    public event Action OnClickToPlayGame;

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

    #region Input

    private void HandleClickToPlayGameButton()
    {
        OnClickToPlayGame?.Invoke();
    }

    #endregion
}
