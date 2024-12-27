using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGamePanel_MainMenuScene : MovePanel
{
    [SerializeField] private List<TypeTextEffect> effects = new List<TypeTextEffect>();
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

    public override void ActivatePanel()
    {
        effects.ForEach(x => x.StopAllCoroutines());
        effects.ForEach(x => x.Activate());

        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        effects.ForEach(x => x.StopAllCoroutines());
        effects.ForEach(x => x.ClearText());

        base.DeactivatePanel();
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
