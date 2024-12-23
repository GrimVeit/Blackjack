using System;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private CardDatas cardDatas;
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

        betPresenter = new BetPresenter(new BetModel(10, 119, bankPresenter), viewContainer.GetView<BetView>());
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

        ActivateTransferEvents();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();
        sceneRoot.Activate();
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
