using UnityEngine;

namespace Plugins.Tools
{
    /// <summary>
    /// Singleton pattern.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T pr_instance;

        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The Instance.</value>
        public static T Instance
        {
            get
            {
                if (pr_instance == null) pr_instance = FindObjectOfType<T>();
                
                return pr_instance;
            }
        }

        /// <summary>
        /// On awake, we initialize our Instance. Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;

            pr_instance = this as T;
        }
    }

    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T pr_instance;

        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The Instance.</value>
        public static T Instance
        {
            get
            {
                if (pr_instance != null) return pr_instance;
                pr_instance = FindObjectOfType<T>();
                if (pr_instance != null) return pr_instance;
                var obj = new GameObject();
                pr_instance = obj.AddComponent<T>();

                return pr_instance;
            }
        }

        /// <summary>
        /// On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
        /// </summary>
        protected virtual void Awake()
        {
            if (pr_instance == null)
            {
                //If I am the first Instance, make me the Singleton
                pr_instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                //If a Singleton already exists and you find
                //another reference in scene, destroy it!
                if (this != pr_instance) Destroy(gameObject);
            }
        }
    }
}
