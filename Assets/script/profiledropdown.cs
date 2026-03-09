using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UserProfileController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI profileNameText;           // Displays the full username
    public TextMeshProUGUI profileLetterTopBarText;   // Displays first letter (top bar)
    public TextMeshProUGUI profileLetterDropdownText; // Displays first letter (dropdown)
    public Button logoutButton;                       // Logout button
    public Button profileButton;                      // Profile button to toggle dropdown
    public GameObject profileDropdownPanel;           // Dropdown panel (hidden by default)

    private bool isDropdownVisible = false;

    void Start()
    {
        // Add listeners
        if (logoutButton != null)
            logoutButton.onClick.AddListener(Logout);

        if (profileButton != null)
            profileButton.onClick.AddListener(ToggleDropdown);

        // Hide dropdown initially
        if (profileDropdownPanel != null)
            profileDropdownPanel.SetActive(false);

        // Load saved username
        string username = PlayerPrefs.GetString("Username", "");

        if (!string.IsNullOrEmpty(username))
        {
            // Display username and initials
            profileNameText.text = username;
            string firstLetter = username.Substring(0, 1).ToUpperInvariant();
            profileLetterTopBarText.text = firstLetter;
            profileLetterDropdownText.text = firstLetter;
        }
        else
        {
            // No username found → redirect to login
            Debug.LogWarning("No username found. Redirecting to login page...");
            SceneManager.LoadScene("SampleScene 1"); // ✅ must match the scene used in your LoginController
        }
    }

    void Update()
    {
        // Detect click outside of dropdown to hide it
        if (isDropdownVisible && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement(profileDropdownPanel))
            {
                HideDropdown();
            }
        }
    }

    // Toggles the dropdown visibility
    void ToggleDropdown()
    {
        isDropdownVisible = !isDropdownVisible;
        profileDropdownPanel.SetActive(isDropdownVisible);
    }

    // Hides the dropdown panel
    void HideDropdown()
    {
        isDropdownVisible = false;
        profileDropdownPanel.SetActive(false);
    }

    // Checks if the pointer is currently over the dropdown UI
    bool IsPointerOverUIElement(GameObject target)
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == target || result.gameObject.transform.IsChildOf(target.transform))
                return true;
        }

        return false;
    }

    // Logout: clears PlayerPrefs and redirects to login
    public void Logout()
    {
        PlayerPrefs.DeleteKey("Username");
        PlayerPrefs.DeleteKey("UserEmail");
        PlayerPrefs.Save();

        Debug.Log("User logged out. Redirecting to login...");
        SceneManager.LoadScene("SampleScene"); // ✅ match your login scene name
    }
}
