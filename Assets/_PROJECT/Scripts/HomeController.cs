using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _PROJECT.Scripts.Systems;
using TMPro;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public TextMeshProUGUI userDetailsText;

    void Start()
    {
        userDetailsText.text = Client.Instance.User.DisplayName + " ~ " + Client.Instance.User.Email;

        FetchPlaces();
    }

    async Task FetchPlaces()
    {
        Places[] places = await NetworkController.GetAllPlaces();
        
        Debug.LogError("Places count: " + places.Length);
    }
    
    
    public void Logout()
    {
        FirebaseManager.Instance.SignOut();
    }
}
