using System;
using _PROJECT.Scripts.Systems;
using TMPro;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public TextMeshProUGUI userDetailsText;


    void Start()
    {
        PlacesController.OnPlacesFetched += OnPlacesFetched;
        
        userDetailsText.text = Client.Instance.User.DisplayName + " ~ " + Client.Instance.User.Email;
        
    }

    private void OnPlacesFetched(Place place)
    {
        //Open the deep link id
        UIManager.Instance.DisplayPlaceView(place);
    }

    private void OnDestroy()
    {
        PlacesController.OnPlacesFetched -= OnPlacesFetched;
    }


    public void Logout()
    {
        FirebaseManager.Instance.SignOut();
        
        UIManager.GoToPage(PageEnum.Login);
    }
}
