using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Events
{
    [CreateAssetMenu(
        fileName = "OnMonsterStopMoveScriptableEvent",
        menuName = ScriptableEventConstants.MenuNameCustom + "/On Monster Stop Move Event Args Scriptable Event",
        order = ScriptableEventConstants.MenuOrderCustom + 0
    )]
    public class OnMonsterStopMoveScriptableEvent : ScriptableEvent<OnMonsterStopMoveEventArgs>
    {
    }
}
