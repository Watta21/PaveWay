using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class PasswordStepController : MonoBehaviour
{
    public TMP_InputField passwordField;

    [Header("User & Image Settings")]
    public string username = "Guest";   // <-- drag or type username sa Inspector
    public string imageID = "Image1";   // <-- drag or type image ID sa Inspector

    [Header("SHOW / HIDE Buttons (1–5)")]
    public GameObject[] showButtons = new GameObject[5];
    public GameObject[] hideButtons = new GameObject[5];

    private int visibleCount = 0;

    public string loadURL = "http://localhost/unity/load_state.php";
    public string saveURL = "http://localhost/unity/save_state.php";

    void Start()
    {
        StartCoroutine(LoadState());
    }

    IEnumerator LoadState()
    {
        WWWForm f = new WWWForm();
        f.AddField("username", username);
        f.AddField("image_id", imageID);

        UnityWebRequest req = UnityWebRequest.Post(loadURL, f);
        yield return req.SendWebRequest();

        if (!req.isNetworkError && !req.isHttpError)
        {
            int.TryParse(req.downloadHandler.text, out visibleCount);
        }

        ApplyUI(visibleCount);
    }

    public void OnShow(int step)
    {
        if (step > visibleCount)
            UpdateState(step);
    }

    public void OnHide(int step)
    {
        UpdateState(step);
    }

    void UpdateState(int count)
    {
        visibleCount = Mathf.Clamp(count, 0, 5);
        ApplyUI(visibleCount);
        StartCoroutine(SaveState());
    }

    IEnumerator SaveState()
    {
        WWWForm f = new WWWForm();
        f.AddField("username", username);
        f.AddField("image_id", imageID);
        f.AddField("visible_step", visibleCount);

        UnityWebRequest req = UnityWebRequest.Post(saveURL, f);
        yield return req.SendWebRequest();
    }

    void ApplyUI(int count)
    {
        passwordField.contentType = count == 0
            ? TMP_InputField.ContentType.Password
            : TMP_InputField.ContentType.Standard;

        passwordField.ForceLabelUpdate();

        for (int i = 0; i < 5; i++)
        {
            bool active = i < count;
            showButtons[i].SetActive(!active);
            hideButtons[i].SetActive(active);
        }
    }
}
