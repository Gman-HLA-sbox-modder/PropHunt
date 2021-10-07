using Sandbox;

namespace PropHunt
{
    public class SeekingRound : BaseRound
    {
        public override int RoundDuration => 180;
        public override string RoundName => "Seeking";

        public override void OnStart()
        {
            Log.Info(RoundName + " Round has started.");
        }

        public override void OnPlayerKilled(PropHuntPlayer player)
        {
            CheckPlayerCount();
        }

        public override void OnPlayerLeave(PropHuntPlayer player)
        {
            CheckPlayerCount();
        }

        public override void OnTimerEnd()
        {
            PropHuntGame.ChangeRound(new FinishedRound());
        }

        private void CheckPlayerCount()
        {
            if(PropHuntGame.GetTeamPlayerCount(PropHuntGame.SeekerTeam.Index) == 0)
            {
                //Props win
                PropHuntGame.ChangeRound(PropHuntGame.FinishedRound);
            }
            else if(PropHuntGame.GetTeamPlayerCount(PropHuntGame.PropTeam.Index) == 0)
            {
                //Seekers win
                PropHuntGame.ChangeRound(PropHuntGame.FinishedRound);
            }
        }
    }
}