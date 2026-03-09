using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class RatingSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI avgRatingText;
    public TextMeshProUGUI totalUsersText;
    public TextMeshProUGUI totalStarsText;

    [Header("User Info")]
    public TextMeshProUGUI usernameText;
    private string username => usernameText.text;

    [Header("Image Info")]
    public string imageName = "image1";

    [Header("Server URL")]
    public string phpURL = "https://paveway.fun/submit_rating.php";

    [Header("Buttons")]
    public Button rateContinueButton;
    public Button cancelButton;

    private int lastRating = 0;
    private bool keepUpdating = true;

    private void Start()
    {
        // Start realtime updates
        StartCoroutine(RealtimeRatingUpdate());

        // Continue
        rateContinueButton.onClick.AddListener(RateAndContinue);

        // Cancel
        cancelButton.onClick.AddListener(OnCancelClicked);
    }

    // User clicks rating button
    public void ToggleRating(int rating)
    {
        lastRating = rating;
    }

    // Cancel → NO RATING → return dashboard
    public void OnCancelClicked()
    {
        keepUpdating = false;
        SceneManager.LoadScene("SampleScene");
    }

    // Submit rating for the logged-in user
    public void RateAndContinue()
    {
        if (lastRating == 0)
        {
            Debug.Log("User has not rated yet");
            return;
        }

        StartCoroutine(SendRating(lastRating));
    }

    IEnumerator SendRating(int rating)
    {
        WWWForm form = new WWWForm();
        form.AddField("image_name", imageName);
        form.AddField("username", username);
        form.AddField("rating", rating);

        using (UnityWebRequest www = UnityWebRequest.Post(phpURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Rating submitted!");

                // Stop updates and return to dashboard
                keepUpdating = false;
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                Debug.LogError("Error submitting rating: " + www.error);
            }
        }
    }

    // 🔥 REALTIME UPDATE LOOP
    IEnumerator RealtimeRatingUpdate()
    {
        while (keepUpdating)
        {
            yield return GetRating();   // refresh values
            yield return new WaitForSeconds(1f); // update every 1 second
        }
    }

    // 🔥 Update UI from server
    IEnumerator GetRating()
    {
        WWWForm form = new WWWForm();
        form.AddField("image_name", imageName);
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(phpURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                RatingData data = JsonUtility.FromJson<RatingData>(www.downloadHandler.text);

                if (data != null)
                {
                    avgRatingText.text = "" + data.avg_rating;
                    totalUsersText.text = "(" + data.total_users + ")";
                    totalStarsText.text = ""+ data.total_stars;
                }
            }
            else
            {
                Debug.LogError("Error fetching rating: " + www.error);
            }
        }
    }

    [System.Serializable]
    public class RatingData
    {
        public float avg_rating;
        public int total_users;
        public int total_stars;
        public int user_rating;
    }
}
