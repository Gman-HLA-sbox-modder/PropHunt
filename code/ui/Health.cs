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
            Panel healthBackground = Add.Panel("Health");
            HealthText = healthBackground.Add.Label("0", "HealthText");
            healthBackground.Add.Label("HEALTH", "HealthTextName");
        }

        public override void Tick()
        {
            base.Tick();
            Entity player = Local.Pawn;
            if(player == null)
                return;

            HealthText.Text = player.Health.CeilToInt().ToString();
        }
    }
}
