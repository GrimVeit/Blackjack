using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : MonoBehaviour
{
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private BetFooterPanel betFooterPanel;
    [SerializeField] private GameFooterPanel gameFooterPanel;
    
    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        winPanel.Initialize();
        betFooterPanel.Initialize();
        gameFooterPanel.Initialize();
    }

    public void Activate()
    {

    }

    public void Deactivate()
    {
        currentPanel.DeactivatePanel();
    }

    public void Dispose()
    {
        winPanel.Dispose();
        betFooterPanel.Dispose();
        gameFooterPanel.Dispose();
    }

    public void OpenMainPanel()
    {

    }

    public void OpenWinPanel()
    {
        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        CloseOtherPanel(winPanel);
    }

    public void OpenBetFooterPanel()
    {
        OpenOtherPanel(betFooterPanel);
    }

    public void CloseBetFooterPanel()
    {
        CloseOtherPanel(betFooterPanel);
    }

    public void OpenGameFooterPanel()
    {
        OpenOtherPanel(gameFooterPanel);
    }

    public void CloseGameFooterPanel()
    {
        CloseOtherPanel(gameFooterPanel);
    }

    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }

    #region Input

    public event Action OnClickToStartPlay
    {
        add { betFooterPanel.OnClickToPlayGame += value; }
        remove { betFooterPanel.OnClickToPlayGame -= value; }
    }

    public event Action OnClickToAddCard
    {
        add { gameFooterPanel.OnClickToAddCardButton += value; }
        remove { gameFooterPanel.OnClickToAddCardButton -= value; }
    }

    public event Action OnClickToDealerTurn
    {
        add { gameFooterPanel.OnClickToDealerTurnButton += value; }
        remove { gameFooterPanel.OnClickToDealerTurnButton -= value; }
    }

    #endregion
}
