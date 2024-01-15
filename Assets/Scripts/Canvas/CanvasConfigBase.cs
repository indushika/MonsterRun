using UnityEngine;

namespace MonsterRun.Canvas
{
    public abstract class CanvasConfigBase : ScriptableObject
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