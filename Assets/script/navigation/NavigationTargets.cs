using System.Collections.Generic;
using UnityEngine;

namespace Immersal.Samples.Navigation
{
    public class NavigationTargets
    {
        public enum NavigationCategory { People, Locations };
        public NavigationCategory navigationCategories = NavigationCategory.Locations;
        public static Dictionary<NavigationCategory, List<GameObject>> NavigationTargetsDict = new Dictionary<NavigationCategory, List<GameObject>>();
    }
}
