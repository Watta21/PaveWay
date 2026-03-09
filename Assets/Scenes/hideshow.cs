using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour
{
    [Header("Password Field")]
    public TMP_InputField passwordField;

    [Header("Buttons")]
    public GameObject buttonShow; // 👁️ (Show password)
    public GameObject buttonHide; // 🙈 (Hide password)

    private bool isHidden = true;

    private void Start()
    {
        // Start with password hidden
        SetPasswordHidden(true);
    }

    // 👁️ Button pressed
    public void OnShowClicked()
    {
        SetPasswordHidden(false);
    }

    // 🙈 Button pressed
    public void OnHideClicked()
    {
        SetPasswordHidden(true);
    }

    private void SetPasswordHidden(bool hide)
    {
        isHidden = hide;

        if (hide)
        {
            // Hide password
            passwordField.contentType = TMP_InputField.ContentType.Password;
            buttonShow.SetActive(true);   // show 👁️ button
            buttonHide.SetActive(false);  // hide 🙈 button
        }
        else
        {
            // Show password
            passwordField.contentType = TMP_InputField.ContentType.Standard;
            buttonShow.SetActive(false);  // hide 👁️ button
            buttonHide.SetActive(true);   // show 🙈 button
        }

        passwordField.ForceLabelUpdate();
    }
}