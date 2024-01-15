using MonsterRun.Canvas;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayCanvasConfig", 
    menuName = "Scriptable Objects/Data Configurations/Gameplay Canvas Config")]
    public class GameplayCanvasConfig : CanvasConfigBase
    {
        public string ActiveMonsterCountLabel;
        public string RoundTimeLabel; 
    }
