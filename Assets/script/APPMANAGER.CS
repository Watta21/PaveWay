using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AppManager : MonoBehaviour
{
    public Canvas canvas;
    public TMP_InputField searchInput;
    public GameObject contentParent;

    void Start()
    {
        // Setup Canvas
        GameObject canvasGO = new GameObject("Canvas");
        canvasGO.layer = LayerMask.NameToLayer("UI");
        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        // Background image
        GameObject bgGO = new GameObject("BackgroundImage");
        bgGO.transform.SetParent(canvasGO.transform, false);
        RectTransform bgRect = bgGO.AddComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;
        Image bgImage = bgGO.AddComponent<Image>();
        Sprite bgSprite = Resources.Load<Sprite>("img1");
        if (bgSprite != null)
        {
            bgImage.sprite = bgSprite;
            bgImage.type = Image.Type.Sliced;
            bgImage.color = new Color(1f, 1f, 1f, 0.55294f);
        }
        bgGO.transform.SetAsFirstSibling();

        // Search bar
        GameObject inputGO = new GameObject("SearchInput");
        inputGO.transform.SetParent(canvasGO.transform, false);
        RectTransform inputRect = inputGO.AddComponent<RectTransform>();
        inputRect.anchorMin = new Vector2(0.5f, 1);
        inputRect.anchorMax = new Vector2(0.5f, 1);
        inputRect.pivot = new Vector2(0.5f, 1);
        inputRect.sizeDelta = new Vector2(400, 40);
        inputRect.anchoredPosition = new Vector2(0, -60);
        inputGO.AddComponent<Image>().color = Color.white;
        searchInput = inputGO.AddComponent<TMP_InputField>();

        GameObject placeholderGO = new GameObject("Placeholder");
        placeholderGO.transform.SetParent(inputGO.transform, false);
        TMP_Text placeholderText = placeholderGO.AddComponent<TextMeshProUGUI>();
        placeholderText.text = "Search...";
        placeholderText.color = Color.gray;
        placeholderText.alignment = TextAlignmentOptions.MidlineLeft;
        RectTransform placeholderRect = placeholderGO.GetComponent<RectTransform>();
        placeholderRect.anchorMin = Vector2.zero;
        placeholderRect.anchorMax = Vector2.one;
        placeholderRect.offsetMin = Vector2.zero;
        placeholderRect.offsetMax = Vector2.zero;

        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(inputGO.transform, false);
        TMP_Text text = textGO.AddComponent<TextMeshProUGUI>();
        text.text = "";
        text.color = Color.black;
        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        searchInput.textComponent = text;
        searchInput.placeholder = placeholderText;

        // ScrollView
        GameObject scrollViewGO = new GameObject("ScrollView");
        scrollViewGO.transform.SetParent(canvasGO.transform, false);
        RectTransform scrollRect = scrollViewGO.AddComponent<RectTransform>();
        scrollRect.anchorMin = new Vector2(0, 0);
        scrollRect.anchorMax = new Vector2(1, 1);
        scrollRect.pivot = new Vector2(0.5f, 1);
        scrollRect.offsetMin = new Vector2(70, 0);
        scrollRect.offsetMax = new Vector2(-75, -80);
        ScrollRect scroll = scrollViewGO.AddComponent<ScrollRect>();
        scroll.horizontal = false;
        scroll.vertical = true;

        GameObject viewportGO = new GameObject("Viewport");
        viewportGO.transform.SetParent(scrollViewGO.transform, false);
        RectTransform viewportRect = viewportGO.AddComponent<RectTransform>();
        viewportRect.anchorMin = new Vector2(0, 0);
        viewportRect.anchorMax = new Vector2(1, 1);
        viewportRect.offsetMin = Vector2.zero;
        viewportRect.offsetMax = Vector2.zero;
        Mask mask = viewportGO.AddComponent<Mask>();
        mask.showMaskGraphic = false;
        viewportGO.AddComponent<Image>().color = Color.white;

        contentParent = new GameObject("Content");
        contentParent.transform.SetParent(viewportGO.transform, false);
        RectTransform contentRect = contentParent.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.pivot = new Vector2(0.5f, 1);
        contentRect.anchoredPosition = Vector2.zero;
        contentRect.sizeDelta = new Vector2(0, 500);
        VerticalLayoutGroup layout = contentParent.AddComponent<VerticalLayoutGroup>();
        layout.childControlHeight = true;
        layout.childForceExpandHeight = false;
        layout.spacing = 20;
        contentParent.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scroll.viewport = viewportRect;
        scroll.content = contentRect;

        // Create gallery items
        CreateGalleryItem("New Government Center (NGC)", "img1", "This is a description of the New Government Center.");
        CreateGalleryItem("Gallery G", "img2", "A description for G.");
        CreateGalleryItem("Gallery 3", "img3", "This is the description for Gallery 3.");
        CreateGalleryItem("Gallery 4", "img4", "Detailed description of Gallery 4.");
        CreateGalleryItem("Gallery 5", "img5", "Gallery 5's description goes here.");

        searchInput.onValueChanged.AddListener(FilterGallery);
    }

    void CreateGalleryItem(string title, string imageName, string description)
    {
        GameObject itemGO = new GameObject(title);
        itemGO.transform.SetParent(contentParent.transform, false);
        VerticalLayoutGroup vLayout = itemGO.AddComponent<VerticalLayoutGroup>();
        vLayout.childAlignment = TextAnchor.UpperCenter;
        vLayout.spacing = 20;
        vLayout.padding = new RectOffset(20, 20, 20, 20);
        itemGO.AddComponent<LayoutElement>().preferredHeight = 1000;
        itemGO.AddComponent<Image>().color = new Color(1f, 1f, 1f, 0.9f);

        // Image with border
        GameObject borderGO = new GameObject("ImageBorder");
        borderGO.transform.SetParent(itemGO.transform, false);
        RectTransform borderRect = borderGO.AddComponent<RectTransform>();
        borderRect.sizeDelta = new Vector2(600, 500);
        borderRect.anchoredPosition = new Vector2(0, 0);
        Image borderImage = borderGO.AddComponent<Image>();
        borderImage.sprite = Resources.Load<Sprite>("rounded_border");
        borderImage.type = Image.Type.Sliced;
        borderImage.color = Color.white;
        borderGO.AddComponent<Mask>().showMaskGraphic = true;

        GameObject imgGO = new GameObject("ItemImage");
        imgGO.transform.SetParent(borderGO.transform, false);
        RectTransform imgRect = imgGO.AddComponent<RectTransform>();
        imgRect.anchorMin = new Vector2(0.5f, 0.5f);
        imgRect.anchorMax = new Vector2(0.5f, 0.5f);
        imgRect.pivot = new Vector2(0.5f, 0.5f);
        imgRect.sizeDelta = new Vector2(580, 480);
        Image image = imgGO.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>(imageName);
        image.preserveAspect = true;
        image.type = Image.Type.Simple;

        // Title
        GameObject titleGO = new GameObject("ItemText");
        titleGO.transform.SetParent(itemGO.transform, false);
        RectTransform titleRect = titleGO.AddComponent<RectTransform>();
        titleRect.sizeDelta = new Vector2(580, 60);
        TMP_Text titleText = titleGO.AddComponent<TextMeshProUGUI>();
        titleText.text = title;
        titleText.fontSize = 28;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = Color.black;
        titleText.fontStyle = FontStyles.Bold;
        titleText.enableWordWrapping = true;

        // Description
        GameObject descGO = new GameObject("DescriptionText");
        descGO.transform.SetParent(itemGO.transform, false);
        RectTransform descRect = descGO.AddComponent<RectTransform>();
        descRect.sizeDelta = new Vector2(580, 80);
        TMP_Text descText = descGO.AddComponent<TextMeshProUGUI>();
        descText.text = description;
        descText.fontSize = 20;
        descText.alignment = TextAlignmentOptions.Center;
        descText.color = new Color(0.2f, 0.2f, 0.2f);
        descText.enableWordWrapping = true;

        // Comment Toggle Button
        GameObject commentToggleGO = new GameObject("CommentToggle");
        commentToggleGO.transform.SetParent(itemGO.transform, false);
        RectTransform toggleRect = commentToggleGO.AddComponent<RectTransform>();
        toggleRect.sizeDelta = new Vector2(10, 0);
        Button commentButton = commentToggleGO.AddComponent<Button>();
        commentButton.colors = CreateButtonColors();

        Image commentIcon = commentToggleGO.AddComponent<Image>();
        Sprite iconSprite = Resources.Load<Sprite>("comment_icon");
        if (iconSprite != null)
        {
            commentIcon.sprite = iconSprite;
            commentIcon.preserveAspect = true;
        }

        // Comment Section Container
        GameObject commentSection = new GameObject("CommentSection");
        commentSection.transform.SetParent(itemGO.transform, false);
        VerticalLayoutGroup commentLayout = commentSection.AddComponent<VerticalLayoutGroup>();
        commentLayout.spacing = 10;
        commentLayout.childForceExpandWidth = true;
        commentSection.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        commentSection.SetActive(false);

        // Comment Input Section
        GameObject inputFieldGO = new GameObject("CommentInput");
        inputFieldGO.transform.SetParent(commentSection.transform, false);
        HorizontalLayoutGroup hLayout = inputFieldGO.AddComponent<HorizontalLayoutGroup>();
        hLayout.spacing = 10;

        GameObject inputGO = new GameObject("InputField");
        inputGO.transform.SetParent(inputFieldGO.transform, false);
        RectTransform inputRect = inputGO.AddComponent<RectTransform>();
        inputRect.sizeDelta = new Vector2(420, 30);
        TMP_InputField commentInput = inputGO.AddComponent<TMP_InputField>();
        TextMeshProUGUI inputText = inputGO.AddComponent<TextMeshProUGUI>();
        inputText.color = Color.black;
        commentInput.textComponent = inputText;
        GameObject placeholderGO = new GameObject("Placeholder");
        placeholderGO.transform.SetParent(inputGO.transform, false);
        TextMeshProUGUI placeholderText = placeholderGO.AddComponent<TextMeshProUGUI>();
        placeholderText.text = "Enter your comment...";
        placeholderText.color = Color.gray;
        commentInput.placeholder = placeholderText;

        GameObject buttonGO = new GameObject("SubmitButton");
        buttonGO.transform.SetParent(inputFieldGO.transform, false);
        Button submitButton = buttonGO.AddComponent<Button>();
        Image buttonImage = buttonGO.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.6f, 1f);
        buttonGO.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 30);

        GameObject btnTextGO = new GameObject("Text");
        btnTextGO.transform.SetParent(buttonGO.transform, false);
        TextMeshProUGUI btnText = btnTextGO.AddComponent<TextMeshProUGUI>();
        btnText.text = "Submit";
        btnText.color = Color.white;
        btnText.alignment = TextAlignmentOptions.Center;
        RectTransform btnTextRect = btnTextGO.GetComponent<RectTransform>();
        btnTextRect.anchorMin = Vector2.zero;
        btnTextRect.anchorMax = Vector2.one;
        btnTextRect.offsetMin = Vector2.zero;
        btnTextRect.offsetMax = Vector2.zero;

        // Scrollable comment container
        GameObject scrollGO = new GameObject("CommentScrollView");
        scrollGO.transform.SetParent(commentSection.transform, false);
        RectTransform scrollRect = scrollGO.AddComponent<RectTransform>();
        scrollRect.sizeDelta = new Vector2(500, 200);
        ScrollRect scroll = scrollGO.AddComponent<ScrollRect>();
        scroll.horizontal = false;

        GameObject viewportGO = new GameObject("Viewport");
        viewportGO.transform.SetParent(scrollGO.transform, false);
        RectTransform viewportRect = viewportGO.AddComponent<RectTransform>();
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.offsetMin = Vector2.zero;
        viewportRect.offsetMax = Vector2.zero;
        Image viewportImage = viewportGO.AddComponent<Image>();
        viewportImage.color = new Color(1f, 1f, 1f, 0.1f);
        Mask viewportMask = viewportGO.AddComponent<Mask>();
        viewportMask.showMaskGraphic = false;

        GameObject commentContent = new GameObject("CommentContent");
        commentContent.transform.SetParent(viewportGO.transform, false);
        RectTransform contentRect = commentContent.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.pivot = new Vector2(0.5f, 1);
        contentRect.anchoredPosition = Vector2.zero;
        commentContent.AddComponent<VerticalLayoutGroup>().spacing = 10;
        commentContent.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scroll.viewport = viewportRect;
        scroll.content = contentRect.transform as RectTransform;

        // Hook up submit
        submitButton.onClick.AddListener(() =>
        {
            string comment = commentInput.text;
            if (!string.IsNullOrWhiteSpace(comment))
            {
                commentInput.text = "";
                ShowSubmittedComment(commentContent.transform, comment);
            }
        });

        // Toggle comment section
        commentButton.onClick.AddListener(() =>
        {
            commentSection.SetActive(!commentSection.activeSelf);
        });
    }

    ColorBlock CreateButtonColors()
    {
        return new ColorBlock()
        {
            normalColor = new Color(0.8f, 0.8f, 0.8f),
            highlightedColor = new Color(0.7f, 0.7f, 0.7f),
            pressedColor = new Color(0.6f, 0.6f, 0.6f),
            selectedColor = new Color(0.7f, 0.7f, 0.7f),
            disabledColor = new Color(0.5f, 0.5f, 0.5f),
            colorMultiplier = 1,
            fadeDuration = 0.1f
        };
    }

    void ShowSubmittedComment(Transform parent, string comment)
    {
        GameObject commentBox = new GameObject("UserComment");
        commentBox.transform.SetParent(parent, false);
        commentBox.AddComponent<LayoutElement>().preferredHeight = 50;
        Image bg = commentBox.AddComponent<Image>();
        bg.color = new Color(0.95f, 0.95f, 0.95f);
        TMP_Text commentText = commentBox.AddComponent<TextMeshProUGUI>();
        commentText.text = comment;
        commentText.fontSize = 16;
        commentText.color = Color.black;
        commentText.alignment = TextAlignmentOptions.Left;
        RectTransform rect = commentText.rectTransform;
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.offsetMin = new Vector2(10, 5);
        rect.offsetMax = new Vector2(-10, -5);
    }

    void FilterGallery(string query)
    {
        string lowerQuery = query.ToLower();
        foreach (Transform child in contentParent.transform)
        {
            child.gameObject.SetActive(child.name.ToLower().Contains(lowerQuery));
        }
    }
}