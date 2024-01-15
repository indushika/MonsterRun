using System;
using System.Collections.Generic;
using MonsterRun.Canvas;
using UnityEngine;

namespace MonsterRun.Canvas
{
    [CreateAssetMenu(fileName = "CanvasConfigDispatcher", menuName = "Scriptable Objects/ Canvas Config Dispatcher")]
    public class CanvasConfigDispatcher : ScriptableObject
    {
        [SerializeField] private List<CanvasConfigBase> canvasConfigRegistry;

        private Dictionary<Type, CanvasConfigBase> canvasConfigByType; 

        private void PopulateCanvasConfigByType()
        {
            canvasConfigByType = new Dictionary<Type, CanvasConfigBase>(); 
            foreach (var canvasConfig in canvasConfigRegistry)
            {
                canvasConfigByType.Add(canvasConfig.GetType(),canvasConfig);
            }
        }
        
        public bool TryGetCanvasConfigInstance(Type configType, out CanvasConfigBase configInstance)
        {
            if (canvasConfigByType == null)
            {
                PopulateCanvasConfigByType();
            }

            return canvasConfigByType.TryGetValue(configType, out configInstance); 
        }
    }
}