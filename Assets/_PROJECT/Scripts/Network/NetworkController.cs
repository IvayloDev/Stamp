using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Network;
using UnityEngine;

public static class NetworkController
{
    public static async Task<Places[]> GetAllPlaces()
    {
        var entriesSerialized = await NetworkManager.Instance.Get
        (
            Consts.GetPlacesEndpoint
        );
        
        Places[] placesDesirialized = JsonHelper.getJsonArray<Places>(entriesSerialized);
        return placesDesirialized;
    }
    
    
}
