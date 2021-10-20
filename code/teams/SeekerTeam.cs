namespace PropHunt
{
    public class SeekerTeam : BaseTeam
    {
        public override string Name => "team_seekers";
        public override string HudName => "Seekers";
        public override Color32 Color => new Color32(255, 240, 0);

        public override void OnJoin(PropHuntPlayer player)
        {
            base.OnJoin(player);
            player.Respawn();
        }
    }
}