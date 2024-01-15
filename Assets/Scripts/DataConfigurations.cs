using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using MonsterRun.Canvas;
using MonsterRun.Round;
using UnityEngine;

[CreateAssetMenu(fileName = "DataConfigurations",menuName = "Scriptable Objects/Data Configuration/ Collection")]
public class DataConfigurations : ScriptableObject
{
    [SerializeField] private RoundDataConfig roundDataConfig;
    [SerializeField] private MonsterSpawnerDataConfig monsterSpawnerDataConfig;
    [SerializeField] private CanvasEventDataConfig canvasEventDataConfrig; 
    [SerializeField] private CanvasConfigDispatcher configDispatcher;

    
    public RoundDataConfig RoundDataConfig => roundDataConfig;
    public MonsterSpawnerDataConfig MonsterSpawnerDataConfig => monsterSpawnerDataConfig;
    public CanvasEventDataConfig CanvasEventDataConfig => canvasEventDataConfrig;
    public CanvasConfigDispatcher CanvasConfigDispatcher => configDispatcher; 
}
