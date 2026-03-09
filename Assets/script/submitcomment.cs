using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class SubmitComment : MonoBehaviour
{
    public TMP_InputField commentInputField;
    public TextMeshProUGUI usernameInputField; // Input for the username
    public string imageName = "sss"; // You can set this per image

    public void OnSubmitComment()
    {
        Debug.Log("Submit button clicked");

        string commentText = commentInputField.text;
        string username = usernameInputField.text;

        if (!string.IsNullOrEmpty(commentText) && !string.IsNullOrEmpty(username))
        {
            StartCoroutine(SendCommentToServer(imageName, commentText, username));
        }
        else
        {
            Debug.LogWarning("Comment or Username is empty");
        }
    }

    IEnumerator SendCommentToServer(string imageName, string comment, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("image_name", imageName);
        form.AddField("comment", comment);
        form.AddField("username", username);

        string url = "https://paveway.fun/submit_comment.php.php"; // Make sure the filename is correct
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Debug.Log("Server Response: " + www.downloadHandler.text);
        }
    }
}
