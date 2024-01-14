using CHARK.ScriptableEvents.Events;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "EventDispatcher", menuName = "Scriptable Objects/Event Dispatcher")]
    public class EventDispatcher : ScriptableObject
    {
        [SerializeField] private SimpleScriptableEvent onRunStartEvent;
        [SerializeField] private SimpleScriptableEvent onRunEndEvent;
        [SerializeField] private SimpleScriptableEvent onRoundStartEvent;
        [SerializeField] private SimpleScriptableEvent onRoundEndEvent;

    }
}