using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviourSingletonPersistent<UIManager>
{
    static PageEnum currentPage = PageEnum.Login;
    public event Action<Place, Sprite> OnDisplayPlace;
    

    public void DisplayPlaceView(Place place, Sprite placeImage)
    {
        OnDisplayPlace?.Invoke(place, placeImage);
    }
    
    public static void GoToPage(PageEnum page)
    {
        SceneManager.LoadScene(page.ToString(), LoadSceneMode.Additive);
        
        UnloadAllScenes();

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