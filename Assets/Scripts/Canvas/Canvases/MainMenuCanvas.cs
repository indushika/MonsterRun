using System;
using MonsterRun.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterRun.Canvas
{
    public class MainMenuCanvas : CanvasBehaviour
    {
        [SerializeField] private TextMeshProUGUI startButtonText;
        [SerializeField] private Button startButton; 

        private MainMenuCanvasConfig mainMenuCanvasConfig; 
        public override void OnInitialized()
        {
            if (Config is MainMenuCanvasConfig mainMenuCanvasConfig)
            {
                this.mainMenuCanvasConfig = mainMenuCanvasConfig; 
            }
            else
            {
                Debug.LogError("Canvas Config Type is incorrect, make sure it's type of: ");
            }
        }

        private void Start()
        {
            startButton.onClick.AddListener(OnStartHandler);
        }

        public void OnStartHandler()
        {
            var gameManager = GameManager.Instance;
            var dataConfig = gameManager.DataConfigurations; 
            var openCanvasEvent = dataConfig.CanvasEventDataConfig.OnOpenCanvasEvent;
            
            if (dataConfig.CanvasConfigDispatcher.TryGetCanvasConfigInstance(typeof(GameplayCanvasConfig),
                    out CanvasConfigBase config))
            {
                var eventArgs = new OnOpenCanvasEventArgs()
                {
                    CanvasConfig = config,
                    ClosePreviousCanvas = true
                }; 
                openCanvasEvent.Raise(eventArgs);
            }
            gameManager.StartGame();
        }
    }
}