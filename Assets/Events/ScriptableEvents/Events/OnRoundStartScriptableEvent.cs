using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Events
{
    [CreateAssetMenu(
        fileName = "OnRoundStartScriptableEvent",
        menuName = ScriptableEventConstants.MenuNameCustom + "/On Round Start Scriptable Event",
        order = ScriptableEventConstants.MenuOrderCustom + 0
    )]
    public class OnRoundStartScriptableEvent : ScriptableEvent<OnRoundStartEventArgs>
    {
    }
}
