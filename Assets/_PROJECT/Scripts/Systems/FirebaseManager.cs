using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Managers;
using TMPro;
using UnityEngine;

namespace _PROJECT.Scripts.Systems
{
    public class FirebaseManager : MonoBehaviourSingletonPersistent<FirebaseManager>
    {
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;

        [SerializeField] private bool autoLogin = true;


        void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    InitializeFirebase();
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                }
                else
                {
                    Debug.LogError("Could not resolve all firebase dependencies" + dependencyStatus);
                }
            });
        }

        void InitializeFirebase()
        {
            auth = FirebaseAuth.DefaultInstance;
            
            Debug.Log("Firebase Initialized !");

            if (autoLogin && auth.CurrentUser != null)
            {
                Client.Instance.User = auth.CurrentUser;
                
                Debug.Log("AutoLog User: " + Client.Instance.User.DisplayName + " email: " + Client.Instance.User.Email);

            }
        }

        public void Logout()
        {
            auth.SignOut();
        }

    }
}
