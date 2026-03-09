using UnityEngine;
using UnityEngine.UI;

public class ToggleOnClickOnly_NineOFF : MonoBehaviour
{
    [Header("References")]
    public Toggle toggle1;       // first child toggle
    public Toggle toggle2;       // second child toggle
    public Toggle toggle3;       // third child toggle
    public Toggle toggle4;       // fourth child toggle
    public Toggle toggle5;       // fifth child toggle
    public Toggle toggle6;       // sixth child toggle
    public Toggle toggle7;       // seventh child toggle
    public Toggle toggle8;       // eighth child toggle
    public Toggle toggle9;       // ninth child toggle
    public Button button;        // parent button
    public GameObject target;    // optional target to show/hide

    // Array to store all toggles for easier management
    private Toggle[] allToggles;

    void Start()
    {
        // Auto-assign button if not set
        if (button == null)
            button = GetComponent<Button>();

        // Initialize the toggle array
        allToggles = new Toggle[] { toggle1, toggle2, toggle3, toggle4, toggle5, toggle6, toggle7, toggle8, toggle9 };

        // Auto-assign toggles if not set
        bool needAutoAssign = false;
        foreach (Toggle toggle in allToggles)
        {
            if (toggle == null)
            {
                needAutoAssign = true;
                break;
            }
        }

        if (needAutoAssign)
        {
            Toggle[] childToggles = GetComponentsInChildren<Toggle>(true);
            for (int i = 0; i < Mathf.Min(allToggles.Length, childToggles.Length); i++)
            {
                allToggles[i] = childToggles[i];
            }
        }

        // ✅ Do NOT enable toggles or target at start
        // This prevents them from appearing immediately
        foreach (Toggle toggle in allToggles)
        {
            if (toggle != null)
                toggle.isOn = true;
        }

        if (target != null)
            target.SetActive(true);

        // Listen to button click
        if (button != null)
            button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        // ✅ Turn OFF all toggles
        foreach (Toggle toggle in allToggles)
        {
            if (toggle != null)
                toggle.isOn = false;
        }

        // ✅ Turn OFF target
        if (target != null)
            target.SetActive(false);

        Debug.Log($"[ToggleOnClickOnly_NineOFF] Button clicked → all 9 toggles OFF & target deactivated");
    }

    // Helper method to get a specific toggle by index (1-9)
    public Toggle GetToggle(int index)
    {
        if (index >= 1 && index <= 9)
            return allToggles[index - 1];
        return null;
    }

    // Helper method to set a specific toggle
    public void SetToggle(int index, Toggle toggle)
    {
        if (index >= 1 && index <= 9)
            allToggles[index - 1] = toggle;
    }
}