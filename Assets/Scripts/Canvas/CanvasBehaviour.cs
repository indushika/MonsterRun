using DefaultNamespace;
using UnityEngine;

namespace MonsterRun.Canvas
{
    public abstract class CanvasBehaviour : MonoBehaviour
    {
        private CanvasConfigBase config;
        public CanvasConfigBase Config => config;
        public abstract void OnInitialized(); 
        
        public void Initialize(CanvasConfigBase config)
        {
            this.config = config;
            OnInitialized();
        } 
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

      
    }
}