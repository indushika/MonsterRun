using System.Collections.Generic;
using MonsterRun.Main;
using UnityEngine;

namespace MonsterRun.UI
{
    public interface IUIService
    {
        public CanvasBehaviour CreateCanvas(CanvasConfigBase configBase);
        public void CloseCanvas(); 
    }
    public class UIManager : MonoBehaviour, IUIService
    {
        [SerializeField] private List<CanvasConfigBase> canvasConfigs;

        private Dictionary<CanvasConfigBase, CanvasBehaviour> canvasRegistry = new Dictionary<CanvasConfigBase, CanvasBehaviour>(); 
        private GameSettings gameSettings;
        private bool gameStartedFlag; 
        
        public void Initialize(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings;
            PopulateCanvasRegistry();
        }

        public CanvasBehaviour CreateCanvas(CanvasConfigBase configBase)
        {
            throw new System.NotImplementedException();
        }

        public void CloseCanvas()
        {
            throw new System.NotImplementedException();
        }

        #region Implementation

        private void PopulateCanvasRegistry()
        {
            foreach (var config in canvasConfigs)
            {
                canvasRegistry.TryAdd(config, config.Canvas); 
            }
        }

        #endregion
        
        private void OnGUI()
        {
            if (!gameStartedFlag)
            {
                if (GUI.Button(new Rect(10,10,150,100), "Start Game"))
                {
                    GameManager.Instance.StartGame();
                    gameStartedFlag = true; 
                }
            }
           
        }
    }
}