using _PROJECT.Scripts.Systems;
using TMPro;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public TextMeshProUGUI userDetailsText;


    void Start()
    {
        userDetailsText.text = Client.Instance.User.DisplayName + " ~ " + Client.Instance.User.Email;
    }
    
    
    public void Logout()
    {
        FirebaseManager.Instance.SignOut();
        
        UIManager.GoToPage(PageEnum.Login);
    }
}
