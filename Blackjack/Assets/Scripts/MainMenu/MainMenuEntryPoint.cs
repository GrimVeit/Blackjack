using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private AvatarPresenter avatarPresenter;
    private AvatarPresenter avatarPresenterChanges;
    private NicknamePresenter nicknamePresenter;

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

        nicknamePresenter = new NicknamePresenter
            (new NicknameModel(PlayerPrefsKeys.NICKNAME, soundPresenter),
            viewContainer.GetView<NicknameView>());

        avatarPresenter = new AvatarPresenter
            (new AvatarModel(PlayerPrefsKeys.IMAGE_INDEX, soundPresenter),
            viewContainer.GetView<AvatarView>("Main"));

        avatarPresenterChanges = new AvatarPresenter
            (new AvatarModel(PlayerPrefsKeys.IMAGE_INDEX, soundPresenter),
            viewContainer.GetView<AvatarView>("Changes"));

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();


        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        nicknamePresenter.Initialize();
        avatarPresenter.Initialize();
        avatarPresenterChanges.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();
    }

    private void ActivateTransitionsSceneEvents()
    {

    }

    private void DeactivateTransitionsSceneEvents()
    {

    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        nicknamePresenter?.Dispose();
        avatarPresenter?.Dispose();
        avatarPresenterChanges?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action GoToSoloGame_Action;
    public event Action GoToBotGame_Action;
    public event Action GoToFriendGame_Action;


    private void HandleGoToSoloGame()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        GoToSoloGame_Action?.Invoke();
    }

    private void HandleGoToBotGame()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        GoToBotGame_Action?.Invoke();
    }

    private void HandlerGoToFriendGame()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        GoToFriendGame_Action?.Invoke();
    }

    #endregion
}
