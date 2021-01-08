using UnityEngine;

namespace Plugins.Tools
{
    public static class UtilityMethods
    {
        public static void SetActiveChildren(this GameObject parent, bool active = true)
        {
            foreach (Transform child in parent.GetComponentsInChildren<Transform>(active)) child.gameObject.SetActive(active);
        }
    }
}
