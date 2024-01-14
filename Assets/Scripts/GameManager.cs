using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Monster;
using MonsterRun.Round;
using MonsterRun.UI;
using UnityEngine;

namespace MonsterRun.Main
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings; 
        [SerializeField] private MonsterWaveSpawner monsterWaveSpawner;
        [SerializeField] private UIManager uiManager; 
        [SerializeField] private SimpleScriptableEvent onRoundStartEvent;
        [SerializeField] private SimpleScriptableEvent onRoundEndEvent;
        [SerializeField] private SimpleScriptableEvent onEndRoundEvent;

        
        private static GameManager instance;
        private GameInitializer gameInitializer;
        private RoundRunner roundRunner;
        private RoundDataProvider roundDataProvider;
        private PoolProvider poolProvider; 
        
        public static GameManager Instance => instance;
        public GameSettings GameSettings => gameSettings; 
        public GameInitializer GameInitializer => gameInitializer; 
        public RoundRunner RoundRunner => roundRunner;
        public RoundDataProvider RoundDataProvider => roundDataProvider;
        public MonsterWaveSpawner MonsterWaveSpawner => monsterWaveSpawner;
        public PoolProvider PoolProvider => poolProvider;
        public UIManager UIManager => uiManager; 

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
            if (gameSettings && monsterWaveSpawner)
            {
                roundDataProvider = new RoundDataProvider(gameSettings); 
                roundRunner = new RoundRunner(gameSettings,roundDataProvider,monsterWaveSpawner,
                    onRoundStartEvent, onRoundEndEvent, onEndRoundEvent);
                poolProvider = new PoolProvider();

                gameInitializer = new GameInitializer(gameSettings,roundRunner, roundDataProvider, monsterWaveSpawner, poolProvider);
                gameInitializer.Initialize();
            }
            else
            {
                Debug.LogError("Game Settings or Pool Collection Provider or Monster Wave Spawner is missing");
            }

            try
            {
                onRoundEndEvent.AddListener((s) => OnRoundEndHandler());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public void StartGame()
        {
            roundRunner.StartNewRound();
        }

        public void PauseGame()
        {
            
        }

        private async void OnRoundEndHandler()
        {
            //wait some second(s)
            await WaitUntilNextRound(gameSettings.TimeBetweenRounds); 
            roundRunner.StartNewRound();
            //start the next round
        }

        private async Task WaitUntilNextRound(int seconds)
        {
            await Task.Delay(seconds * 1000); 
            
        }
        //OnRoundStart 
        //OnRoundEnd 

        

        //GameSettings
        //GameInitializer (GameSettings, RoundRunner, MonsterSpawner) 

        //UIManager

        //RoundRunner 
        //RoundDataProvider -> provides the fibonacci seq number for the round

        //MonsterData
        //MonsterBehaviourBase : IDamageable, IInteractable, ICharacterAnimator, IMovement, IMonsterBehaviour 
        //MonsterWaveSpawner 

        //MonsterPoolProvider 
    }
}

