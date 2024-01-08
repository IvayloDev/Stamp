using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using distriqt.plugins.nfc;
using UnityEngine;

public class PlacesController : MonoBehaviour
{
    [SerializeField]
    private GameObject activePlacePrefab;
    
    [SerializeField]
    private Transform placesParentTransform;
  
    private Place[] places;
    
    public static event Action<Place> OnPlacesFetched;
    
    // Start is called before the first frame update
    void Start()
    {
        FetchAndPopulatePlaces();
    }

    async Task FetchAndPopulatePlaces()
    {
        places = await NetworkController.GetAllPlaces();

        for (int i = 0; i < places.Length; i++)
        {
            if (i >= 10)
                break;
            
            GameObject _place = Instantiate(activePlacePrefab, Vector3.zero, Quaternion.identity, placesParentTransform);
            _place.transform.SetParent(placesParentTransform, false);
            _place.GetComponent<PlaceComponent>()?.Initialize(places[i]);
        }

        if (string.IsNullOrEmpty(Consts.ComingFromDeepLink) == false)
        {
            Place placeFromDeepLink = places.FirstOrDefault(place => place.id.ToString() == Consts.ComingFromDeepLink);

            //Find the place with the matching ID
            OnPlacesFetched?.Invoke(placeFromDeepLink);
        }

    }

    
   
}
