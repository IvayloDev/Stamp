using System;
using System.Threading.Tasks;
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

    private void OnDisplayPlaceScreen(Place obj)
    {
        placeViewHolder.SetActive(true);
        placeName.text = obj.name;
        placeDescShort.text = obj.description.Substring(0, 20);
        placeDescLong.text = obj.description.Substring(0, 50);

        if (obj.placeSprite != null)
        {
            placeImage.sprite = obj.placeSprite;
            return;
        }

        SetImage(obj);
    }
    
    async Task SetImage(Place place)
    {
        Texture2D texture = await NetworkManager.Instance.DownloadImage($"https://picsum.photos/id/{place.id}/400/150");

        if (placeImage != null)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            placeImage.sprite = sprite;
        }
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
