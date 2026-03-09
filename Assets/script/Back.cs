using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToWebsite : MonoBehaviour
{
    public string websiteURL = "https://imagesite.42web.io/index.php";
    public string sceneName = "YourSceneName"; // Specify your scene name here

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.OpenURL(websiteURL); // Opens website in browser
            SceneManager.LoadScene(sceneName); // Loads the specified scene
            // Application.Quit(); // Uncomment this line if you want to quit after loading the scene
        }
    }
}
