using CHARK.ScriptableEvents;
using ScriptableEvents.Events;
using UnityEngine;

namespace ScriptableEvents.Listeners
{
    [AddComponentMenu(
        ScriptableEventConstants.MenuNameCustom + "/On Round Start Event Args Scriptable Event Listener",
        ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class OnRoundStartScriptableEventListener : ScriptableEventListener<OnRoundStartEventArgs>
    {
    }
}
