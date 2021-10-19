namespace PropHunt
{
	public class PropTeam : BaseTeam
    {
        public override string Name => "team_props";
        public override string HudName => "Props";
        public override Color32 Color => new Color32(0, 160, 240);

        public override void OnJoin(PropHuntPlayer player)
        {
            base.OnJoin(player);
            player.Respawn();
        }
    }
}