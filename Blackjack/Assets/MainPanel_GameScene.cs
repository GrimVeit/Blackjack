using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_GameScene : MovePanel
{
    [SerializeField] private List<ScaleEffectCombination> effectCombinations = new List<ScaleEffectCombination>();
    [SerializeField] private List<Panel> panelList = new List<Panel>();
    [SerializeField] private Button buttonTransferToReload;
    [SerializeField] private Button buttonTransferToMenu;

    public override void Initialize()
    {
        base.Initialize();

        panelList.ForEach(panel => panel.Initialize());

        buttonTransferToMenu.onClick.AddListener(HandleTransferToMenu);
        buttonTransferToReload.onClick.AddListener(HandleTransferToReload);
    }

    public override void Dispose()
    {
        base.Dispose();

        panelList.ForEach(panel => panel.Dispose());

        buttonTransferToMenu.onClick.RemoveListener(HandleTransferToMenu);
        buttonTransferToReload.onClick.RemoveListener(HandleTransferToReload);
    }

    public override void ActivatePanel()
    {
        Debug.Log("Open game panel");

        panelList.ForEach(panel => panel.ActivatePanel());

        effectCombinations.ForEach(data => data.ActivateEffect());

        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        Debug.Log("Close game panel");

        panelList.ForEach(panel => panel.DeactivatePanel());

        effectCombinations.ForEach(data => data.DeactivateEffect());

        base.DeactivatePanel();
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
