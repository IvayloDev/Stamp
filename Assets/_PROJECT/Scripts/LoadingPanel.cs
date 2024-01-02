using System;
using Managers;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingPanelGO;

    void Start()
    {
        NetworkManager.onAwaitingResponse += OnAwaitingResponse;
    }

    private void OnDestroy()
    {
        NetworkManager.onAwaitingResponse -= OnAwaitingResponse;
    }
    
    private void OnAwaitingResponse(bool response)
    {
        AwaitingResponse(response);
    }

    void AwaitingResponse(bool response)
    {
        loadingPanelGO.SetActive(response);
    }


}
