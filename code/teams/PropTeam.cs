namespace PropHunt
{
	public class PropTeam : BaseTeam
    {
        public override string Name => "team_props";
        public override string HudName => "Props";

        public override void OnJoin(PropHuntPlayer player)
        {
            player.Respawn();
        }
    }
}