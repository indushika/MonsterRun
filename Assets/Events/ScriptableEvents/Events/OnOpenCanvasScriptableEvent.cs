using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Events
{
    [CreateAssetMenu(
        fileName = "OnOpenCanvasScriptableEvent",
        menuName = ScriptableEventConstants.MenuNameCustom + "/On Open Canvas Event Args Scriptable Event",
        order = ScriptableEventConstants.MenuOrderCustom + 0
    )]
    public class OnOpenCanvasScriptableEvent : ScriptableEvent<OnOpenCanvasEventArgs>
    {
    }
}
