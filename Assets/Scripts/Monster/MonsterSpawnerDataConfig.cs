using System.Collections.Generic;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Monster;
using ScriptableEvents.Events;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterRun.Monster
{
    [CreateAssetMenu(fileName = "MonsterSpawnerDataConfig",
        menuName = "Scriptable Objects/Data Configurations/Monster Spawner Data Config")]
    public class MonsterSpawnerDataConfig : ScriptableObject
    {
        [SerializeField] private List<MonsterData> monsters;

        [SerializeField] private SimpleScriptableEvent onStartMonsterMoveEvent;
        [SerializeField] private OnMonsterStopMoveScriptableEvent onMonsterStopEvent;
        
        [SerializeField] private int destroyMonsterInSeconds;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private Vector2 direction;
        [SerializeField] private int activeMonsterCount;
        [SerializeField] private int spawnedMonsterCount;
        [Range(0, 1f)] [SerializeField] private float screenXPositionOffset; 
        [Range(0, 1f)] [SerializeField] private float screenYPositionOffset; 

        private Camera mainCamera; 
        
        public List<MonsterData> Monsters => monsters; 
        public SimpleScriptableEvent OnStartMonsterMoveEvent => onStartMonsterMoveEvent;
        public OnMonsterStopMoveScriptableEvent OnMonsterStopMoveEvent => onMonsterStopEvent; 
        
        public int DestroyMonsterInSeconds => destroyMonsterInSeconds;
        public float MinSpeed => minSpeed; 
        public float MaxSpeed => maxSpeed;
        public Vector2 Direction => direction;
        public float ScreenXPositionOffset => screenXPositionOffset;
        public float ScreenYPositionOffset => screenYPositionOffset;
        

        
        public int ActiveMonsterCount
        {
            get
            {
                return activeMonsterCount; 
            }
            set
            {
                activeMonsterCount = value >= 0 ? value : 0; 
            }
        }
        public int SpawnedMonsterCount
        {
            get
            {
                return spawnedMonsterCount; 
            }
            set
            {
                spawnedMonsterCount = value > 0 ? value : spawnedMonsterCount; 
            }
        }

        public Camera MainCamera
        {
            get
            {
                if (!mainCamera)
                {
                    mainCamera = Camera.main; 
                }

                return mainCamera; 
            }
        }
            

    }
}