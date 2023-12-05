using Firebase.Auth;
using Managers;

namespace _PROJECT.Scripts.Systems
{
    public class Client : MonoBehaviourSingletonPersistent<Client>
    {
        public FirebaseUser User;
    }
}