using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlacesController : MonoBehaviour
{
    [SerializeField]
    private GameObject activePlacePrefab;
    
    [SerializeField]
    private Transform placesParentTransform;
    
    [SerializeField]
    private GameObject activePlaceGO;
    
    private Place[] places;
    
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
    }

    public void OpenAndPopulatePlaceView()
    {
        activePlaceGO.SetActive(true);
    }
   
}
