using UnityEngine;
using UnityEngine.UI;

public class OpenWebsiteButton1 : MonoBehaviour
{
    public Button loginButton; // Assign this in the Inspector
    public string targetUrl = "http://192.168.100.127/image_project/in.php";

    void Start()
    {
        if (loginButton != null)
        {
            loginButton.onClick.AddListener(OpenWebPage);
        }
        else
        {
            Debug.LogError("Login button is not assigned in the Inspector!");
        }
    }

    void OpenWebPage()
    {
        Debug.Log("Opening URL: " + targetUrl);
        Application.OpenURL(targetUrl);
    }
}
