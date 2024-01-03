using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static PageEnum currentPage = PageEnum.Login;
    public static event Action<Place> OnDisplayPlace;


    public static void DisplayPlaceView(Place place)
    {
        Debug.Log(place.id);
        OnDisplayPlace?.Invoke(place);
    }
    
    public static void GoToPage(PageEnum page)
    {
        UnloadAllScenes();
        
        SceneManager.LoadScene(page.ToString(), LoadSceneMode.Additive);
        
        currentPage = page;
    }

    public static void UnloadAllScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            //Unload everything except the loader scene
            if (SceneManager.GetSceneAt(i).isLoaded && SceneManager.GetSceneAt(i).name != PageEnum.LoaderScene.ToString())
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }
    }
}

public enum PageEnum
{
    Login,
    Main,
    Splash,
    LoaderScene
}