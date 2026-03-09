using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeedUIController : MonoBehaviour
{
    [System.Serializable]
    public class PostPanel
    {
        public GameObject panelRoot;
        public TextMeshProUGUI nameText;
        public GameObject commentSection;
        public TMP_InputField commentInput;
        public Button commentButton;
        public Button sendButton;
        public RectTransform commentInputRect;
        public RectTransform sendButtonRect;
    }

    [Header("Search")]
    public TMP_InputField searchField;

    [Header("Posts")]
    public PostPanel[] posts;

    void Start()
    {
        if (searchField != null)
            searchField.onValueChanged.AddListener(FilterPosts);

        foreach (var post in posts)
        {
            var capturedPost = post; // Fix closure issue

            if (capturedPost.commentSection != null)
                capturedPost.commentSection.SetActive(false);

            if (capturedPost.commentButton != null && capturedPost.commentSection != null)
            {
                capturedPost.commentButton.onClick.AddListener(() =>
                {
                    bool isActive = capturedPost.commentSection.activeSelf;
                    capturedPost.commentSection.SetActive(!isActive);

                    if (!isActive && capturedPost.commentSection.TryGetComponent<RectTransform>(out var rect))
                        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
                });
            }

            if (capturedPost.sendButton != null && capturedPost.commentInput != null)
            {
                capturedPost.sendButton.onClick.AddListener(() =>
                {
                    string text = capturedPost.commentInput.text.Trim();
                    if (!string.IsNullOrEmpty(text))
                    {
                        Debug.Log($"Comment on {capturedPost.nameText.text}: {text}");
                        capturedPost.commentInput.text = "";

                        if (capturedPost.commentSection.TryGetComponent<RectTransform>(out var rect))
                            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
                    }
                });
            }

            if (capturedPost.commentInput != null)
            {
                capturedPost.commentInput.onValueChanged.AddListener((_) =>
                {
                    AdjustCommentLayout(capturedPost);
                });
            }
        }

        FilterPosts(searchField != null ? searchField.text : "");
    }

    void AdjustCommentLayout(PostPanel post)
    {
        if (post.commentInput == null || post.commentInputRect == null) return;

        float preferredHeight = post.commentInput.textComponent.preferredHeight + 20f;
        float newHeight = Mathf.Clamp(preferredHeight, 40f, 200f);

        post.commentInputRect.sizeDelta = new Vector2(post.commentInputRect.sizeDelta.x, newHeight);

        if (post.sendButtonRect != null)
        {
            post.sendButtonRect.sizeDelta = new Vector2(post.sendButtonRect.sizeDelta.x, newHeight);
            post.sendButtonRect.anchoredPosition = new Vector2(post.sendButtonRect.anchoredPosition.x, 0);
        }

        if (post.commentSection != null && post.commentSection.TryGetComponent<RectTransform>(out var rect))
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    }

    void FilterPosts(string query)
    {
        string lowerQuery = query.Trim().ToLowerInvariant();

        foreach (var post in posts)
        {
            if (post.panelRoot != null)
            {
                if (string.IsNullOrEmpty(lowerQuery))
                {
                    post.panelRoot.SetActive(true);
                }
                else
                {
                    post.panelRoot.SetActive(
                        post.nameText != null &&
                        post.nameText.text.ToLowerInvariant().Contains(lowerQuery)
                    );
                }
            }
        }
    }
}