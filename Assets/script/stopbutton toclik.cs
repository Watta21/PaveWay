using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisableAllButtons : MonoBehaviour
{
    [Header("Buttons to Disable")]
    public List<Button> buttonsToDisable;

    void Start()
    {
        DisableButtons();
    }

    // DISABLE ALL BUTTONS
    public void DisableButtons()
    {
        foreach (Button btn in buttonsToDisable)
        {
            btn.interactable = false;
        }
    }

    // ENABLE ALL BUTTONS
    public void EnableButtons()
    {
        foreach (Button btn in buttonsToDisable)
        {
            btn.interactable = true;
        }
    }
}
