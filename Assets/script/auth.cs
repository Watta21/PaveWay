using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    [Header("Register UI")]
    public TMP_InputField regUsername;
    public TMP_InputField regPassword;

    [Header("Login UI")]
    public TMP_InputField logUsername;
    public TMP_InputField logPassword;

    [Header("Status")]
    public TMP_Text statusText;

    private string registerURL = "http://localhost/unity/register.php";
    private string loginURL = "http://localhost/unity/login.php";

    public void RegisterUser()
    {
        StartCoroutine(RegisterCoroutine());
    }

    public void LoginUser()
    {
        StartCoroutine(LoginCoroutine());
    }

    IEnumerator RegisterCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", regUsername.text);
        form.AddField("password", regPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post(registerURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                statusText.text = "Error: " + www.error;
            }
            else
            {
                statusText.text = www.downloadHandler.text;
            }
        }
    }

    IEnumerator LoginCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", logUsername.text);
        form.AddField("password", logPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                statusText.text = "Error: " + www.error;
            }
            else
            {
                statusText.text = www.downloadHandler.text;
                if (www.downloadHandler.text == "SUCCESS")
                {
                    Debug.Log("Login successful!");
                    // Load next scene here
                }
            }
        }
    }
}
