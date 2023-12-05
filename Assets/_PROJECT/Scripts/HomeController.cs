using System.Collections;
using System.Collections.Generic;
using _PROJECT.Scripts.Systems;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public void Logout()
    {
        FirebaseManager.Instance.Logout();
    }
}
