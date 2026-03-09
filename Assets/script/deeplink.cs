using UnityEngine;

public class DeepLinkHandler : MonoBehaviour
{
    void Start()
    {
        string link = Application.absoluteURL;

        if (!string.IsNullOrEmpty(link))
        {
            Debug.Log("Opened from deep link: " + link);
            HandleDeepLink(link);
        }
    }

    void HandleDeepLink(string url)
    {
        if (url.Contains("PaveWay://login"))
        {
            // Open your login page inside the Unity app
            // Example: SceneManager.LoadScene("LoginScene");
        }
    }
}
