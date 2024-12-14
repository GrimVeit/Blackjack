using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    //[SerializeField] private UIGameRoot sceneRootPrefab;

    [SerializeField] private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private PseudoChipPresenter pseudoChipPresenter;
    private ChipPresenter chipPresenter;
    private BetPresenter betPresenter;

    public void Awake()
    {
        //sceneRoot = Instantiate(sceneRootPrefab);

        //uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

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

        betPresenter = new BetPresenter(new BetModel(3, 69, bankPresenter), viewContainer.GetView<BetView>());
        betPresenter.Initialize();

        pseudoChipPresenter = new PseudoChipPresenter(new PseudoChipModel(bankPresenter, betPresenter, soundPresenter), viewContainer.GetView<PseudoChipView>());
        pseudoChipPresenter.Initialize();

        chipPresenter = new ChipPresenter(new ChipModel(soundPresenter), viewContainer.GetView<ChipView>());
        chipPresenter.Initialize();

        ActivateTransferEvents();
        ActivateEvents();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();
        sceneRoot.Activate();
    }

    private void ActivateEvents()
    {
        betPresenter.OnCountForMaxBet += chipPresenter.SpawnNumbers;

        pseudoChipPresenter.OnSpawnChip += chipPresenter.SpawnChip;
        chipPresenter.OnAddChip += betPresenter.AddBet;
        chipPresenter.OnRemoveChip += betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips += betPresenter.ClearBet; 
    }

    private void ActivateTransferEvents()
    {

    }

    private void DeactivateEvents()
    {
        pseudoChipPresenter.OnSpawnChip -= chipPresenter.SpawnChip;
        chipPresenter.OnAddChip -= betPresenter.AddBet;
        chipPresenter.OnRemoveChip -= betPresenter.RemoveBet;
        chipPresenter.OnRemoveAllChips -= betPresenter.ClearBet;
    }

    private void DeactivateTransferEvents()
    {

    }

    private void Dispose()
    {
        DeactivateEvents();
        DeactivateTransferEvents();
        sceneRoot?.Deactivate();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();

        pseudoChipPresenter?.Dispose();
        chipPresenter?.Dispose();
    }
}
