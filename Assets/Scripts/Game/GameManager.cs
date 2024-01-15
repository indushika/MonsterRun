using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Monster;
using MonsterRun.Round;
using MonsterRun.Canvas;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonsterRun.Main
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings; 
        [SerializeField] private MonsterWaveSpawner monsterWaveSpawner;
        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private DataConfigurations dataConfigurations; 
        
        private static GameManager instance;
        private GameInitializer gameInitializer;
        private RoundRunner roundRunner;
        private RoundDataProvider roundDataProvider;
        private PoolProvider poolProvider;

        private SimpleScriptableEvent onRoundEndEvent; 
        
        public static GameManager Instance => instance;
        public GameSettings GameSettings => gameSettings; 
        public GameInitializer GameInitializer => gameInitializer; 
        public RoundRunner RoundRunner => roundRunner;
        public RoundDataProvider RoundDataProvider => roundDataProvider;
        public MonsterWaveSpawner MonsterWaveSpawner => monsterWaveSpawner;
        public PoolProvider PoolProvider => poolProvider;
        public CanvasManager CanvasManager => canvasManager;
        public DataConfigurations DataConfigurations => dataConfigurations; 

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            if (gameSettings && monsterWaveSpawner && dataConfigurations && canvasManager)
            {
                roundDataProvider = new RoundDataProvider(dataConfigurations.RoundDataConfig); 
                roundRunner = new RoundRunner(roundDataProvider);
                poolProvider = new PoolProvider();
                onRoundEndEvent = dataConfigurations.RoundDataConfig.OnRoundEndEvent;
                canvasManager.Initialize(dataConfigurations.CanvasConfigDispatcher,dataConfigurations.CanvasEventDataConfig);
                
                gameInitializer = new GameInitializer(gameSettings,roundRunner, roundDataProvider, monsterWaveSpawner, 
                    poolProvider, dataConfigurations);
                
                onRoundEndEvent.AddListener((s) => OnRoundEndEventHandler() );

                gameInitializer.Initialize();
            }
            else
            {
                Debug.LogError("Game Settings or Data Configurations or Monster Wave Spawner is missing");
            }

            if (dataConfigurations.CanvasConfigDispatcher.
                TryGetCanvasConfigInstance(typeof(MainMenuCanvasConfig),out CanvasConfigBase config))
            {
                Debug.Log("Canvas config: " + config.GetType());
                var openCanvasEvent = dataConfigurations.CanvasEventDataConfig.OnOpenCanvasEvent;
                var eventArgs = new OnOpenCanvasEventArgs()
                {
                    CanvasConfig = config,
                    ClosePreviousCanvas = false
                };
                openCanvasEvent.Raise(eventArgs);
            }
            else
            {
                Debug.Log("Canvas config not found");
            }
            
        }

        public void StartGame()
        {
            roundRunner.StartNewRound();
        }

        public void PauseGame()
        {
            
        }
        
        private async void OnRoundEndEventHandler()
        {
            int seconds = dataConfigurations.RoundDataConfig.TimeBetweenRounds; 
            Debug.Log("Round Ended. Waiting: " + seconds);
            
            await Task.Delay(seconds * 1000); 
            roundRunner.StartNewRound();
        }
    }
}

