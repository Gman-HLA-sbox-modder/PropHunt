using Sandbox;

namespace PropHunt
{
    public class HidingRound : BaseRound
    {
        public override int RoundDuration => 30;
        public override string RoundName => "Hiding";

        public override void OnStart()
        {
            foreach(Client client in Client.All)
            {
                PropHuntPlayer player = client.Pawn as PropHuntPlayer;
                if(player.TeamIndex == PropHuntGame.SeekerTeam.Index || player.TeamIndex == PropHuntGame.PropTeam.Index)
                    player.Respawn();
            }
        }

        public override void OnTimerEnd()
        {
            PropHuntGame.ChangeRound(PropHuntGame.SeekingRound);
        }
    }
}