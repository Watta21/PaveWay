using UnityEngine;
using UnityEngine.UI;

public class ToggleOnClickOnly : MonoBehaviour
{
    [Header("References")]
    public Toggle toggle;       // child toggle
    public Button button;       // parent button
    public GameObject target;   // optional target to show/hide

    void Start()
    {
        if (toggle == null)
            toggle = GetComponentInChildren<Toggle>();

        if (button == null)
            button = GetComponent<Button>();

        if (toggle != null)
            toggle.isOn = false;

        if (target != null)
            target.SetActive(false);

        // ✅ When button clicked, sabay toggle + target activation
        if (button != null)
            button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (toggle != null)
            toggle.isOn = true;

        if (target != null)
            target.SetActive(true);

        Debug.Log($"[ToggleOnClickOnly] {name} → SABAY Toggle + Button Triggered");
    }
}
