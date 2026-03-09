using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject forgotPasswordUI;
    public GameObject resetPasswordUI;  // <-- new reset password screen

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    // Show Login screen
    public void LoginScreen()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        forgotPasswordUI.SetActive(false);
        resetPasswordUI.SetActive(false);
    }

    // Show Register screen
    public void RegisterScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
        forgotPasswordUI.SetActive(false);
        resetPasswordUI.SetActive(false);
    }

    // Show Forgot Password screen
    public void ForgotPasswordScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        forgotPasswordUI.SetActive(true);
        resetPasswordUI.SetActive(false);
    }

    // Show Reset Password screen
    public void ResetPasswordScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
        forgotPasswordUI.SetActive(false);
        resetPasswordUI.SetActive(false);
    }
}
