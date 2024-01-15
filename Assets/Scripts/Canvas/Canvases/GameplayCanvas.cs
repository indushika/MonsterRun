using System;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Main;
using ScriptableEvents.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterRun.Canvas
{
    public class GameplayCanvas : CanvasBehaviour
    {
        [SerializeField] private TextMeshProUGUI activeMonsterCountLabel; 
        [SerializeField] private TextMeshProUGUI roundTimeCountLabel;
        [SerializeField] private TextMeshProUGUI activeMonsterCountText;
        [SerializeField] private TextMeshProUGUI roundTimeCountText; 
        
        private GameplayCanvasConfig gameplayCanvasConfig;
        private OnRoundStartScriptableEvent onRoundStartEvent;
        private SimpleScriptableEvent onRoundEndEvent;

        private bool inRoundFlag;
        private float toSecondCounter;
        private float timeElapsedSinceRoundStart;

        public override void OnInitialized()
        {
            if (Config is GameplayCanvasConfig gamePlayCanvasConfig)
            {
                this.gameplayCanvasConfig = gamePlayCanvasConfig; 
            }
            else
            {
                Debug.LogError("Canvas Config Type is incorrect, make sure it's type of: ");
            }

            var roundDataConfig = GameManager.Instance.DataConfigurations.RoundDataConfig;
            onRoundStartEvent = roundDataConfig.OnRoundStartEvent;
            onRoundEndEvent = roundDataConfig.OnRoundEndEvent;

            try
            {
                onRoundStartEvent.AddListener((eventArgs) => OnRoundStartEventHandler(eventArgs));
                onRoundEndEvent.AddListener((args) => OnRoundEndEventHandler());
                SetupCanvasData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void OnDestroy()
        {
            onRoundStartEvent.RemoveListener((eventArgs) => OnRoundStartEventHandler(eventArgs));
            onRoundEndEvent.RemoveListener((args) => OnRoundEndEventHandler());
        }

        private void OnRoundStartEventHandler(OnRoundStartEventArgs roundStartEventArgs)
        {
            inRoundFlag = true;
            timeElapsedSinceRoundStart = 0;
            toSecondCounter = 0; 
            try
            {
                activeMonsterCountText.text = roundStartEventArgs.RoundDataConfig.MonsterSpawnCount.ToString();
                SetRoundTimerText();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private void OnRoundEndEventHandler()
        {
            inRoundFlag = false; 
        }

        private void SetupCanvasData()
        {
            try
            {
                activeMonsterCountLabel.text = gameplayCanvasConfig.ActiveMonsterCountLabel;
                roundTimeCountLabel.text = gameplayCanvasConfig.RoundTimeLabel; 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Update()
        {
            if (inRoundFlag)
            {
                toSecondCounter += Time.deltaTime;
                if (toSecondCounter > 1)
                {
                    timeElapsedSinceRoundStart += toSecondCounter;
                    if (roundTimeCountText)
                    {
                        SetRoundTimerText();
                    }
                    toSecondCounter = 0; 
                }
            }
        }

        private void SetRoundTimerText()
        {
            roundTimeCountText.text = Mathf.Round(timeElapsedSinceRoundStart) + " s";
        }
    }
}