using Sandbox;
using Sandbox.UI;

namespace PropHunt
{
    public partial class MainHud : RootPanel
    {
        public MainHud()
        {
            //Default
            AddChild<ChatBox>();
            AddChild<KillFeed>();
            AddChild<Scoreboard<ScoreboardEntry>>();
            AddChild<VoiceList>();

            //Custom
            AddChild<Ammo>();
            AddChild<Health>();
            AddChild<Team>();
            AddChild<TeamSelection>();
            AddChild<Timer>();

            CrosshairCanvas.SetCrosshair(new Crosshair());
        }
    }
}
