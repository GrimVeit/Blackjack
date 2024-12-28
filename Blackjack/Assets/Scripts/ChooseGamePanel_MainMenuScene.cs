using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGamePanel_MainMenuScene : MovePanel
{
    [SerializeField] private List<TypeTextEffect> textEffects = new List<TypeTextEffect>();
    [SerializeField] private List<ScaleEffectCombination> scaleEffects = new List<ScaleEffectCombination>();
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
        textEffects.ForEach(x => x.StopAllCoroutines());
        textEffects.ForEach(x => x.Activate());
        scaleEffects.ForEach(data => data.ActivateEffect());

        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        textEffects.ForEach(x => x.StopAllCoroutines());
        textEffects.ForEach(x => x.ClearText());
        scaleEffects.ForEach(data => data.DeactivateEffect());

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
