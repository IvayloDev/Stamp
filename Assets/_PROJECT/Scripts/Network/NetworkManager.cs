using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Managers;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviourSingletonPersistent<NetworkManager>
{
    public static event Action<bool> OnAwaitingResponse;

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
        {
            var request = new UnityWebRequest(path, type.ToString());

            if (data != null)
            {
                var json = JsonUtility.ToJson(data);
                var bodyRaw = Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }
            
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }
    
    
    public async Task<string> SendServerRequest(string path, RequestType type, object data = null)
    {
        OnAwaitingResponse?.Invoke(true);
        
        var request = CreateRequest(path, type, data);
        Debug.Log($"Sending {type} request to {path}");
        request.SendWebRequest();
        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(
                " failed with status " +
                request.responseCode +
                ", handling the error \n" + "full error: \n" +
                request.downloadHandler.text
            );
                
            // handle the error
            Debug.LogError("error: " + request);
            
            OnAwaitingResponse?.Invoke(false);

            return null;
            //TODO return await NetworkErrorHandler.ServerRequestErrorHandler(endpoint, request);
        }
        else
        {
            Debug.Log($"Request to {request.url} succeeded with status {request.responseCode}");
            var response = request.downloadHandler.text;
            request.Dispose();
            
            OnAwaitingResponse?.Invoke(false);

            return response;
        }
    }
    
    public async Task<string> Post(string path, object data = null)
    {
        return await SendServerRequest (path, RequestType.POST, data: data);
    }

    public async Task<string> Get(string path)
    {
        return await SendServerRequest(path, RequestType.GET);
    }

    public async Task<string> Put(string path, object data = null)
    {
        return await SendServerRequest(path, RequestType.PUT, data: data);
    }
    
    public async Task<Texture2D> DownloadImage(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            var asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield(); // Yield control until the operation is done
            }

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                return ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            }
            else
            {
                Debug.LogError("Failed to download image. Error: " + webRequest.error);
                return null;
            }
        }
    }
    
    
    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE
        
    }
}
