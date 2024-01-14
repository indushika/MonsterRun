using System;
using System.Collections.Generic;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Main;
using MonsterRun.Round;
using ScriptableEvents.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MonsterRun.Monster
{
    public class MonsterWaveSpawner : MonoBehaviour
    {
        [SerializeField] private List<MonsterData> monsters;

        [SerializeField] private SimpleScriptableEvent onStartMonsterMoveEvent;
        [SerializeField] private OnMonsterStopMoveScriptableEvent onMonsterStopEvent;
        [SerializeField] private SimpleScriptableEvent onEndRoundEvent;

        private IPoolProvider poolProvider;
        private MonsterPoolProvider monsterPoolProvider;  
        private int activeMonsterCount;

        public MonsterPoolProvider MonsterPoolProvider => monsterPoolProvider; 

        #region API

        public void Initialize(IPoolProvider poolProvider)
        {
            this.poolProvider = poolProvider;
            monsterPoolProvider = new MonsterPoolProvider(this.poolProvider); 
            try
            {
                onMonsterStopEvent.AddListener((eventArgs) => OnRunEndEventHandler(eventArgs));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void SpawnMonstersForRound(RoundDataConfig roundDataConfig)
        {
            var boundaryData = GetNewBoundaryData(); 
            if (poolProvider != null)
            {
                int spawnCount = roundDataConfig.MonsterSpawnCount;

                for (int i = 0; i < spawnCount; i++)
                {
                    var monsterData = GetRandomMonster();
                    var monster = monsterPoolProvider.GetPoolByType(monsterData).Get(); 
                    if (monster)
                    {
                        monster.Initialize(monsterData,GetNewDynamicData(), boundaryData,onStartMonsterMoveEvent,onMonsterStopEvent);
                    }
                }
                GameManager.Instance.GameSettings.SpawnedMonsterCount = spawnCount; 
                GameManager.Instance.GameSettings.ActiveMonsterCount = spawnCount; 

            }
            else
            {
                Debug.LogError("Pool Provider is missing.");
            }
        }

        public void StartMonsterRun()
        {
            try
            {
                onStartMonsterMoveEvent.Raise();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion

        #region Implementation

        private DynamicData GetNewDynamicData()
        {
            var gameSettings = GameManager.Instance.GameSettings;
            float speed = Random.Range(gameSettings.MinSpeed, gameSettings.MaxSpeed);
            return new DynamicData(gameSettings.Direction, speed);
        }

        private BoundaryData GetNewBoundaryData()
        {
            var settings = GameManager.Instance.GameSettings;
            try
            {
                return new BoundaryData(settings.ScreenBoundaryTransform.position, settings.MinimumDistanceToBoundary);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private MonsterData GetRandomMonster()
        {
            int rand = Random.Range(0, monsters.Count);
            return monsters[rand]; 
        }

        private void OnRunEndEventHandler(OnMonsterStopMoveEventArgs args)
        {
            //reduce game settings monster count 
            var pool = monsterPoolProvider.GetPoolByType(args.Monster.MonsterData); 
            pool.Release(args.Monster);
            GameManager.Instance.GameSettings.ActiveMonsterCount -= 1;

            Debug.Log("Active Monster Count: " + GameManager.Instance.GameSettings.ActiveMonsterCount);
            if (GameManager.Instance.GameSettings.ActiveMonsterCount == 0)
            {
                onEndRoundEvent.Raise();
            }
        }
        
        #endregion
        
    }
}