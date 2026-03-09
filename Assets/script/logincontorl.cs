using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginController : MonoBehaviour
{
    public TMP_InputField usernameOrEmailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public TextMeshProUGUI messageText;

    void Start()
    {
        if (loginButton != null)
            loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    void OnLoginButtonClicked()
    {
        string user = usernameOrEmailInput.text;
        string pass = passwordInput.text;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            messageText.text = "Please enter username/email and password.";
            return;
        }

        StartCoroutine(Login(user, pass));
    }

    IEnumerator Login(string usernameOrEmail, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameOrEmail);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://paveway.fun/login_action.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Login response: " + responseText);

                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                if (loginResponse != null && loginResponse.status == "success")
                {
                    PlayerPrefs.SetString("Username", loginResponse.username);
                    PlayerPrefs.SetString("UserEmail", loginResponse.email);
                    PlayerPrefs.Save();

                    // Load homepage scene after successful login
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    messageText.text = loginResponse != null ? loginResponse.message : "Login failed.";
                }
            }
            else
            {
                messageText.text = "Network error: " + www.error;
            }
        }
    }

    [System.Serializable]
    public class LoginResponse
    {
        public string status;
        public string username;
        public string email;
        public string message;
    }
}
