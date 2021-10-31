using System.Collections.Generic;

namespace PropHunt
{
    public class FinishedRound : BaseRound
    {
        public override int RoundDuration => 15;
        public override string RoundName => "Round over";

        public override void OnTimerEnd()
        {
            List<PropHuntPlayer> seekers = PropHuntGame.GetPlayersByTeam(PropHuntGame.SeekerTeam.Index);
            List<PropHuntPlayer> props = PropHuntGame.GetPlayersByTeam(PropHuntGame.PropTeam.Index);

            foreach(PropHuntPlayer player in seekers)
                player.SetTeam(PropHuntGame.PropTeam.Index);

            foreach(PropHuntPlayer player in props)
                player.SetTeam(PropHuntGame.SeekerTeam.Index);

            PropHuntGame.ChangeRound(new WaitingRound());
        }
    }
}