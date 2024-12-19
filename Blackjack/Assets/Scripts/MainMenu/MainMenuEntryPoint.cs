using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private Levels levels;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private LevelPresenter levelPresenter;
    private LevelVisualizePresenter levelVisualizePresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(menuRootPrefab);

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

        levelVisualizePresenter = new LevelVisualizePresenter(new LevelVisualizeModel(bankPresenter), viewContainer.GetView<LevelVisualizeView>());

        levelPresenter = new LevelPresenter(new LevelModel(levels), viewContainer.GetView<LevelView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        bankPresenter.Initialize();
        particleEffectPresenter.Initialize();
        levelVisualizePresenter.Initialize();
        levelPresenter.Initialize();
    }

    private void ActivateEvents()
    {
        levelPresenter.OnChooseLevel += levelVisualizePresenter.SetData;

        ActivateTransitionEvents();
    }

    private void DeactivateEvents()
    {
        levelPresenter.OnChooseLevel -= levelVisualizePresenter.SetData;

        DeactivateTransitionEvents();
    }

    private void ActivateTransitionEvents()
    {
        sceneRoot.OnClickToStartPlayGame += HandleTransitionToGameScene;

        sceneRoot.OnClickToDailyReward_MainPanel += sceneRoot.OpenDailyRewardPanel;
        sceneRoot.OnClickToPlayGame_MainPanel += sceneRoot.OpenChooseGamePanel;

        sceneRoot.OnClickToHome_DailyRewardPanel += sceneRoot.OpenMainPanel;

        sceneRoot.OnClickToHome_ChooseGamePanel += sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToPlay_ChooseGamePanel += sceneRoot.OpenGameDescriptionPanel;

        sceneRoot.OnClickToCancel_GameDescriptionPanel += sceneRoot.OpenChooseGamePanel;
        sceneRoot.OnClickToHome_GameDescriptionPanel += sceneRoot.OpenMainPanel;
    }

    private void DeactivateTransitionEvents()
    {
        sceneRoot.OnClickToStartPlayGame -= HandleTransitionToGameScene;

        sceneRoot.OnClickToDailyReward_MainPanel -= sceneRoot.OpenDailyRewardPanel;
        sceneRoot.OnClickToPlayGame_MainPanel -= sceneRoot.OpenChooseGamePanel;

        sceneRoot.OnClickToHome_DailyRewardPanel -= sceneRoot.OpenMainPanel;

        sceneRoot.OnClickToHome_ChooseGamePanel -= sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToPlay_ChooseGamePanel -= sceneRoot.OpenGameDescriptionPanel;

        sceneRoot.OnClickToCancel_GameDescriptionPanel -= sceneRoot.OpenChooseGamePanel;
        sceneRoot.OnClickToHome_GameDescriptionPanel -= sceneRoot.OpenMainPanel;
    }

    private void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action OnTransitionToGameScene;

    private void HandleTransitionToGameScene()
    {
        sceneRoot.Deactivate();
        OnTransitionToGameScene?.Invoke();
    }

    #endregion
}
