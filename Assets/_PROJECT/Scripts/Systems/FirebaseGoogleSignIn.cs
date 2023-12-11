using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _PROJECT.Scripts.Systems;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using Managers;
using TMPro;
using UnityEngine;

public class FirebaseGoogleSignIn : MonoBehaviourSingletonPersistent<FirebaseGoogleSignIn>
{

    public void GoogleSignInButtonClick()
    {
         Debug.LogError("GoogleSignInButtonClick !! ");

        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            RequestEmail = true,
            WebClientId = "385798430792-8vifpqbav8rq2uabmcm5vhsgh2o5o545.apps.googleusercontent.com"
        };

        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

        Debug.LogError("GoogleSignInButtonClick 22222!! ");

        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();

        // Ensure the continuation runs on the main thread
        signIn.ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("GoogleSignInButtonClick 33333!! ");
                signInCompleted.SetCanceled();
            }
            else if (task.IsFaulted)
            {
                signInCompleted.SetException(task.Exception);
                Debug.LogError("GoogleSignInButtonClick 4444!! ");
            }
            else
            {
                Debug.LogError("GoogleSignInButtonClick 55555!! ");

                Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
                
                FirebaseManager.Instance.auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(authTask =>
                {
                    if (authTask.IsCanceled)
                    {
                        signInCompleted.SetCanceled();
                        Debug.LogError("GoogleSignInButtonClick 6666!! ");
                    }
                    else if (authTask.IsFaulted)
                    {
                        signInCompleted.SetException(authTask.Exception);
                        Debug.LogError("GoogleSignInButtonClick 77777!! ");
                    }
                    else
                    {
                        signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);
                        Debug.LogError("USER LOGGED !! " + authTask.Result.Email);
                        Client.Instance.User = authTask.Result;
                        UIManager.GoToPage(PageEnum.Main);
                    }
                });
            }
        });
    }
    
}
