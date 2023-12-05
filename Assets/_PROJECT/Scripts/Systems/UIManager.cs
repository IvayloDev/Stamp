using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static PageEnum currentPage = PageEnum.Login;

    public static void GoToPage(PageEnum page)
    {
        SceneManager.LoadScene(page.ToString(), LoadSceneMode.Single);
        currentPage = page;
    }
}

public enum PageEnum
{
    Login,
    Main,
}