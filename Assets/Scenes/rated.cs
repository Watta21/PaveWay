using UnityEngine;

public static class RatedStatusManager
{
    // Mark as rated for a specific user + image
    public static void MarkAsRated(string username, string imageName)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(imageName)) return;
        string key = GetKey(username, imageName);
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    // Check per-user if rated
    public static bool HasRated(string username, string imageName)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(imageName)) return false;
        string key = GetKey(username, imageName);
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    // Optional: remove rating mark (for testing/admin)
    public static void UnmarkRated(string username, string imageName)
    {
        string key = GetKey(username, imageName);
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    private static string GetKey(string username, string imageName)
    {
        return $"RATED_{username}_{imageName}";
    }
}
