using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGamePanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button playButton;

    public override void Initialize()
    {
        base.Initialize();

        homeButton.onClick.AddListener(HandleClickToHomeButton);
        playButton.onClick.AddListener(HandleClickToPlayButton);
    }

    public override void Dispose()
    {
        base.Dispose();
        
        homeButton.onClick.RemoveListener(HandleClickToHomeButton);
        playButton.onClick.RemoveListener(HandleClickToPlayButton);
    }

    #region Input

    public event Action OnClickToHomeButton;
    public event Action OnClickToPlayButton;

    private void HandleClickToHomeButton()
    {
        OnClickToHomeButton?.Invoke();
    }

    private void HandleClickToPlayButton()
    {
        OnClickToPlayButton?.Invoke();
    }

    #endregion
}
