using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;
    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalGameSettings();

        instance = new GameEntryPoint();
        instance.Run();

    }

    private static void GlobalGameSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartCountryChecker()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.COUNTRY_CHECKER);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<CountryCheckerSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToOther += () => coroutines.StartCoroutine(LoadAndStartOther());
        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartOther()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.OTHER);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<OtherSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MENU);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnTransitionToGameScene += () => coroutines.StartCoroutine(LoadAndStartGameScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameScene()
    {
        rootView.SetLoadScreen(1);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnTransitionToMenuScene += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.OnTransitionToGameScene += () => coroutines.StartCoroutine(LoadAndStartGameScene());

        yield return rootView.HideLoadingScreen();
    }

    
    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
