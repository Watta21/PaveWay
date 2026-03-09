
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Immersal.Samples.Navigation
{
    public class NavigationTargetListButton : Button, IPointerClickHandler
    {
        [HideInInspector]
        public GameObject targetObject = null;

        [SerializeField]
        private TextMeshProUGUI m_TextMeshProUGUI = null;
        [SerializeField]
        private Image m_Image = null;

        private string targetName = null;

        public void SetText(string text)
        {
            targetName = text;
            if (m_TextMeshProUGUI != null)
            {
                m_TextMeshProUGUI.text = targetName;
            }
        }

        public void SetIcon(Sprite icon)
        {
            if (m_Image != null)
            {
                m_Image.sprite = icon;
            }
        }

        public void SetTarget(GameObject go)
        {
            targetObject = go;
        }

        override public void OnPointerClick(PointerEventData pointerEventData)
        {
            NavigationManager.Instance.InitializeNavigation(this);
            NavigationManager.Instance.ToggleTargetsList();
            base.OnPointerClick(pointerEventData);
        }
    }
}