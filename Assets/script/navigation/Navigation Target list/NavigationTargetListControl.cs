using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Immersal.Samples.Navigation
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(ScrollRect))]
    public class NavigationTargetListControl : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_ButtonTemplate = null;

        [SerializeField]
        private RectTransform m_ContentParent = null;

        [Header("Navigation Icon")]
        [SerializeField]
        private Sprite m_NavigationIcon = null; // ✅ icon (logo) shown beside name

        private List<IsNavigationTarget> targets = new List<IsNavigationTarget>();

        public void GenerateButtons()
        {
            // 🧹 Clear old buttons
            foreach (Transform child in m_ContentParent)
            {
                Destroy(child.gameObject);
            }

            // ✅ Include even inactive targets (so they still appear in list)
            targets = new List<IsNavigationTarget>(FindObjectsOfType<IsNavigationTarget>(includeInactive: true));

            // 🧩 Create button for each target
            foreach (IsNavigationTarget t in targets)
            {
                GameObject btnObj = Instantiate(m_ButtonTemplate, m_ContentParent);
                btnObj.SetActive(true);

                NavigationTargetListButton btn = btnObj.GetComponent<NavigationTargetListButton>();
                if (btn != null)
                {
                    btn.SetText(t.name);
                    btn.SetTarget(t.gameObject);

                    // ✅ Apply navigation logo if assigned
                    if (m_NavigationIcon != null)
                        btn.SetIcon(m_NavigationIcon);
                }
            }
        }
    }
}
