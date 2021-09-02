using Sandbox;

namespace PropHunt
{
    [Library("prop_physics")]
    public partial class Prop : Sandbox.Prop, IUse
    {
        public bool IsUsable(Entity user)
        {
            return true;
        }

        public bool OnUse(Entity user)
        {
            if(user is PropHuntPlayer player)
                player.OnUseProp(this);

            return false;
        }
    }
}
