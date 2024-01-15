using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

namespace MonsterRun.Main
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        // [SerializeField] private uint roundCount;
        // [SerializeField] private int destroyMonsterInSeconds;
        // [SerializeField] private float minSpeed; 
        // [SerializeField] private float maxSpeed; 
        // [SerializeField] private Vector2 direction;
        // [SerializeField] private int timeBetweenRounds;
        // [SerializeField] private Transform screenBoundaryMarker;
        // [SerializeField] private float minimumDistancceToBoundary; 
        //
        // [SerializeField]
        // private int activeMonsterCount;
        // [SerializeField]
        // private int spawnedMonsterCount; 
        //
        //
        // public uint RoundCount => roundCount;
        // public int DestroyMonsterInSeconds => destroyMonsterInSeconds;
        // public float MinSpeed => minSpeed; 
        // public float MaxSpeed => maxSpeed;
        // public Vector2 Direction => direction;
        // public int TimeBetweenRounds => timeBetweenRounds;
        // public Transform ScreenBoundaryTransform => screenBoundaryMarker;
        // public float MinimumDistanceToBoundary => minimumDistancceToBoundary; 
        // public int ActiveMonsterCount
        // {
        //     get
        //     {
        //         return activeMonsterCount; 
        //     }
        //     set
        //     {
        //         activeMonsterCount = value >= 0 ? value : 0; 
        //     }
        // }
        // public int SpawnedMonsterCount
        // {
        //     get
        //     {
        //         return spawnedMonsterCount; 
        //     }
        //     set
        //     {
        //         spawnedMonsterCount = value > 0 ? value : spawnedMonsterCount; 
        //     }
        // }
        //
        // public void SetRoundCount(uint lastRoundCount)
        // {
        //     this.roundCount = lastRoundCount; 
        // }
        
    }
}

