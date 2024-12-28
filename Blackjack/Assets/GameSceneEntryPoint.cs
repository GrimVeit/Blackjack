using System;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private CardDatas cardDatas;
    [SerializeField] private Levels levels;
    [SerializeField] private UIGameRoot sceneRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private PseudoChipPresenter pseudoChipPresenter;
    private ChipPresenter chipPresenter;
    private BetPresenter betPresenter;

    private CardPresenter cardPresenter_Player;
    private CardPresenter cardPresenter_Dealer;

    private CardScorePresenter cardScorePresenter;

    private GameProgressPresenter gameProgressPresenter;
    private LevelInfoPresenter levelInfoPresenter;

    private TypeTextEffectPresenter textEffectPresenter;
    private ScaleEffectPresenter scaleEffectPresenter;

    private GlobalStateMachine globalStateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        betPresenter = new BetPresenter(new BetModel(bankPresenter), viewContainer.GetView<BetView>());
        betPresenter.Initialize();

        pseudoChipPresenter = new PseudoChipPresenter(new PseudoChipModel(bankPresenter, betPresenter, soundPresenter), viewContainer.GetView<PseudoChipView>());
        pseudoChipPresenter.Initialize();

        chipPresenter = new ChipPresenter(new ChipModel(soundPresenter), viewContainer.GetView<ChipView>());
        chipPresenter.Initialize();

        cardPresenter_Player = new CardPresenter(new CardModel(cardDatas), viewContainer.GetView<CardView>("Player"));
        cardPresenter_Player.Initialize();

        cardPresenter_Dealer = new CardPresenter(new CardModel(cardDatas), viewContainer.GetView<CardView>("Dealer"));
        cardPresenter_Dealer.Initialize();

        cardScorePresenter = new CardScorePresenter(new CardScoreModel(), viewContainer.GetView<CardScoreView>());
        cardScorePresenter.Initialize();

        textEffectPresenter = new TypeTextEffectPresenter(new TypeTextEffectModel(), viewContainer.GetView<TypeTextEffectView>());
        textEffectPresenter.Initialize();

        scaleEffectPresenter = new ScaleEffectPresenter(new ScaleEffectModel(), viewContainer.GetView<ScaleEffectView>());
        scaleEffectPresenter.Initialize();

        levelInfoPresenter = new LevelInfoPresenter(new LevelInfoModel(), viewContainer.GetView<LevelInfoView>());
        levelInfoPresenter.Initialize();

        gameProgressPresenter = new GameProgressPresenter(new GameProgressModel(levels));

        globalStateMachine = new GlobalStateMachine(
            sceneRoot,
            pseudoChipPresenter, 
            chipPresenter, 
            betPresenter, 
            cardPresenter_Player, 
            cardPresenter_Dealer, 
            cardScorePresenter,
            bankPresenter);
        globalStateMachine.Initialize();

        ActivateEvents();
        ActivateTransferEvents();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();
        sceneRoot.Activate();

        gameProgressPresenter.Initialize();
    }

    private void ActivateEvents()
    {
        gameProgressPresenter.OnSelectLevel += betPresenter.SetLevel;
        gameProgressPresenter.OnSelectLevel += levelInfoPresenter.SetLevel;
        gameProgressPresenter.OnSelectLevel += pseudoChipPresenter.SetLevel;
        gameProgressPresenter.OnSelectLevel += chipPresenter.SetLevel;
    }

    private void DeactivateEvents()
    {
        gameProgressPresenter.OnSelectLevel -= betPresenter.SetLevel;
        gameProgressPresenter.OnSelectLevel -= levelInfoPresenter.SetLevel;
        gameProgressPresenter.OnSelectLevel -= pseudoChipPresenter.SetLevel;
        gameProgressPresenter.OnSelectLevel -= chipPresenter.SetLevel;
    }

    private void ActivateTransferEvents()
    {
        sceneRoot.OnTransferToMenu += HandleTransitionToMenuScene;
        sceneRoot.OnTransferToReload += HandleTransitionToGameScene;
    }

    private void DeactivateTransferEvents()
    {
        sceneRoot.OnTransferToMenu -= HandleTransitionToMenuScene;
        sceneRoot.OnTransferToReload -= HandleTransitionToGameScene;
    }

    private void Dispose()
    {
        DeactivateEvents();
        DeactivateTransferEvents();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();

        pseudoChipPresenter?.Dispose();
        chipPresenter?.Dispose();
        betPresenter?.Dispose();
        cardPresenter_Player?.Dispose();
        cardPresenter_Dealer?.Dispose();
        cardScorePresenter?.Dispose();
        gameProgressPresenter?.Dispose();
        textEffectPresenter?.Dispose();
        scaleEffectPresenter?.Dispose();
        globalStateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnTransitionToGameScene;
    public event Action OnTransitionToMenuScene;

    private void HandleTransitionToGameScene()
    {
        sceneRoot.Deactivate();
        OnTransitionToGameScene?.Invoke();
    }

    private void HandleTransitionToMenuScene()
    {
        sceneRoot.Deactivate();
        OnTransitionToMenuScene?.Invoke();
    }

    #endregion
}
