using System;
using System.Collections.Generic;
using System.Linq;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Main;
using ScriptableEvents.Events;
using UnityEngine;

namespace MonsterRun.Canvas
{
    public interface ICanvas
    {
        public void OpenCanvas(CanvasConfigBase canvasConfig, bool closePreviousCanvas = true); 
        public void CloseCanvas(); 
    }
    public class CanvasManager : MonoBehaviour, ICanvas
    {
         private CanvasConfigDispatcher configDispatcher;
        
        private SimpleScriptableEvent onCloseCanvasEvent;
        private OnOpenCanvasScriptableEvent onOpenCanvasEvent; 
        
        [SerializeField]
        private List<CanvasConfigBase> activeCanvasConfigs;
        private Dictionary<CanvasConfigBase, CanvasBehaviour> activeCanvases;
        private bool gameStartedFlag;

        
        public void Initialize(CanvasConfigDispatcher canvasConfigDispatcher,CanvasEventDataConfig canvasEventDataConfig)
        {
            configDispatcher = canvasConfigDispatcher; 
            onCloseCanvasEvent = canvasEventDataConfig.OnCloseCanvasEvent;
            onOpenCanvasEvent = canvasEventDataConfig.OnOpenCanvasEvent; 
            try
            {
                onCloseCanvasEvent.AddListener((s) => CloseCanvas());
                onOpenCanvasEvent.AddListener((eventArgs) => OpenCanvas(eventArgs.CanvasConfig,eventArgs.ClosePreviousCanvas));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            activeCanvasConfigs = new List<CanvasConfigBase>();
            activeCanvases = new Dictionary<CanvasConfigBase, CanvasBehaviour>();

        }
        
        public void OpenCanvas(CanvasConfigBase configBase, bool closePreviousCanvas = true) 
        {
            if (configDispatcher)
            {
                if (configDispatcher.TryGetCanvasConfigInstance(configBase.GetType() , out CanvasConfigBase canvasConfig))
                {
                    Debug.Log("Active Canvas Configs : " + activeCanvasConfigs.Count);

                    if (closePreviousCanvas)
                    {
                        CloseActiveCanvas();
                    }
                    activeCanvasConfigs.Add(canvasConfig);
                    Debug.Log("Active Canvas Configs : " + activeCanvasConfigs.Count);
                    var canvas = CreateOrGetCanvas(canvasConfig);
                    canvas.Initialize(canvasConfig);
                    canvas.Open();

                }
            }
            else
            {
                Debug.LogError("Config Dispatcher is missing.");
            }

        }

        public void CloseCanvas()
        {
            if (activeCanvasConfigs.Count > 1)
            {
                CloseActiveCanvas();
                var newConfig = activeCanvasConfigs.Last();
                var newCanvas = CreateOrGetCanvas(newConfig);
                newCanvas.Initialize(newConfig);
                newCanvas.Open();
            }

        }

        #region Implementation
        

        private CanvasBehaviour CreateOrGetCanvas(CanvasConfigBase configBase)
        {
            if (activeCanvases.TryGetValue(configBase, out CanvasBehaviour canvas))
            {
                return canvas;
            }

            CanvasBehaviour newCanvas = Instantiate(configBase.Canvas);
            activeCanvases.Add(configBase, newCanvas);
            return newCanvas;
        }

        private void CloseActiveCanvas()
        {
            if (activeCanvasConfigs.Count > 0)
            {
                var activeConfig = activeCanvasConfigs.Last();
                if (activeCanvases.TryGetValue(activeConfig, out CanvasBehaviour canvas))
                {
                    canvas.Close();
                    activeCanvasConfigs.Remove(activeConfig);
                    Debug.Log("Closing active canvas: " + activeConfig);
                }
            }
        }

        #endregion
        
        // private void OnGUI()
        // {
        //     if (!gameStartedFlag)
        //     {
        //         if (GUI.Button(new Rect(10,10,150,100), "Start Game"))
        //         {
        //             GameManager.Instance.StartGame();
        //             gameStartedFlag = true; 
        //         }
        //     }
        //    
        // }
    }
}