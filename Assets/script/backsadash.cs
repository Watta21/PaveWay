using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using TMPro; // for TextMeshProUGUI

public class ARMAPBackHandler : MonoBehaviour
{
    [Header("Scene Names")]
    public string dashboardScene = "Dashboard";
    public string ratingScene = "RatingScene";

    [Header("Server URL")]
    public string phpURL = "https://paveway.fun/submit_rating.php";

    [Header("Current User & Image Info")]
    public TextMeshProUGUI usernameText; // drag your TMP Text here
    public string selectedImage;

    private string currentUsername => usernameText.text.Trim(); // auto get username from text

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(CheckRatedThenGoBack());
        }
    }

    IEnumerator CheckRatedThenGoBack()
    {
        // Validate input
        if (string.IsNullOrEmpty(currentUsername) || string.IsNullOrEmpty(selectedImage))
        {
            Debug.LogWarning("Username or image not set, going to rating scene");
            SceneManager.LoadScene(ratingScene);
            yield break;
        }

        // Prepare server request
        WWWForm form = new WWWForm();
        form.AddField("username", currentUsername);
        form.AddField("image_name", selectedImage);

        using (UnityWebRequest www = UnityWebRequest.Post(phpURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Server error: " + www.error);
                SceneManager.LoadScene(ratingScene);
                yield break;
            }

            // Parse server response
            RatingCheck result = JsonUtility.FromJson<RatingCheck>(www.downloadHandler.text);

            // Decide scene based on server data
            if (result.user_rating > 0)
            {
                // User already rated → go to dashboard
                SceneManager.LoadScene(dashboardScene);
            }
            else
            {
                // User has not rated → go back to rating scene
                SceneManager.LoadScene(ratingScene);
            }
        }
    }

    [System.Serializable]
    public class RatingCheck
    {
        public int user_rating;
    }
}
