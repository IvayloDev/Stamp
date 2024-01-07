using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlaceScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject placeViewHolder;

    [SerializeField]
    private TextMeshProUGUI placeName;
    
    [SerializeField]
    private TextMeshProUGUI placeDescShort;
    
    [SerializeField]
    private TextMeshProUGUI placeDescLong;
    
    [SerializeField]
    private Image placeImage;

    void Start()
    {
        placeViewHolder.SetActive(false);
        
        UIManager.Instance.OnDisplayPlace += OnDisplayPlaceScreen;
    }

    private void OnDisplayPlaceScreen(Place obj, Sprite placeImg)
    {
        placeViewHolder.SetActive(true);
        placeName.text = obj.name;
        placeDescShort.text = obj.description.Substring(0, 20);
        placeDescLong.text = obj.description.Substring(0, 50);
        placeImage.sprite = placeImg;
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnDisplayPlace -= OnDisplayPlaceScreen;
    }


    public void DisableDetailsView()
    {
        placeViewHolder.SetActive(false);
    }

}
