
using System.Collections.Generic;
using UnityEngine;

namespace Immersal.Samples.Navigation
{
    public class IsNavigationTarget : MonoBehaviour
    {
        public NavigationTargets.NavigationCategory navigationCategory = NavigationTargets.NavigationCategory.Locations;
        public string targetName;
        public Sprite icon;
        public Vector3 position
        {
            get
            {
                return m_collider.bounds.center;
            }

            set
            {

            }
        }

        private Collider m_collider = null;

        private void Start()
        {
            NavigationGraphManager.Instance?.AddTarget(this);
        }

        private void OnDestroy()
        {
            NavigationGraphManager.Instance?.RemoveTarget(this);
        }

        private void OnEnable()
        {
            m_collider = GetComponent<Collider>();

            if (!NavigationTargets.NavigationTargetsDict.ContainsKey(navigationCategory))
                NavigationTargets.NavigationTargetsDict[navigationCategory] = new List<GameObject>();

            NavigationTargets.NavigationTargetsDict[navigationCategory].Add(gameObject);

            if (targetName.Equals(""))
            {
                targetName = gameObject.name;
            }
        }

        private void OnDisable()
        {
            if (NavigationTargets.NavigationTargetsDict.ContainsKey(navigationCategory))
                NavigationTargets.NavigationTargetsDict[navigationCategory].Remove(gameObject);
        }
    }
}