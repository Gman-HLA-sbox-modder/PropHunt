using Sandbox.UI;

namespace PropHunt
{
    public partial class Crosshair : RootPanel
    {
        public Crosshair()
        {
            StyleSheet.Load("/ui/Crosshair.scss");
            Add.Panel("Crosshair");
        }
    }
}
