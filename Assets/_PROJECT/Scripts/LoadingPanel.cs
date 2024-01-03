using System;
using Managers;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingPanelGO;

    void Start()
    {
        NetworkManager.OnAwaitingResponse += OnAwaitingResponse;
    }

    private void OnDestroy()
    {
        NetworkManager.OnAwaitingResponse -= OnAwaitingResponse;
    }
    
    private void OnAwaitingResponse(bool response)
    {
        loadingPanelGO.SetActive(response);
    }

}
