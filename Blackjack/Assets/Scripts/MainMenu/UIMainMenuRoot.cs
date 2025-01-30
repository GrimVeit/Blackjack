using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_MainMenuScene mainPanel;
    [SerializeField] private DailyRewardPanel_MainMenuScene dailyRewardPanel;
    [SerializeField] private ChooseGamePanel_MainMenuScene chooseGamePanel;
    [SerializeField] private GameDescriptionPanel descriptionPanel;
    [SerializeField] private RoulettePanel roulettePanel;

    [SerializeField] private WinPanel winPanel;
    [SerializeField] private LosePanel losePanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        dailyRewardPanel.Initialize();
        chooseGamePanel.Initialize();
        descriptionPanel.Initialize();

        roulettePanel.Initialize();
        winPanel.Initialize();
        losePanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        dailyRewardPanel.Dispose();
        chooseGamePanel.Dispose();
        descriptionPanel.Dispose();

        roulettePanel.Dispose();
        winPanel.Dispose();
        losePanel.Dispose();
    }

    public void Activate()
    {
        OpenMainPanel();
    }

    public void Deactivate()
    {
        currentPanel.DeactivatePanel();
    }



    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenDailyRewardPanel()
    {
        OpenPanel(dailyRewardPanel);
    }

    public void OpenChooseGamePanel()
    {
        OpenPanel(chooseGamePanel);
    }

    public void OpenGameDescriptionPanel()
    {
        OpenPanel(descriptionPanel);
    }

    public void OpenRoulettePanel()
    {
        OpenPanel(roulettePanel);
    }




    public void OpenWinPanel()
    {
        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        CloseOtherPanel(winPanel);
    }

    public void OpenLosePanel()
    {
        OpenOtherPanel(losePanel);
    }

    public void CloseLosePanel()
    {
        CloseOtherPanel(losePanel);
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


    #region Input Actions

    public event Action OnClickToPlayGame_MainPanel
    {
        add { mainPanel.OnClickToPlayGame += value; }
        remove { mainPanel.OnClickToPlayGame -= value; }
    }

    public event Action OnClickToDailyReward_MainPanel
    {
        add { mainPanel.OnClickToDailyReward += value; }
        remove { mainPanel.OnClickToDailyReward -= value; }
    }

    public event Action OnClickToRoulette_MainPanel
    {
        add { mainPanel.OnClickToRoulette += value; }
        remove { mainPanel.OnClickToRoulette -= value; }
    }




    public event Action OnClickToHome_DailyRewardPanel
    {
        add { dailyRewardPanel.OnClickToHomeButton += value; }
        remove { dailyRewardPanel.OnClickToHomeButton -= value; }
    }



    public event Action OnClickToPlay_ChooseGamePanel
    {
        add { chooseGamePanel.OnClickToPlayButton += value; }
        remove { chooseGamePanel.OnClickToPlayButton -= value; }
    }

    public event Action OnClickToHome_ChooseGamePanel
    {
        add { chooseGamePanel.OnClickToHomeButton += value; }
        remove { chooseGamePanel.OnClickToHomeButton -= value; }
    }




    public event Action OnClickToCancel_GameDescriptionPanel
    {
        add { descriptionPanel.OnClickToCancel += value; }
        remove { descriptionPanel.OnClickToCancel -= value; }
    }

    public event Action OnClickToHome_GameDescriptionPanel
    {
        add { descriptionPanel.OnClickToHome += value; }
        remove { descriptionPanel.OnClickToHome -= value; }
    }

    public event Action OnClickToHome_RoulettePanel
    {
        add { roulettePanel.OnClickExit += value; }
        remove { roulettePanel.OnClickExit -= value; }
    }

    public event Action OnClickToStartPlayGame
    {
        add { descriptionPanel.OnClickToStartPlayGame += value; }
        remove { descriptionPanel.OnClickToStartPlayGame -= value; }
    }


    public event Action OnClickToExit_WinPanel
    {
        add { winPanel.OnClickExitWin += value; }
        remove { winPanel.OnClickExitWin -= value; }
    }

    public event Action OnClickToExit_LosePanel
    {
        add { losePanel.OnClickExitLose += value; }
        remove { losePanel.OnClickExitLose -= value; }
    }

    #endregion
}
