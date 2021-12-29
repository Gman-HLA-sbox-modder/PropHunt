using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class MainHud : RootPanel
    {
        public MainHud()
        {
            StyleSheet.Load("/ui/MainHud.scss");
            PropHuntPlayer player = Local.Pawn as PropHuntPlayer;
            if(player != null)
            {
                BaseTeam team = PropHuntGame.GetTeam(player.TeamIndex);
                AddClass(team?.HudName);
            }

            //Default
            AddChild<ChatBox>();
            AddChild<KillFeed>();
            AddChild<Scoreboard<ScoreboardEntry>>();
            AddChild<VoiceList>();

            //Custom
            AddChild<Ammo>();
            AddChild<Blind>();
            AddChild<Counter>();
            AddChild<Crosshair>();
            AddChild<EndScreen>();
            AddChild<Health>();
            AddChild<Team>();
            AddChild<TeamSelection>();
            AddChild<Timer>();
        }

        public void OnJoinTeam(PropHuntPlayer player, BaseTeam team)
        {
            if(player != Local.Pawn)
                return;

            AddClass(team.HudName);
        }

        public void OnLeaveTeam(PropHuntPlayer player, BaseTeam team)
        {
            if(player != Local.Pawn)
                return;

            RemoveClass(team.HudName);
        }
    }
}
