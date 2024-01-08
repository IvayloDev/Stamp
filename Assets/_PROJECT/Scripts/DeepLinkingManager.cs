using System.Collections;
using System.Collections.Generic;
using _PROJECT.Scripts.Systems;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DeepLinkingManager : MonoBehaviourSingletonPersistent<DeepLinkingManager>
{
    [SerializeField]
    private SplashController _splashController;
    
    private void Awake()
    {
        Application.deepLinkActivated += onDeepLinkActivated;
        if (!string.IsNullOrEmpty(Application.absoluteURL))
        {
            // Cold start and Application.absoluteURL not null so process Deep Link.
            onDeepLinkActivated(Application.absoluteURL);
        }
    }
 
    private void onDeepLinkActivated(string url)
    {
        string placeId = url.Split('=')[^1];

        Consts.ComingFromDeepLink = placeId;

        if (_splashController != null)
        {
            //NFC from outside the app
            Invoke(nameof(Continue),1); //TODO Refactor!
        }
        else if(Client.Instance.User != null)
        {
            //NFC from inside the app
            UIManager.GoToPage(PageEnum.Main);
        }
    }

    void Continue()
    {
        _splashController.Continue();
    }
}
