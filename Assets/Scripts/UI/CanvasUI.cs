using UnityEngine;

namespace UI
{
    public class CanvasUI : MonoBehaviour
    {
        public static CanvasUI Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}