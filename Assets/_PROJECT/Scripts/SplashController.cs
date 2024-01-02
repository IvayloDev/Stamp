using _PROJECT.Scripts.Systems;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    [SerializeField]
    private bool autoLogin = true;

    public void Start()
    {
        SceneManager.LoadSceneAsync(Consts.LoaderPanelSceneName, LoadSceneMode.Additive);
    }
    
    public void Continue()
    {
        if (autoLogin && Client.Instance.User != null)
        {
            UIManager.GoToPage(PageEnum.Main);
        }
        else
        {
            UIManager.GoToPage(PageEnum.Login);
        }
    }
}
