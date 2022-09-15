using UnityEngine;

namespace Patterns
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject g = new GameObject();
                        g.name = "Instance of: " + typeof(T);
                        instance = g.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        private static T instance;
    
    
    }
}