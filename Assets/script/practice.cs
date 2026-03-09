using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DashboardUIBuilder : MonoBehaviour
{
    public Font font; // Optional: assign via Inspector, or it'll default to Arial

    void Start()
    {
        // Canvas
        GameObject canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);

        // Event System
        if (FindObjectOfType<EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        // Scroll View
        GameObject scrollViewGO = CreateScrollView(canvasGO.transform);

        // Content area
        Transform content = scrollViewGO.transform.Find("Viewport/Content");

        for (int i = 0; i < 5; i++)
        {
            CreateImagePanel(content, i);
        }
    }

    GameObject CreateScrollView(Transform parent)
    {
        GameObject scrollViewGO = new GameObject("ScrollView", typeof(RectTransform), typeof(ScrollRect), typeof(Image));
        scrollViewGO.transform.SetParent(parent, false);
        RectTransform rt = scrollViewGO.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        scrollViewGO.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f);

        GameObject viewport = new GameObject("Viewport", typeof(RectTransform), typeof(Mask), typeof(Image));
        viewport.transform.SetParent(scrollViewGO.transform, false);
        RectTransform vpRT = viewport.GetComponent<RectTransform>();
        vpRT.anchorMin = Vector2.zero;
        vpRT.anchorMax = Vector2.one;
        vpRT.offsetMin = Vector2.zero;
        vpRT.offsetMax = Vector2.zero;
        viewport.GetComponent<Image>().color = Color.white;
        viewport.GetComponent<Mask>().showMaskGraphic = false;

        GameObject content = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        content.transform.SetParent(viewport.transform, false);
        RectTransform contentRT = content.GetComponent<RectTransform>();
        contentRT.anchorMin = new Vector2(0, 1);
        contentRT.anchorMax = new Vector2(1, 1);
        contentRT.pivot = new Vector2(0.5f, 1);
        contentRT.offsetMin = Vector2.zero;
        contentRT.offsetMax = Vector2.zero;

        VerticalLayoutGroup layout = content.GetComponent<VerticalLayoutGroup>();
        layout.spacing = 20;
        layout.childControlHeight = true;
        layout.childForceExpandHeight = false;
        layout.childControlWidth = true;

        content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        ScrollRect sr = scrollViewGO.GetComponent<ScrollRect>();
        sr.viewport = vpRT;
        sr.content = contentRT;

        return scrollViewGO;
    }

    void CreateImagePanel(Transform parent, int index)
    {
        GameObject panel = new GameObject($"ImagePanel_{index}", typeof(RectTransform), typeof(Image), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        panel.transform.SetParent(parent, false);
        RectTransform panelRT = panel.GetComponent<RectTransform>();
        panelRT.sizeDelta = new Vector2(0, 0);

        panel.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);

        VerticalLayoutGroup layout = panel.GetComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(20, 20, 20, 20);
        layout.spacing = 10;
        layout.childControlHeight = true;
        layout.childForceExpandHeight = false;

        panel.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Image
        GameObject img = new GameObject("Image", typeof(RectTransform), typeof(Image));
        img.transform.SetParent(panel.transform, false);
        img.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300);
        img.GetComponent<Image>().color = Random.ColorHSV();

        // Comment Section (hidden by default)
        GameObject commentSection = new GameObject("CommentSection", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        commentSection.transform.SetParent(panel.transform, false);
        VerticalLayoutGroup secLayout = commentSection.GetComponent<VerticalLayoutGroup>();
        secLayout.spacing = 10;
        secLayout.childControlHeight = true;
        secLayout.childForceExpandHeight = false;
        commentSection.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        commentSection.SetActive(false); // hidden initially

        // InputField
        GameObject inputGO = CreateInputField(commentSection.transform);

        // Send Button
        GameObject sendBtn = CreateButton("Send", commentSection.transform, () =>
        {
            string commentText = inputGO.GetComponent<InputField>().text;
            if (!string.IsNullOrWhiteSpace(commentText))
            {
                GameObject txtGO = CreateText(commentText, commentSection.transform);
                inputGO.GetComponent<InputField>().text = "";
            }
        });

        // Comment Button (toggle)
        GameObject commentBtn = CreateButton("Comment", panel.transform, () =>
        {
            commentSection.SetActive(!commentSection.activeSelf);
        });
    }

    GameObject CreateButton(string label, Transform parent, UnityEngine.Events.UnityAction onClick)
    {
        GameObject btnGO = new GameObject(label + "Button", typeof(RectTransform), typeof(Button), typeof(Image));
        btnGO.transform.SetParent(parent, false);
        btnGO.GetComponent<Image>().color = new Color(0.2f, 0.5f, 0.9f);

        // Text
        GameObject txtGO = CreateText(label, btnGO.transform);
        txtGO.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        RectTransform btnRT = btnGO.GetComponent<RectTransform>();
        btnRT.sizeDelta = new Vector2(0, 60);

        Button btn = btnGO.GetComponent<Button>();
        btn.onClick.AddListener(onClick);

        return btnGO;
    }

    GameObject CreateText(string content, Transform parent)
    {
        GameObject txtGO = new GameObject("Text", typeof(RectTransform), typeof(Text));
        txtGO.transform.SetParent(parent, false);

        Text txt = txtGO.GetComponent<Text>();
        txt.text = content;
        txt.fontSize = 24;
        txt.color = Color.black;
        txt.font = font != null ? font : Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.alignment = TextAnchor.UpperLeft;
        txt.horizontalOverflow = HorizontalWrapMode.Wrap;
        txt.verticalOverflow = VerticalWrapMode.Overflow;

        RectTransform rt = txtGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 0);

        return txtGO;
    }

    GameObject CreateInputField(Transform parent)
    {
        GameObject inputGO = new GameObject("InputField", typeof(RectTransform), typeof(Image), typeof(InputField));
        inputGO.transform.SetParent(parent, false);
        inputGO.GetComponent<Image>().color = Color.white;

        // Text Component
        GameObject textGO = new GameObject("Text", typeof(RectTransform), typeof(Text));
        textGO.transform.SetParent(inputGO.transform, false);
        RectTransform textRT = textGO.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = new Vector2(10, 10);
        textRT.offsetMax = new Vector2(-10, -10);

        Text text = textGO.GetComponent<Text>();
        text.font = font != null ? font : Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 24;
        text.color = Color.black;
        text.alignment = TextAnchor.UpperLeft;
        text.supportRichText = true;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;

        ContentSizeFitter fitter = textGO.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        InputField input = inputGO.GetComponent<InputField>();
        input.textComponent = text;
        input.lineType = InputField.LineType.MultiLineNewline;

        LayoutElement layout = inputGO.AddComponent<LayoutElement>();
        layout.minHeight = 60;

        return inputGO;
    }
}
