using System;
using UnityEngine;
using UnityEngine.UI;

public class GameDescriptionPanel : MovePanel
{
    [SerializeField] private Button buttonStartPlay;
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Button buttonHome;

    public override void Initialize()
    {
        base.Initialize();

        buttonStartPlay.onClick.AddListener(HandleClickToStartPlay);
        buttonCancel.onClick.AddListener(HandleClickToCancel);
        buttonHome.onClick.AddListener(HandleClickToHome);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonStartPlay.onClick.RemoveListener(HandleClickToStartPlay);
        buttonCancel.onClick.RemoveListener(HandleClickToCancel);
        buttonHome.onClick.RemoveListener(HandleClickToHome);
    }

    #region Input

    public event Action OnClickToStartPlayGame;
    public event Action OnClickToCancel;
    public event Action OnClickToHome;

    private void HandleClickToStartPlay()
    {
        OnClickToStartPlayGame?.Invoke();
    }

    private void HandleClickToCancel()
    {
        OnClickToCancel?.Invoke();
    }

    private void HandleClickToHome()
    {
        OnClickToHome?.Invoke();
    }

    #endregion
}
