using UnityEngine;
using UnityEngine.SceneManagement;

public class DashboardImageButton : MonoBehaviour
{
    public string imageName;
    public string armapSceneName;

    public void OnImageClick()
    {
        PlayerPrefs.SetString("selected_image", imageName);

        string username = PlayerPrefs.GetString("current_user", "");
        if (string.IsNullOrEmpty(username))
        {
            username = "guest";
            PlayerPrefs.SetString("current_user", username);
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene(armapSceneName);
    }
}
