using UnityEngine;

namespace Plugins.Tools
{
    /// <summary>
    /// A class with useful methods for every type of special case
    /// </summary>
    public static class UtilityMethods
    {
        /// <summary>
        /// Activates or deactivates the children the gameObject
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="active">Whether to activate or deactivate its children</param>
        public static void SetActiveChildren(this GameObject parent, bool active = true)
        {
            foreach (Transform child in parent.GetComponentsInChildren<Transform>(active)) child.gameObject.SetActive(active);
        }
    }
}
