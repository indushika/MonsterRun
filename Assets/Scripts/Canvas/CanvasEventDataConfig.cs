using System.Collections;
using System.Collections.Generic;
using CHARK.ScriptableEvents.Events;
using ScriptableEvents.Events;
using UnityEngine;

namespace MonsterRun.Canvas
{
    [CreateAssetMenu(fileName = "MonsterSpawnerDataConfig",
        menuName = "Scriptable Objects/Data Configurations/Canvas Event Data Config")]
    public class CanvasEventDataConfig : ScriptableObject
    {
        [SerializeField] private SimpleScriptableEvent onCloseCanvasEvent;
        [SerializeField] private OnOpenCanvasScriptableEvent onOpenCanvasEvent;

        public SimpleScriptableEvent OnCloseCanvasEvent => onCloseCanvasEvent;
        public OnOpenCanvasScriptableEvent OnOpenCanvasEvent => onOpenCanvasEvent; 
    }
}

