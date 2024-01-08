using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts
{
    
    public const string ServerUrl = "https://bobapi.kaukov.dev";

    public const string GetUsersEndpoint = ServerUrl + "/users";
    public const string GetPlacesEndpoint = ServerUrl + "/places";
    
    //Scenes
    public const string LoaderPanelSceneName = "LoaderScene";
    
    //Deep linking
    public static string ComingFromDeepLink = "";

}
