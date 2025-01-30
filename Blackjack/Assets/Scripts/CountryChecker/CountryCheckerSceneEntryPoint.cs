using System;
using UnityEngine;

public class CountryCheckerSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UICountryCheckerSceneRoot sceneRootPrefab;

    private UICountryCheckerSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private GeoLocationPresenter geoLocationPresenter;
    private InternetPresenter internetPresenter;
    private SoundPresenter soundPresenter;

    public void Run(UIRootView uIRootView)
    {
        Debug.Log("OPEN COUNTRY CHECKER SCENE");

        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        geoLocationPresenter = new GeoLocationPresenter(new GeoLocationModel());

        internetPresenter = new InternetPresenter(new InternetModel(), viewContainer.GetView<InternetView>());
        internetPresenter.Initialize();

        Debug.Log("Success");

        ActivateActions();

        Debug.Log("Success");

        internetPresenter.StartCheckInternet();

    }

    public void Dispose()
    {
        DeactivateActions();

        internetPresenter?.Dispose();
    }

    private void ActivateActions()
    {
        internetPresenter.OnInternetUnavailable += TransitionToMainMenu;
        internetPresenter.OnInternetAvailable += geoLocationPresenter.GetUserCountry;

        geoLocationPresenter.OnGetCountry += ActivateSceneInCountry;
    }

    private void DeactivateActions()
    {
        internetPresenter.OnInternetUnavailable -= TransitionToMainMenu;
        internetPresenter.OnInternetAvailable -= geoLocationPresenter.GetUserCountry;

        geoLocationPresenter.OnGetCountry -= ActivateSceneInCountry;
    }

    private void ActivateSceneInCountry(string country)
    {
        switch (country)
        {
            case "AU":
                TransitionToOther();
                break;
            case "DE":
                TransitionToOther();
                break;
            case "IT":
                TransitionToOther();
                break;
            case "AT":
                TransitionToOther();
                break;
            case "FR":
                TransitionToOther();
                break;
            default:
                TransitionToMainMenu();
                break;
        }
    }

    #region Input

    public event Action GoToMainMenu;
    public event Action GoToOther;

    private void TransitionToMainMenu()
    {
        Dispose();
        Debug.Log("NO GOOD");
        GoToMainMenu?.Invoke();
    }

    private void TransitionToOther()
    {
        Dispose();
        Debug.Log("GOOD");
        GoToOther?.Invoke();
    }

    #endregion
}
