using System;
using System.Threading.Tasks;
using CHARK.ScriptableEvents;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Main;
using MonsterRun.Monster;
using ScriptableEvents.Events;
using UnityEngine;

namespace MonsterRun.Round
{
    public class RoundRunner
    {
        private RoundDataProvider roundDataProvider;
      
        private OnRoundStartScriptableEvent onRoundStartEvent;
        private SimpleScriptableEvent onRoundEndEvent; 
        private SimpleScriptableEvent onEndRoundEvent;

        private int timeBetweenRounds; 

        public RoundRunner(RoundDataProvider roundDataProvider)
        {
            this.roundDataProvider = roundDataProvider;
            var config = roundDataProvider.GetRoundDataConfig();

            onRoundStartEvent = config.OnRoundStartEvent;
            onRoundEndEvent = config.OnRoundEndEvent;
            onEndRoundEvent = config.OnEndRoundEvent;
            timeBetweenRounds = config.TimeBetweenRounds; 
            
            try
            {
                onEndRoundEvent.AddListener((s) => EndRound());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        public void StartNewRound()
        {
            var config = roundDataProvider.GetRoundDataConfig();
            var eventArgs = new OnRoundStartEventArgs()
            {
                RoundDataConfig = config
            };
            
            if (onRoundStartEvent)
            {
                onRoundStartEvent.Raise(eventArgs);
            }
            else
            {
                Debug.LogError("OnRoundStartEvent Missing.");
            }
        }

        public void EndRound()
        {
            onRoundEndEvent.Raise();
            roundDataProvider.UpdateRoundDataConfig(); 
        }

    
        
    }
}