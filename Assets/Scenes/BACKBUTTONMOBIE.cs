using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public string defaultScene = "MainMenu";

    void Update()
    {
        // Android back button o Escape key sa Editor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    public void GoBack()
    {
        if (SceneManager.GetActiveScene().name != defaultScene)
        {
            SceneManager.LoadScene(defaultScene);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop Play Mode sa Editor
#else
            Application.Quit(); // Quit sa build
#endif
        }
    }
} // <--- siguraduhing ito ang closing brace ng class
