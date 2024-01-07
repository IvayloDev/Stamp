using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceComponent : MonoBehaviour
{
    private Place currentPlace;
    [SerializeField] private Image placeImage;
    [SerializeField] private TextMeshProUGUI placeName;
    [SerializeField] private TextMeshProUGUI placeShortDesc;
    [SerializeField] private TextMeshProUGUI placeFullDesc;
    [SerializeField] private TextMeshProUGUI placeCardRequirement;
    [SerializeField] private TextMeshProUGUI availableStamps;

    public async Task Initialize(Place place)
    {
        currentPlace = place;
        
        SetImage();
        
        placeName.text = place.name;
        placeShortDesc.text = place.description.Substring(0, 40);
        placeFullDesc.text = place.description.Substring(0, 40);
        placeCardRequirement.text = place.stampPrice.ToString();
        //TODO availableStamps.text
    }

    private async Task SetImage()
    {
        Texture2D texture = await NetworkManager.Instance.DownloadImage("https://picsum.photos/200");

        if (placeImage != null)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            placeImage.sprite = sprite;
        }
    }

    public void OnMapsButtonClick()
    {
        Application.OpenURL("http://maps.google.com/maps?q=" + "42.14945613239546, 24.748751638915294");
    }

    public void OnOpenCardViewClick()
    {
        UIManager.Instance.DisplayPlaceView(currentPlace, placeImage.sprite);
    }
    
}
