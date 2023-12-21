using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Network;
using UnityEngine;

public static class NetworkController
{
    public static async Task<Place[]> GetAllPlaces()
    {
        var entriesSerialized = await NetworkManager.Instance.Get
        (
            Consts.GetPlacesEndpoint
        );
        
        Place[] placesDesirialized = JsonHelper.getJsonArray<Place>(entriesSerialized);
        return placesDesirialized;
    }
    
    
}
