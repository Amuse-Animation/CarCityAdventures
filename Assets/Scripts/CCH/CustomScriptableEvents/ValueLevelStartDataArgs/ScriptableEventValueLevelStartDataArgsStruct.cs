using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCH.LevelStartInit.Controllers;
using UnityEngine;

namespace CCH.CustomScriptableEvents.ValueLevelStartDataArgs
{
    [CreateAssetMenu(fileName = "CustomScriptableEventLevelStartDataArgs", menuName = "CustomScriptableEventsArgs/StartDataArgs", order = 5)]
    public class ScriptableEventValueLevelStartDataArgsStruct : ScriptableEventBaseArgs<LevelStartDataStruct>
    {
    }
}
