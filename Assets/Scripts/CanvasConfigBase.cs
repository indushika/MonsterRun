using UnityEngine;

namespace MonsterRun.UI
{
    [CreateAssetMenu(fileName = "CanvasConfig", menuName = "Scriptable Objects/Canvas Config")]
    public class CanvasConfigBase : ScriptableObject
    {
        [SerializeField] private CanvasBehaviour canvas;
        [SerializeField] private UILayer uiLayer; 
        public CanvasBehaviour Canvas => canvas;
        public UILayer UILayer => uiLayer; 
    }

    public enum UILayer
    {
        Menu,
        Gameplay, 
        Popup
        
    }
}