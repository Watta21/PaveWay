using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginRegisterForgot : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField usernameInput;          // Register only
    public TMP_InputField emailInput;             // Login & Register
    public TMP_InputField passwordInput;          // Password input field
    public TMP_InputField confirmPasswordInput;   // Register only 
    public TMP_Text messageText;

    [Header("Forgot Password UI")]
    public TMP_InputField forgotEmailInput;
    public TMP_Text forgotPasswordMessageText;

    [Header("Reset Password UI")]
    public TMP_InputField resetPasswordInput;         // New password
    public TMP_InputField resetConfirmPasswordInput;  // Confirm new password
    public TMP_Text resetPasswordMessageText;

    private string verifiedEmail = ""; // Store verified email after forgot password success    

    [Header("PHP URLs")]
    public string loginURL = "https://paveway.fun/login.php";
    public string registerURL = "https://paveway.fun/register.php";
    public string forgotPasswordURL = "https://paveway.fun/forgot_password.php";
    public string resetPasswordURL = "https://paveway.fun/reset_password.php";

    [Header("Password Visibility Toggle")]
    public GameObject passwordToggleButton; // Button to toggle password visibility
    public GameObject confirmPasswordToggleButton; // Button to toggle confirm password visibility

    private void Awake()
    {
        // Make sure all TMP fields are safe before use
        ForceSafeTMPUpdate();
    }

    private void ForceSafeTMPUpdate()
    {
        TMP_InputField[] inputs = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var input in inputs)
        {
            input.ForceLabelUpdate();
        }
    }

    // ---------------------------
    // BUTTON EVENT HANDLERS
    // ---------------------------

    public void OnLogin()
    {
        if (!CheckUIAssignments(false)) return;
        StartCoroutine(Login());
    }

    public void OnRegister()
    {
        if (!CheckUIAssignments(true)) return;
        StartCoroutine(Register());
    }

    public void OnForgotPassword()
    {
        if (forgotEmailInput == null || forgotPasswordMessageText == null)
        {
            Debug.LogError("Forgot Password UI references missing!");
            return;
        }

        string email = forgotEmailInput.text.Trim();
        if (string.IsNullOrEmpty(email))
        {
            forgotPasswordMessageText.text = "Enter your email.";
            return;
        }

        StartCoroutine(ForgotPassword(email));
    }

    public void OnResetPassword()
    {
        if (resetPasswordInput == null || resetConfirmPasswordInput == null || resetPasswordMessageText == null)
        {
            Debug.LogError("Reset Password UI references missing!");
            return;
        }

        string newPass = resetPasswordInput.text;
        string confirmPass = resetConfirmPasswordInput.text;

        if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
        {
            resetPasswordMessageText.text = "Fill all fields.";
            return;
        }

        if (newPass != confirmPass)
        {
            resetPasswordMessageText.text = "Passwords do not match.";
            return;
        }

        if (string.IsNullOrEmpty(verifiedEmail))
        {
            resetPasswordMessageText.text = "No verified email found. Please go back and enter your email.";
            return;
        }

        StartCoroutine(ResetPassword(verifiedEmail, newPass));
    }

    // ---------------------------
    // VALIDATION
    // ---------------------------

    private bool CheckUIAssignments(bool isRegister)
    {
        if (emailInput == null || passwordInput == null || messageText == null)
        {
            Debug.LogError("❌ UI references missing.");
            if (messageText != null)
                messageText.text = "UI references missing!";
            return false;
        }

        if (isRegister)
        {
            if (usernameInput == null || confirmPasswordInput == null)
            {
                Debug.LogError("❌ Register inputs missing.");
                messageText.text = "Fill all inputs.";
                return false;
            }
        }

        return true;
    }

    // ---------------------------
    // LOGIN
    // ---------------------------

    private IEnumerator Login()
    {
        yield return null; // Prevents TMP error when switching panels

        string email = emailInput.text.Trim();
        if (string.IsNullOrEmpty(email))
        {
            messageText.text = "Enter email.";
            yield break;
        }

        string pass = passwordInput.text;
        if (string.IsNullOrEmpty(pass))
        {
            messageText.text = "Enter password.";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                messageText.text = "Error: " + www.error;
            }
            else
            {
                string res = www.downloadHandler.text.Trim();
                messageText.text = res;

                if (res.ToLower().Contains("success"))
                {
                    PlayerPrefs.SetString("email", email);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
    }

    // ---------------------------
    // REGISTER
    // ---------------------------

    private IEnumerator Register()
    {
        yield return null;

        string user = usernameInput.text.Trim();
        string email = emailInput.text.Trim();
        string pass = passwordInput.text;
        string confirmPass = confirmPasswordInput.text;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirmPass))
        {
            messageText.text = "Fill all fields.";
            yield break;
        }

        if (pass != confirmPass)
        {
            messageText.text = "Passwords do not match.";
            yield break;
        }

        // Validate password length
        if (pass.Length < 6)
        {
            messageText.text = "Password must be at least 6 characters long.";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("email", email);
        form.AddField("password", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(registerURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                messageText.text = "Error: " + www.error;
            }
            else
            {
                string res = www.downloadHandler.text.Trim();
                messageText.text = res;

                // If the registration is successful, clear all fields
                if (res.ToLower().Contains("success"))
                {
                    // Clear the input fields after success
                    usernameInput.text = "";
                    emailInput.text = "";
                    passwordInput.text = "";
                    confirmPasswordInput.text = "";

                    // Optionally, you can set a success message or redirect the user
                    messageText.text = "Registration successful!";

                    // You can redirect to login or another scene, for example:
                    // SceneManager.LoadScene("LoginScene"); // Uncomment this line if you want to load the login scene
                }
            }
        }
    }

    // ---------------------------
    // FORGOT PASSWORD
    // ---------------------------

    private IEnumerator ForgotPassword(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        using (UnityWebRequest www = UnityWebRequest.Post(forgotPasswordURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                forgotPasswordMessageText.text = "Error: " + www.error;
            }
            else
            {
                string res = www.downloadHandler.text.Trim();
                forgotPasswordMessageText.text = res;

                if (res.ToLower().Contains("success"))
                {
                    verifiedEmail = email;

                    // Switch UI to reset password panel safely
                    UIManager.instance.ResetPasswordScreen();

                    yield return null; // wait 1 frame before TMP update
                    ForceSafeTMPUpdate();

                    // Clear reset password fields and message
                    resetPasswordInput.text = "";
                    resetConfirmPasswordInput.text = "";
                    resetPasswordMessageText.text = "";
                }
                else
                {
                    forgotPasswordMessageText.text = res;
                }
            }
        }
    }

    // ---------------------------
    // RESET PASSWORD
    // ---------------------------

    private IEnumerator ResetPassword(string email, string newPassword)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", newPassword);

        using (UnityWebRequest www = UnityWebRequest.Post(resetPasswordURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                resetPasswordMessageText.text = "Error: " + www.error;
            }
            else
            {
                string res = www.downloadHandler.text.Trim();
                resetPasswordMessageText.text = res;

                if (res.ToLower().Contains("success"))
                {
                    UIManager.instance.LoginScreen();
                    verifiedEmail = "";
                }
            }
        }
    }

    // ---------------------------
    // TOGGLE PASSWORD VISIBILITY
    // ---------------------------

    public void TogglePasswordVisibility()
    {
        // Toggle the password input field's content type between Password and Standard
        passwordInput.contentType = passwordInput.contentType == TMP_InputField.ContentType.Password ?
            TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;

        passwordInput.ForceLabelUpdate(); // Force label update to show changes

        // You can also change the button text or icon if needed
        // Example: Change button text to "Show" or "Hide"
        TextMeshProUGUI buttonText = passwordToggleButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = passwordInput.contentType == TMP_InputField.ContentType.Password ? "Show" : "Hide";
        }
    }

    public void ToggleConfirmPasswordVisibility()
    {
        // Toggle the confirm password input field's content type between Password and Standard
        confirmPasswordInput.contentType = confirmPasswordInput.contentType == TMP_InputField.ContentType.Password ?
            TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;

        confirmPasswordInput.ForceLabelUpdate(); // Force label update to show changes

        // Example: Change button text to "Show" or "Hide"
        TextMeshProUGUI buttonText = confirmPasswordToggleButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = confirmPasswordInput.contentType == TMP_InputField.ContentType.Password ? "Show" : "Hide";
        }
    }
}