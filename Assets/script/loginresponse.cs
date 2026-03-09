[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string status;   // This MUST exist
    public string username;
    public string email;
    public string message;  // optional error message
}
