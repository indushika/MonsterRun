using MonsterRun.Main;
using UnityEngine;

namespace MonsterRun.Round
{
    public class RoundDataProvider
    {
        private RoundDataConfig roundDataConfig;

        #region API

        public RoundDataProvider(RoundDataConfig roundDataConfig)
        {
            this.roundDataConfig = roundDataConfig;
            if (!this.roundDataConfig)
            {
                Debug.LogError("Round Data Config is missing.");
            }
        }

        public RoundDataConfig GetRoundDataConfig()
        {
            if (roundDataConfig.RoundNumber == 0)
            {
                UpdateRoundCount();
            }
            roundDataConfig.SetMonsterSpawnCount(GetMonsterSpawnCount());
            return roundDataConfig; 
        }

        public void UpdateRoundDataConfig()
        {
            UpdateRoundCount();
        }

        #endregion

        #region Implementation
        private void UpdateRoundCount()
        {
            roundDataConfig.SetRoundNumber(roundDataConfig.RoundNumber+1);
        }

        private int GetMonsterSpawnCount()
        {
            uint roundCount = roundDataConfig.RoundNumber; 
            int nMinusTwo = 0;
            int nMinusOne = 1;
            int n = nMinusOne; 

            if (roundCount > 1)
            {
                for (int i = 2; i < roundCount+1; i++)
                {
                    n = nMinusOne + nMinusTwo;
                    nMinusTwo = nMinusOne;
                    nMinusOne = n; 
                }
            }
            
            return n; 
        }

        #endregion
        
    }
}