using MonsterRun.Main;

namespace MonsterRun.Round
{
    public class RoundDataProvider
    {
        //provide round data -> monster spawn count per round
        private GameSettings gameSettings; 
        
        public RoundDataProvider(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings; 
        }

        public RoundDataConfig GetNewRoundData(uint roundCount)
        {
            if (roundCount == 0)
            {
                UpdateRoundCount(roundCount);
            }

            roundCount = gameSettings.RoundCount;
            var roundData = new RoundDataConfig(roundCount, GetMonsterSpawnCount(roundCount)); 
            return roundData; 
        }

        public void UpdateRoundCount(uint roundCount)
        {
            gameSettings.SetRoundCount(roundCount+1);
        }

        private int GetMonsterSpawnCount(uint roundCount)
        {
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
    }
}