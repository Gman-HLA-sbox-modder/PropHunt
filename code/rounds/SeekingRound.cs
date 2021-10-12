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
            PropHuntGame.SetWinner(PropHuntGame.PropTeam);
            PropHuntGame.ChangeRound(new FinishedRound());
        }

        private void CheckPlayerCount()
        {
            if(PropHuntGame.GetPlayersByTeam(PropHuntGame.SeekerTeam.Index, true).Count == 0)
            {
                PropHuntGame.SetWinner(PropHuntGame.PropTeam);
                PropHuntGame.ChangeRound(PropHuntGame.FinishedRound);
            }
            else if(PropHuntGame.GetPlayersByTeam(PropHuntGame.PropTeam.Index, true).Count == 0)
            {
                PropHuntGame.SetWinner(PropHuntGame.SeekerTeam);
                PropHuntGame.ChangeRound(PropHuntGame.FinishedRound);
            }
        }
    }
}