using System;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_GameScene : MovePanel
{
    [SerializeField] private Button buttonTransferToReload;
    [SerializeField] private Button buttonTransferToMenu;

    public override void Initialize()
    {
        base.Initialize();

        buttonTransferToMenu.onClick.AddListener(HandleTransferToMenu);
        buttonTransferToReload.onClick.AddListener(HandleTransferToReload);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonTransferToMenu.onClick.RemoveListener(HandleTransferToMenu);
        buttonTransferToReload.onClick.RemoveListener(HandleTransferToReload);
    }

    #region Input

    public event Action OnTransferToReload;
    public event Action OnTransferToMenu;

    private void HandleTransferToReload()
    {
        OnTransferToReload?.Invoke();
    }

    private void HandleTransferToMenu()
    {
        OnTransferToMenu?.Invoke();
    }

    #endregion
}
