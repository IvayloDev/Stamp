using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlaceScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject placeViewHolder;
    
    void Start()
    {
        placeViewHolder.SetActive(false);
        
        UIManager.OnDisplayPlace += OnDisplayPlaceScreen;
    }

    private void OnDisable()
    {
        UIManager.OnDisplayPlace -= OnDisplayPlaceScreen;
    }
    
    private void OnDisplayPlaceScreen(Place obj)
    {
        Debug.LogError("tuk ???");
        placeViewHolder.SetActive(true);
    }

}
