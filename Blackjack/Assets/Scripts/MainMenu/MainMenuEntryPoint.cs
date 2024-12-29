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
    private GameProgressPresenter gameProgressPresenter;
    private DailyBonusPresenter dailyBonusPresenter;
    private LevelVisualizePresenter levelVisualizePresenter;

    private ScaleEffectPresenter scaleEffectPresenter;
    private TypeTextEffectPresenter typeTextEffectPresenter;

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

        gameProgressPresenter = new GameProgressPresenter(new GameProgressModel(levels));

        dailyBonusPresenter = new DailyBonusPresenter(new DailyBonusModel(bankPresenter), viewContainer.GetView<DailyBonusView>());

        levelPresenter = new LevelPresenter(new LevelModel(), viewContainer.GetView<LevelView>());

        scaleEffectPresenter = new ScaleEffectPresenter(new ScaleEffectModel(), viewContainer.GetView<ScaleEffectView>());

        typeTextEffectPresenter = new TypeTextEffectPresenter(new TypeTextEffectModel(), viewContainer.GetView<TypeTextEffectView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        bankPresenter.Initialize();
        particleEffectPresenter.Initialize();
        levelVisualizePresenter.Initialize();
        dailyBonusPresenter.Initialize();
        levelPresenter.Initialize();
        gameProgressPresenter.Initialize();
        scaleEffectPresenter.Initialize();
        typeTextEffectPresenter.Initialize();
    }

    private void ActivateEvents()
    {
        levelPresenter.OnChooseLevel += gameProgressPresenter.ChooseLevel;
        gameProgressPresenter.OnSelectLevel += levelPresenter.SelectLevel;
        gameProgressPresenter.OnSelectLevel += levelVisualizePresenter.SetLevel;
        gameProgressPresenter.OnUnselectLevel += levelPresenter.UnselectLevel;

        ActivateTransitionEvents();
    }

    private void DeactivateEvents()
    {
        levelPresenter.OnChooseLevel -= gameProgressPresenter.ChooseLevel;
        gameProgressPresenter.OnSelectLevel -= levelPresenter.SelectLevel;
        gameProgressPresenter.OnSelectLevel -= levelVisualizePresenter.SetLevel;
        gameProgressPresenter.OnUnselectLevel -= levelPresenter.UnselectLevel;

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
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();
        particleEffectPresenter?.Dispose();
        levelVisualizePresenter?.Dispose();
        dailyBonusPresenter?.Dispose();
        levelPresenter?.Dispose();
        gameProgressPresenter?.Dispose();
        scaleEffectPresenter?.Dispose();
        typeTextEffectPresenter?.Dispose();
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
