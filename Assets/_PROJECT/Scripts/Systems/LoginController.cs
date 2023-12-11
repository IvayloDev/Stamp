using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine;

namespace _PROJECT.Scripts.Systems
{
    public class LoginController : MonoBehaviour
    {
        [Header("Login")]
        [SerializeField]
        private TMP_InputField emailLoginField;
        [SerializeField]
        private TMP_InputField passwordLoginField;
    
        [Header("Register")]
        [SerializeField]
        private TMP_InputField emailRegisterField;
        [SerializeField]
        private TMP_InputField passwordRegisterField;
        [SerializeField]
        private TMP_InputField usernameRegisterField;
        [SerializeField] 
        private TMP_InputField passwordRegisterVerifyField;
    
        [Header("Error")] 
        [SerializeField]
        private TextMeshProUGUI errorMessageText;

        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        //Function for the login button
        public void LoginButton()
        {
            //Call the login coroutine passing the email and password
            StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
        }
    
        //Function for the register button
        public void RegisterButton()
        {
            //Call the register coroutine passing the email, password, and username
            StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
        }

        public void GoogleLoginButton()
        {
            FirebaseGoogleSignIn.Instance.GoogleSignInButtonClick();
        }
        
        private IEnumerator Login(string _email, string _password)
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> LoginTask = FirebaseManager.Instance.auth.SignInWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            if (LoginTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogError(message: $"Failed to register task with {LoginTask.Exception}");
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Login Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong Password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        break;
                    case AuthError.UserNotFound:
                        message = "Account does not exist";
                        break;
                }
                errorMessageText.text = message;
            }
            else
            {
                //User is now logged in
                //Now get the result
                Client.Instance.User = LoginTask.Result.User;
                Debug.LogFormat("User signed in successfully: {0} ({1})", LoginTask.Result.User.DisplayName, LoginTask.Result.User.Email);
            
                UIManager.GoToPage(PageEnum.Main);
                errorMessageText.text = "";
            }
        }
        
        private IEnumerator Register(string _email, string _password, string _username)
        {
            Match match = emailRegex.Match(_email);
        
            if (!match.Success)
            {
                errorMessageText.text = "Invalid Email";

            }
            else if (_username == "")
            {
                //If the username field is blank show a warning
                errorMessageText.text = "Missing Username";
            }
            else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
            {
                //If the password does not match show a warning
                errorMessageText.text = "Password Does Not Match!";
            }
            else
            {
                //Call the Firebase auth signin function passing the email and password
                Task<AuthResult> RegisterTask = FirebaseManager.Instance.auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
                //Wait until the task completes
                yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

                if (RegisterTask.Exception != null)
                {
                    //If there are errors handle them
                    Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                    FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

                    string message = "Register Failed!";
                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            message = "Missing Email";
                            break;
                        case AuthError.MissingPassword:
                            message = "Missing Password";
                            break;
                        case AuthError.WeakPassword:
                            message = "Weak Password";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            message = "Email Already In Use";
                            break;
                    }

                    errorMessageText.text = message;
                }
                else
                {
                    //User has now been created
                    //Now get the result
                    Client.Instance.User = RegisterTask.Result.User;

                    if (Client.Instance.User != null)
                    {
                        //Create a user profile and set the username
                        UserProfile profile = new UserProfile {DisplayName = _username};

                        //Call the Firebase auth update user profile function passing the profile with the username
                        Task ProfileTask = Client.Instance.User.UpdateUserProfileAsync(profile);
                        //Wait until the task completes
                        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                        if (ProfileTask.Exception != null)
                        {
                            //If there are errors handle them
                            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                            FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                            AuthError errorCode = (AuthError) firebaseEx.ErrorCode;
                            errorMessageText.text = "Username Set Failed!";
                        }
                        else
                        {
                            //Username is now set
                            //Now return to login screen
                            Debug.Log("Registered !");
                        
                            UIManager.GoToPage(PageEnum.Main);
                            errorMessageText.text = "";
                        }
                    }
                }
            }
        }
        
    }
}