namespace PropHunt
{
    public class SeekerTeam : BaseTeam
    {
        public override string Name => "team_seekers";
        public override string HudName => "Seekers";

        public override void OnJoin(PropHuntPlayer player)
        {
            player.Respawn();
        }
    }
}