using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PropHunt
{
    public partial class Health : Panel
    {
        private Label HealthText;

        public Health()
        {
            StyleSheet.Load("/ui/Health.scss");
            Panel healthBackground = Add.Panel();
            healthBackground.Add.Label("HEALTH", "HealthTextName");
            HealthText = healthBackground.Add.Label("0", "HealthText");
        }

        public override void Tick()
        {
            base.Tick();
            AddClass("Hidden");
            PropHuntPlayer player = Local.Pawn as PropHuntPlayer;
            if(player == null)
                return;

            if(player.Health <= 0 || player.LifeState != LifeState.Alive)
                return;

            RemoveClass("Hidden");
            HealthText.Text = player.Health.CeilToInt().ToString();
        }
    }
}
