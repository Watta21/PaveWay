using UnityEngine;
using TMPro;
using System.Collections;

public class CommentSender : MonoBehaviour
{
    public TextMeshProUGUI sentNotification; // Drag your "Sent" TMP text here
    public float displayDuration = 1.5f;     // How long "Sent" stays visible

    public void OnSendButtonClicked()
    {
        // Dito mo ilalagay comment sending logic mo
        // (Optional) Debug.Log("Comment sent!");

        // Show "Sent" notification
        StartCoroutine(ShowSentNotification());
    }

    private IEnumerator ShowSentNotification()
    {
        sentNotification.gameObject.SetActive(true);   // Show
        yield return new WaitForSeconds(displayDuration);
        sentNotification.gameObject.SetActive(false);  // Hide
    }
}
